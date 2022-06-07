// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.UI.Dispatching;

namespace Carna.WinUIRunner;

/// <summary>
/// Provides some utility extensions on <see cref="DispatcherQueue"/>.
/// </summary>
public static class DispatcherQueueExtensions
{
    /// <summary>
    /// Runs the specified task on the thread associated with the <see cref="DispatcherQueue"/> asynchronously.
    /// </summary>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> with which the thread is associated.</param>
    /// <param name="action">The delegate to the task to execute.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task RunAsync(this DispatcherQueue dispatcher, DispatcherQueueHandler action) => RunAsync(dispatcher, DispatcherQueuePriority.Normal, action);

    /// <summary>
    /// Runs the specified task on the thread associated with the <see cref="DispatcherQueue"/>
    /// with the specified priority asynchronously.
    /// </summary>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> with which the thread is associated.</param>
    /// <param name="priority">The priority of the task (such as Low, Normal, or High).</param>
    /// <param name="action">The delegate to the task to execute.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task RunAsync(this DispatcherQueue dispatcher, DispatcherQueuePriority priority, DispatcherQueueHandler action)
    {
        if (dispatcher.HasThreadAccess) return PerformDispatcherQueueAction(action);

        var taskCompletionSource = new TaskCompletionSource();

        if (!dispatcher.TryEnqueue(priority, () => PerformDispatcherQueueAction(taskCompletionSource, action)))
        {
            taskCompletionSource.SetException(new InvalidOperationException("Failed to enqueue the task to execute."));
        }

        return taskCompletionSource.Task;
    }

    private static Task PerformDispatcherQueueAction(DispatcherQueueHandler action)
    {
        try
        {
            action();
            return Task.CompletedTask;
        }
        catch (Exception exc)
        {
            return Task.FromException(exc);
        }
    }

    private static void PerformDispatcherQueueAction(TaskCompletionSource taskCompletionSource, DispatcherQueueHandler action)
    {
        try
        {
            action();
            taskCompletionSource.SetResult();
        }
        catch (Exception exc)
        {
            taskCompletionSource.SetException(exc);
        }
    }

    /// <summary>
    /// Runs the specified task on the thread associated with the <see cref="DispatcherQueue"/> asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> with which the thread is associated.</param>
    /// <param name="action">The delegate to the task to execute.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task<TResult> RunAsync<TResult>(this DispatcherQueue dispatcher, Func<TResult> action) => RunAsync(dispatcher, DispatcherQueuePriority.Normal, action);

    /// <summary>
    /// Runs the specified task on the thread associated with the <see cref="DispatcherQueue"/>
    /// with the specified priority asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> with which the thread is associated.</param>
    /// <param name="priority">The priority of the task (such as Low, Normal, or High).</param>
    /// <param name="action">The delegate to the task to execute.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task<TResult> RunAsync<TResult>(this DispatcherQueue dispatcher, DispatcherQueuePriority priority, Func<TResult> action)
    {
        if (dispatcher.HasThreadAccess) return PerformDispatcherQueueAction(action);

        var taskCompletionSource = new TaskCompletionSource<TResult>();

        if (!dispatcher.TryEnqueue(priority, () => PerformDispatcherQueueAction(taskCompletionSource, action)))
        {
            taskCompletionSource.SetException(new InvalidOperationException("Failed to enqueue the task to execute."));
        }

        return taskCompletionSource.Task;
    }

    private static Task<TResult> PerformDispatcherQueueAction<TResult>(Func<TResult> action)
    {
        try
        {
            return Task.FromResult(action());
        }
        catch (Exception exc)
        {
            return Task.FromException<TResult>(exc);
        }
    }

    private static void PerformDispatcherQueueAction<TResult>(TaskCompletionSource<TResult> taskCompletionSource, Func<TResult> action)
    {
        try
        {
            taskCompletionSource.SetResult(action());
        }
        catch (Exception exc)
        {
            taskCompletionSource.SetException(exc);
        }
    }
}