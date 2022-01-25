// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Step;

/// <summary>
/// Represents a step that indicates a When.
/// </summary>
public class WhenStep : FixtureStep
{
    /// <summary>
    /// Gets an action.
    /// </summary>
    public Action? Action { get; }

    /// <summary>
    /// Gets an asynchronous action.
    /// </summary>
    public Func<Task>? AsyncAction { get; }

    /// <summary>
    /// Gets a time-out value for running a step.
    /// </summary>
    public TimeSpan? Timeout { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, caller type, method name, full path
    /// of the source file, and line number in the source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, action, caller type, method name,
    /// full path of the source file, and line number in the source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="action">The action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        Action = action;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, a time-out, action, caller type,
    /// method name, full path of the source file, and line number in the
    /// source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="millisecondsTimeout">The time-out value in milliseconds for running a When step.</param>
    /// <param name="action">The action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, double millisecondsTimeout, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : this(description, TimeSpan.FromMilliseconds(millisecondsTimeout), action, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, a time-out, action, caller type,
    /// method name, full path of the source file, and line number in the
    /// source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="timeout">The time-out value for running a When step.</param>
    /// <param name="action">The action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, TimeSpan timeout, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        Timeout = timeout;
        Action = action;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, asynchronous action, caller type,
    /// method name, full path of the source file, and line number in the
    /// source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="asyncAction">The asynchronous action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        AsyncAction = asyncAction;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, a time-out, asynchronous action,
    /// caller type, method name, full path of the source file, and
    /// line number in the source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="millisecondsTimeout">The time-out value in milliseconds for running a When step.</param>
    /// <param name="asyncAction">The asynchronous action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, double millisecondsTimeout, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : this(description, TimeSpan.FromMilliseconds(millisecondsTimeout), asyncAction, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhenStep"/> class
    /// with the specified description, a time-out, asynchronous action,
    /// caller type, method name, full path of the source file, and
    /// line number in the source file.
    /// </summary>
    /// <param name="description">The description of a When step.</param>
    /// <param name="timeout">The time-out value for running a When step.</param>
    /// <param name="asyncAction">The asynchronous action.</param>
    /// <param name="callerType">The caller type.</param>
    /// <param name="callerMemberName">
    /// The method name of the caller to the method.
    /// </param>
    /// <param name="callerFilePath">
    /// The full path of the source file that contains the caller.
    /// </param>
    /// <param name="callerLineNumber">
    /// The line number in the source file at which the method is called.
    /// </param>
    public WhenStep(string description, TimeSpan timeout, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        Timeout = timeout;
        AsyncAction = asyncAction;
    }
}