// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;
using Carna.Runner.Formatters;
using Carna.WinUIRunner.Converters;
using Microsoft.UI.Xaml;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a Win UI runner of Carna.
/// </summary>
public static class CarnaWinUIRunner
{
    /// <summary>
    /// Gets the currently activated window for the CarnaWinUIRunner.
    /// </summary>
    public static Window Window => window ?? throw new InvalidOperationException("The window instance is not instantiated.");
    private static Window? window;

    /// <summary>
    /// Runs fixtures.
    /// </summary>
    public static void Run() => Run(new FixtureFormatter());

    /// <summary>
    /// Runs fixtures with the specified formatter to format a description of a fixture.
    /// </summary>
    /// <param name="formatter">The formatter to format a description of a fixture.</param>
    public static void Run(IFixtureFormatter formatter)
    {
        RegisterResources();
        ActivateWindow(formatter);
    }

    private static void RegisterResources()
    {
        Application.Current.Resources["BooleanToVisibilityConverter"] = new BooleanToVisibilityConverter();
        Application.Current.Resources["BooleanToChildOpenStringRepresentationConverter"] = new BooleanToChildOpenStringRepresentationConverter();
        Application.Current.Resources["ExceptionToVisibilityConverter"] = new ExceptionToVisibilityConverter();
        Application.Current.Resources["FixtureStatusToBrushConverter"] = new FixtureStatusToBrushConverter();
        Application.Current.Resources["FixtureStepStatusToBrushConverter"] = new FixtureStepStatusToBrushConverter();
    }

    private static void ActivateWindow(IFixtureFormatter formatter)
    {
        window = new Window
        {
            ExtendsContentIntoTitleBar = true,
        };
        window.Content = new CarnaWinUIRunnerHostView
        {
            DataContext = new CarnaWinUIRunnerHost(formatter)
        };
        window.Activate();
    }
}