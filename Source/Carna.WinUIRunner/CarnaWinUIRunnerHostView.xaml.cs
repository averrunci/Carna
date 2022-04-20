// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Carna.Runner;
using Carna.Runner.Step;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a view for <see cref="CarnaWinUIRunnerHost"/>.
/// </summary>
public sealed partial class CarnaWinUIRunnerHostView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarnaWinUIRunnerHostView"/> class.
    /// </summary>
    public CarnaWinUIRunnerHostView()
    {
        InitializeComponent();

        Application.Current.UnhandledException += OnUnhandledException;

        CarnaWinUIRunner.Window.SetTitleBar(TitleGrid);
    }

    private void OnUnhandledException(object? sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        AppendErrorMessage(e.Exception.ToString());
        e.Handled = true;
    }

    private void AppendErrorMessage(string message)
    {
        if (DataContext is not CarnaWinUIRunnerHost host) return;

        host.ErrorMessage += $"{(string.IsNullOrEmpty(host.ErrorMessage) ? string.Empty : Environment.NewLine + Environment.NewLine)}{message}";
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e) => AdjustFixtureContentMaxWidth(e.NewSize.Width);
    private void AdjustFixtureContentMaxWidth(double width)
    {
        if (FixtureItemsControl.ItemsPanelRoot is null) return;

        foreach (var child in FixtureItemsControl.ItemsPanelRoot.Children.OfType<FrameworkElement>())
        {
            child.MaxWidth = width - 52;
        }
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CarnaWinUIRunnerHost host) return;

        try
        {
            if (sender is not FrameworkElement element) return;

            var engine = new FixtureEngine().LoadConfiguration(host);

            host.Summary.OnFixtureBuildingStarting();
            var fixtures = await BuildFixtures(host, engine, element.DispatcherQueue);
            host.Summary.OnFixtureBuildingCompleted(DateTime.UtcNow);

            AdjustFixtureContentMaxWidth(element.ActualWidth);

            host.Summary.OnFixtureRunningStarting();
            var results = await Task.Run(() => engine.RunFixtures(fixtures));
            host.Summary.OnFixtureRunningCompleted(results);

            OpenFailedFixtureContents(host.Fixtures);
            MarkFirstFailedFixtureContent(host.Fixtures);
        }
        catch (Exception exc)
        {
            AppendErrorMessage(exc.ToString());
        }
        finally
        {
            if (host.AutoExit)
            {
                Application.Current.Exit();
            }
        }
    }

    private async Task<List<IFixture>> BuildFixtures(CarnaWinUIRunnerHost host, FixtureEngine engine, DispatcherQueue dispatcher)
    {
        var fixtures = await Task.Run(() => engine.BuildFixtures().ToList());
        var fixtureContents = host.Fixtures;
        await Task.Run(() => Configure(host, fixtures, fixtureContents, engine.Filter, dispatcher));
        SetChildOpenCondition(host.Summary, host.Fixtures);
        return fixtures;
    }

    private void Configure(CarnaWinUIRunnerHost host, IEnumerable<IFixture> fixtures, IList<FixtureContent> fixtureContents, IFixtureFilter? filter, DispatcherQueue dispatcher)
    {
        foreach (var fixture in fixtures)
        {
            if (!fixture.CanRun(filter)) continue;

            var fixtureContent = CreateFixtureContent(host, fixture, dispatcher);
            dispatcher.TryEnqueue(DispatcherQueuePriority.Normal, () => fixtureContents.Add(fixtureContent));

            var childFixtures = RetrieveChildFixtures(fixture).ToList();
            if (childFixtures.Any())
            {
                Configure(host, childFixtures, fixtureContent.Fixtures, EnsureFilter(filter, fixture), dispatcher);
            }
            else
            {
                ConfigureSummary(host, fixture, dispatcher);
            }
        }
    }

    private IEnumerable<IFixture> RetrieveChildFixtures(IFixture fixture)
        => (fixture as FixtureContainer)?.GetType().GetProperty("Fixtures", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(fixture) as IEnumerable<IFixture> ?? Enumerable.Empty<IFixture>();

    private IFixtureFilter? EnsureFilter(IFixtureFilter? filter, IFixture fixture)
        => filter?.Accept(fixture.FixtureDescriptor) ?? true ? null : filter;

    private void ConfigureSummary(CarnaWinUIRunnerHost host, IFixture fixture, DispatcherQueue dispatcher)
    {
        dispatcher.TryEnqueue(DispatcherQueuePriority.Normal, () => ++host.Summary.TotalCount);

        fixture.FixtureRun += (_, e) => dispatcher.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            switch (e.Result.Status)
            {
                case FixtureStatus.Passed: ++host.Summary.PassedCount; break;
                case FixtureStatus.Failed: ++host.Summary.FailedCount; break;
                case FixtureStatus.Pending: ++host.Summary.PendingCount; break;
            }
        });
    }

    private FixtureContent CreateFixtureContent(CarnaWinUIRunnerHost host, IFixture fixture, DispatcherQueue dispatcher)
    {
        var fixtureContent = new FixtureContent
        {
            Description = host.Formatter.FormatFixture(fixture.FixtureDescriptor).ToString()
        };

        fixture.FixtureRunning += (_, _) => dispatcher.TryEnqueue(DispatcherQueuePriority.Normal, () => fixtureContent.Status = FixtureStatus.Running);
        fixture.FixtureRun += (_, e) => dispatcher.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            fixtureContent.OnFixtureRunningCompleted(e.Result, host.Formatter);
            foreach (var stepContent in e.Result.StepResults.Select(stepResult => new FixtureStepContent(stepResult, host.Formatter)))
            {
                fixtureContent.Steps.Add(stepContent);
            }
        });

        return fixtureContent;
    }

    private void SetChildOpenCondition(FixtureSummary summary, IEnumerable<FixtureContent> fixtureContents)
    {
        var limit = 50;

        if (summary.TotalCount <= limit)
        {
            SetChildOpen(fixtureContents, true);
            return;
        }

        var assemblyFixtureContents = fixtureContents.ToList();
        if (GetChildFixtureCount(assemblyFixtureContents) > limit) return;

        SetChildOpen(assemblyFixtureContents);
        limit -= assemblyFixtureContents.Count;

        var namespaceFixtureContents = assemblyFixtureContents.SelectMany(fixtureContent => fixtureContent.Fixtures).ToList();
        if (GetChildFixtureCount(namespaceFixtureContents) > limit) return;

        SetChildOpen(namespaceFixtureContents);
    }

    private int GetChildFixtureCount(IEnumerable<FixtureContent> fixtureContents) => fixtureContents.SelectMany(fixtureContent => fixtureContent.Fixtures).Count();

    private void SetChildOpen(IEnumerable<FixtureContent> fixtureContents, bool recursive = false)
    {
        foreach (var fixtureContent in fixtureContents)
        {
            fixtureContent.IsChildOpen = true;
            if (recursive) SetChildOpen(fixtureContent.Fixtures, true);
        }
    }

    private void OpenFailedFixtureContents(IEnumerable<FixtureContent> fixtureContents)
    {
        foreach (var fixtureContent in fixtureContents)
        {
            if (fixtureContent.Status is not FixtureStatus.Failed) continue;

            fixtureContent.IsChildOpen = true;
            OpenFailedFixtureContents(fixtureContent.Fixtures);
        }
    }

    private void MarkFirstFailedFixtureContent(IEnumerable<FixtureContent> fixtureContents)
    {
        foreach (var fixtureContent in fixtureContents)
        {
            if (fixtureContent.Status is not FixtureStatus.Failed) continue;

            var stepFailed = fixtureContent.Steps.FirstOrDefault(step => step.Status is FixtureStepStatus.Failed);
            if (stepFailed is not null)
            {
                stepFailed.IsFirstFailed = true;
                return;
            }

            if (!fixtureContent.Fixtures.Any())
            {
                fixtureContent.IsFirstFailed = true;
                return;
            }

            MarkFirstFailedFixtureContent(fixtureContent.Fixtures);
        }
    }
}