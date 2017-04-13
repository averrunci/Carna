// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Fievus.Windows.Mvc;

using Carna.Runner;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Provides the function to handle evnets on the view of <see cref="CarnaUwpRunnerHost"/>.
    /// </summary>
    public class CarnaUwpRunnerHostController
    {
        /// <summary>
        /// Gets or sets <see cref="CarnaUwpRunnerHost"/>.
        /// </summary>
        [DataContext]
        public CarnaUwpRunnerHost Content { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ItemsControl"/> that contains fixtures.
        /// </summary>
        [Element]
        public ItemsControl FixtureItemsControl { get; set; }

        [EventHandler(Event = "SizeChanged")]
        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => AdjustFixtureContentMaxWidth(e.NewSize.Width);

        private void AdjustFixtureContentMaxWidth(double width)
        {
            foreach (FrameworkElement child in FixtureItemsControl.ItemsPanelRoot.Children)
            {
                child.MaxWidth = width - 52;
            }
        }

        [EventHandler(Event = "Loaded")]
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var engine = new FixtureEngine().LoadConfiguration();

            Content.Summary.IsFixtureBuilding.Value = true;
            var fixtures = await Task.Run(() => engine.BuildFixtures().ToList());
            await Task.Run(() => Configure(fixtures, Content.Fixtures, engine.Filter, (sender as DependencyObject)?.Dispatcher));
            SetChildOpenCondition(Content.Fixtures);
            Content.Summary.IsFixtureBuilding.Value = false;
            Content.Summary.IsFixtureBuilt.Value = true;
            Content.Summary.StartDateTime.Value = DateTime.UtcNow.ToString("u");

            AdjustFixtureContentMaxWidth((sender as FrameworkElement).ActualWidth);

            Content.Summary.IsFixtureRunning.Value = true;
            var results = await Task.Run(() => engine.RunFixtures(fixtures));
            Content.Summary.IsFixtureRunning.Value = false;
            Content.Summary.StartDateTime.Value = results.StartTime().ToString("u");
            Content.Summary.EndDateTime.Value = results.EndTime().ToString("u");
            Content.Summary.Duration.Value = $"{(results.EndTime() - results.StartTime()).TotalSeconds:0.000} seconds";
        }

        private async Task Configure(IEnumerable<IFixture> fixtures, IList<FixtureContent> fixtureContents, IFixtureFilter filter, CoreDispatcher dispatcher)
        {
            if (fixtures == null || !fixtures.Any()) { return; }

            foreach (var fixture in fixtures)
            {
                if (!fixture.CanRun(filter)) { continue; }

                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    fixtureContents.Add(CreateFixtureContent(fixture, dispatcher))
                );

                var childFixtures = RetrieveChildFixtures(fixture);
                if (childFixtures == null || !childFixtures.Any())
                {
                    await ConfigureSummary(fixture, dispatcher);
                }
                else
                {
                    await Configure(childFixtures, fixtureContents.Last().Fixtures, filter, dispatcher);
                }
            }
        }

        private IEnumerable<IFixture> RetrieveChildFixtures(IFixture fixture)
            => (fixture as FixtureContainer)?.GetType().GetProperty("Fixtures", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(fixture) as IEnumerable<IFixture>;

        private async Task ConfigureSummary(IFixture fixture, CoreDispatcher dispatcher)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ++Content.Summary.TotalCount.Value;
            });

            fixture.FixtureRun += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    switch (e.Result.Status)
                    {
                        case FixtureStatus.Passed: ++Content.Summary.PassedCount.Value; break;
                        case FixtureStatus.Failed: ++Content.Summary.FailedCount.Value; break;
                        case FixtureStatus.Pending: ++Content.Summary.PendingCount.Value; break;
                    }
                });
            };
        }

        private FixtureContent CreateFixtureContent(IFixture fixture, CoreDispatcher dispatcher)
        {
            var fixtureContent = new FixtureContent();
            fixtureContent.Description.Value = Content.Formatter.FormatFixture(fixture.FixtureDescriptor).ToString();

            fixture.FixtureRunning += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    fixtureContent.Status.Value = FixtureStatus.Running
                );
            };
            fixture.FixtureRun += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    fixtureContent.Description.Value = Content.Formatter.FormatFixture(fixture.FixtureDescriptor).ToString();
                    fixtureContent.Status.Value = e.Result.Status;
                    fixtureContent.Duration.Value = e.Result.Duration.HasValue ? $"{e.Result.Duration.Value.TotalSeconds:0.000} s" : string.Empty;
                    fixtureContent.Exception.Value = e.Result.Exception?.ToString();
                    e.Result.StepResults.Aggregate(fixtureContent.Steps, (steps, stepResult) =>
                    {
                        var fixtureStepContent = new FixtureStepContent();
                        fixtureStepContent.Description.Value = Content.Formatter.FormatFixtureStep(stepResult.Step).ToString();
                        fixtureStepContent.Status.Value = stepResult.Status;
                        fixtureStepContent.Duration.Value = stepResult.Duration.HasValue ? $"{stepResult.Duration.Value.TotalSeconds:0.000} s" : string.Empty;
                        fixtureStepContent.Exception.Value = stepResult.Exception?.ToString();
                        steps.Add(fixtureStepContent);
                        return steps;
                    });
                });
            };

            return fixtureContent;
        }

        private void SetChildOpenCondition(IEnumerable<FixtureContent> fixtureContents)
        {
            var limit = 100;

            if (Content.Summary.TotalCount.Value <= limit)
            {
                SetChildOpen(fixtureContents, true);
                return;
            }

            var assemblyFixtureContents = fixtureContents;
            if (assemblyFixtureContents.Count() > limit) { return; }

            SetChildOpen(assemblyFixtureContents);
            limit -= assemblyFixtureContents.Count();

            var namespaceFixtureContents = assemblyFixtureContents.SelectMany(fixtureContent => fixtureContent.Fixtures).ToList();
            if (namespaceFixtureContents.Count() > limit) { return; }

            SetChildOpen(namespaceFixtureContents);
            limit -= namespaceFixtureContents.Count();

            var rootFixtureContents = namespaceFixtureContents.SelectMany(fixtureContent => fixtureContent.Fixtures).ToList();
            if (rootFixtureContents.Count() > limit) { return; }

            SetChildOpen(rootFixtureContents);
        }

        private void SetChildOpen(IEnumerable<FixtureContent> fixtureContents, bool recursive = false)
        {
            foreach (var fixtureContent in fixtureContents)
            {
                fixtureContent.IsChildOpen.Value = true;
                if (recursive) { SetChildOpen(fixtureContent.Fixtures, recursive); }
            }
        }
    }
}
