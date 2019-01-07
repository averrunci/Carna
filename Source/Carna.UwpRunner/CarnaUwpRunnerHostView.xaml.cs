// Copyright (C) 2017-2019 Fievus
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

using Carna.Runner;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a view for CarnaUwpRunnerHost.
    /// </summary>
    public sealed partial class CarnaUwpRunnerHostView
    {
        private CarnaUwpRunnerHost Host => DataContext as CarnaUwpRunnerHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarnaUwpRunnerHostView"/> class
        /// with the specified host.
        /// </summary>
        /// <param name="host">The host of CarnaUwpRunner.</param>
        public CarnaUwpRunnerHostView(CarnaUwpRunnerHost host)
        {
            DataContext = host;

            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => AdjustFixtureContentMaxWidth(e.NewSize.Width);
        private void AdjustFixtureContentMaxWidth(double width)
        {
            if (FixtureItemsControl.ItemsPanelRoot == null) return;

            foreach (var child in FixtureItemsControl.ItemsPanelRoot.Children.OfType<FrameworkElement>())
            {
                child.MaxWidth = width - 52;
            }
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is FrameworkElement element)) return;

                var engine = new FixtureEngine().LoadConfiguration(Host);

                Host.Summary.OnFixtureBuildingStarting();
                var fixtures = await BuildFixtures(engine, element.Dispatcher);
                Host.Summary.OnFixtureBuildingCompleted(DateTime.UtcNow);

                AdjustFixtureContentMaxWidth(element.ActualWidth);

                Host.Summary.OnFixtureRunningStarting();
                var results = await Task.Run(() => engine.RunFixtures(fixtures));
                Host.Summary.OnFixtureRunningCompleted(results);
            }
            catch (Exception exc)
            {
                Host.ErrorMessage = exc.ToString();
            }
            finally
            {
                if (Host.AutoExit)
                {
                    Application.Current.Exit();
                }
            }
        }

        private async Task<List<IFixture>> BuildFixtures(FixtureEngine engine, CoreDispatcher dispatcher)
        {
            var fixtures = await Task.Run(() => engine.BuildFixtures().ToList());
            var fixtureContents = Host.Fixtures;
            await Task.Run(() => Configure(fixtures, fixtureContents, engine.Filter, dispatcher));
            SetChildOpenCondition(Host.Fixtures);
            return fixtures;
        }

        private async Task Configure(IEnumerable<IFixture> fixtures, IList<FixtureContent> fixtureContents, IFixtureFilter filter, CoreDispatcher dispatcher)
        {
            if (fixtures == null || !fixtures.Any()) return;

            foreach (var fixture in fixtures)
            {
                if (!fixture.CanRun(filter)) continue;

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
                    await Configure(childFixtures, fixtureContents.Last().Fixtures, EnsureFilter(filter, fixture), dispatcher);
                }
            }
        }

        private IEnumerable<IFixture> RetrieveChildFixtures(IFixture fixture)
            => (fixture as FixtureContainer)?.GetType().GetProperty("Fixtures", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(fixture) as IEnumerable<IFixture>;

        private IFixtureFilter EnsureFilter(IFixtureFilter filter, IFixture fixture)
            => filter == null ? filter : filter.Accept(fixture.FixtureDescriptor) ? null : filter;

        private async Task ConfigureSummary(IFixture fixture, CoreDispatcher dispatcher)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ++Host.Summary.TotalCount;
            });

            fixture.FixtureRun += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    switch (e.Result.Status)
                    {
                        case FixtureStatus.Passed: ++Host.Summary.PassedCount; break;
                        case FixtureStatus.Failed: ++Host.Summary.FailedCount; break;
                        case FixtureStatus.Pending: ++Host.Summary.PendingCount; break;
                    }
                });
            };
        }

        private FixtureContent CreateFixtureContent(IFixture fixture, CoreDispatcher dispatcher)
        {
            var fixtureContent = new FixtureContent
            {
                Description = Host.Formatter.FormatFixture(fixture.FixtureDescriptor).ToString()
            };

            fixture.FixtureRunning += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    fixtureContent.Status = FixtureStatus.Running
                );
            };
            fixture.FixtureRun += async (s, e) =>
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    fixtureContent.OnFixtureRunningCompleted(e.Result, Host.Formatter);
                    e.Result.StepResults.Aggregate(fixtureContent.Steps, (steps, stepResult) =>
                    {
                        steps.Add(new FixtureStepContent(stepResult, Host.Formatter));
                        return steps;
                    });
                });
            };

            return fixtureContent;
        }

        private void SetChildOpenCondition(IEnumerable<FixtureContent> fixtureContents)
        {
            var limit = 50;

            if (Host.Summary.TotalCount <= limit)
            {
                SetChildOpen(fixtureContents, true);
                return;
            }

            var assemblyFixtureContents = fixtureContents.ToList();
            if (assemblyFixtureContents.Count() > limit) return;

            SetChildOpen(assemblyFixtureContents);
            limit -= assemblyFixtureContents.Count();

            var namespaceFixtureContents = assemblyFixtureContents.SelectMany(fixtureContent => fixtureContent.Fixtures).ToList();
            if (namespaceFixtureContents.Count() > limit) return;

            SetChildOpen(namespaceFixtureContents);
        }

        private void SetChildOpen(IEnumerable<FixtureContent> fixtureContents, bool recursive = false)
        {
            foreach (var fixtureContent in fixtureContents)
            {
                fixtureContent.IsChildOpen = true;
                if (recursive) SetChildOpen(fixtureContent.Fixtures, true);
            }
        }
    }
}
