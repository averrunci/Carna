﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;

namespace Carna.Step;

/// <summary>
/// Represents a step that indicates an Expect.
/// </summary>
public class ExpectStep : FixtureStep
{
    /// <summary>
    /// Gets an assertion that returns a boolean value.
    /// </summary>
    public Expression<Func<bool>>? Assertion { get; }

    /// <summary>
    /// Gets an action that throws an exception if an assertion is failed.
    /// </summary>
    public Action? Action { get; }

    /// <summary>
    /// Gets an asynchronous action that throws an exception if an assertion is failed.
    /// </summary>
    public Func<Task>? AsyncAction { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectStep"/> class
    /// with the specified description, caller type, method name, full path
    /// of the source file, and line number in the source file.
    /// </summary>
    /// <param name="description">The description of an Expect step.</param>
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
    public ExpectStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectStep"/> class
    /// with the specified description, assertion that returns a boolean value,
    /// caller type, method name, full path of the source file, and line number
    /// in the source file.
    /// </summary>
    /// <param name="description">The description of an Expect step.</param>
    /// <param name="assertion">The assertion that returns a boolean value.</param>
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
    public ExpectStep(string description, Expression<Func<bool>> assertion, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        Assertion = assertion;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectStep"/> class
    /// with the specified description, action that throws an exception
    /// if an assertion is failed, caller type, method name, full path of
    /// the source file, and line number in the source file.
    /// </summary>
    /// <param name="description">The description of an Expect step.</param>
    /// <param name="action">
    /// The action that throws an exception if an assertion is failed.
    /// </param>
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
    public ExpectStep(string description, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        Action = action;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectStep"/> class
    /// with the specified description, asynchronous action that throws
    /// an exception if an assertion is failed, caller type, method name,
    /// full path of the source file, and line number in the source file.
    /// </summary>
    /// <param name="description">The description of an Expect step.</param>
    /// <param name="asyncAction">
    /// The asynchronous action that throws an exception if an assertion is failed.
    /// </param>
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
    public ExpectStep(string description, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
    {
        AsyncAction = asyncAction;
    }
}