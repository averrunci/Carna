// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;
using Carna.Runner.Formatters;
using Carna.WinUIRunner.Converters;
using Microsoft.UI.Dispatching;
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
    [Obsolete("This method is obsolete. Use the InvokeAsync method instead if you want to run a task on the thread associated with the DispatcherQueue asynchronously.")]
    public static Window Window => window ?? throw new InvalidOperationException("The window instance is not instantiated.");
    private static Window? window;

    /// <summary>
    /// Invokes the specified task on the thread associated with the <see cref="DispatcherQueue"/> asynchronously.
    /// </summary>
    /// <param name="action">The delegate to the task to invoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task InvokeAsync(DispatcherQueueHandler action) => Window.DispatcherQueue.RunAsync(action);

    /// <summary>
    /// Invokes the specified task on the thread associated with the <see cref="DispatcherQueue"/>
    /// with the specified priority asynchronously.
    /// </summary>
    /// <param name="priority">The priority of the task (such as Low, Normal, or High).</param>
    /// <param name="action">The delegate to the task to invoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task InvokeAsync(DispatcherQueuePriority priority, DispatcherQueueHandler action) => Window.DispatcherQueue.RunAsync(priority, action);

    /// <summary>
    /// Invokes the specified task on the thread associated with the <see cref="DispatcherQueue"/> asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
    /// <param name="action">The delegate to the task to invoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task<TResult> InvokeAsync<TResult>(Func<TResult> action) => Window.DispatcherQueue.RunAsync(action);

    /// <summary>
    /// Invokes the specified task on the thread associated with the <see cref="DispatcherQueue"/>
    /// with the specified priority asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
    /// <param name="priority">The priority of the task (such as Low, Normal, or High).</param>
    /// <param name="action">The delegate to the task to invoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task<TResult> InvokeAsync<TResult>(DispatcherQueuePriority priority, Func<TResult> action) => Window.DispatcherQueue.RunAsync(priority, action);

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