// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;
using Carna.Assertions;

namespace Carna;

internal class StepAssertions
{
    public class ExpectStep : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        Expression<Func<bool>>? Assertion { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }

        protected ExpectStep(string description, Action? action, Expression<Func<bool>>? assertion, Func<Task>? asyncAction)
        {
            Description = description;
            Action = action;
            Assertion = assertion;
            AsyncAction = asyncAction;
        }

        public static ExpectStep Of(string description) => new(description, null, null, null);
        public static ExpectStep Of(string description, Action action) => new(description, action, null, null);
        public static ExpectStep Of(string description, Expression<Func<bool>> assertion) => new(description, null, assertion, null);
        public static ExpectStep Of(string description, Func<Task> asyncAction) => new(description, null, null, asyncAction);
        public static ExpectStep Of(Step.ExpectStep step) => new(step.Description, step.Action, step.Assertion, step.AsyncAction);
    }

    public class GivenStep : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Arrangement { get; }
        [AssertionProperty]
        Func<Task>? AsyncArrangement { get; }

        public GivenStep(string description, Action? arrangement, Func<Task>? asyncArrangement)
        {
            Description = description;
            Arrangement = arrangement;
            AsyncArrangement = asyncArrangement;
        }

        public static GivenStep Of(string description) => new(description, null,  null);
        public static GivenStep Of(string description, Action arrangement) => new(description, arrangement, null);
        public static GivenStep Of(string description, Func<Task> asyncArrangement) => new(description, null, asyncArrangement);
        public static GivenStep Of(Step.GivenStep step) => new(step.Description, step.Arrangement, step.AsyncArrangement);
    }

    public class WhenStep : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }

        public WhenStep(string description, Action? action, Func<Task>? asyncAction)
        {
            Description = description;
            Action = action;
            AsyncAction = asyncAction;
        }

        public static WhenStep Of(string description) => new(description, null, null);
        public static WhenStep Of(string description, Action action) => new(description, action, null);
        public static WhenStep Of(string description, Func<Task> asyncAction) => new(description, null, asyncAction);
        public static WhenStep Of(Step.WhenStep step) => new(step.Description, step.Action, step.AsyncAction);
    }

    public class ThenStep : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        Action<Exception>? ExceptionAction { get; }
        [AssertionProperty]
        Expression<Func<bool>>? Assertion { get; }
        [AssertionProperty]
        Expression<Func<Exception, bool>>? ExceptionAssertion { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }
        [AssertionProperty]
        Func<Exception, Task>? AsyncExceptionAction { get; }
        [AssertionProperty]
        Type? ExceptionType { get; }

        public ThenStep(string description, Action? action, Action<Exception>? exceptionAction, Expression<Func<bool>>? assertion, Expression<Func<Exception, bool>>? exceptionAssertion, Func<Task>? asyncAction, Func<Exception, Task>? asyncExceptionAction, Type? exceptionType)
        {
            Description = description;
            Action = action;
            ExceptionAction = exceptionAction;
            Assertion = assertion;
            ExceptionAssertion = exceptionAssertion;
            AsyncAction = asyncAction;
            AsyncExceptionAction = asyncExceptionAction;
            ExceptionType = exceptionType;
        }

        public static ThenStep Of(string description) => new(description, null, null, null, null, null, null, null);
        public static ThenStep Of(string description, Action action) => new(description, action, null, null, null, null, null, null);
        public static ThenStep Of(string description, Action<Exception> exceptionAction) => new(description, null, exceptionAction, null, null, null, null, null);
        public static ThenStep Of(string description, Expression<Func<bool>> assertion) => new(description, null, null, assertion, null, null, null, null);
        public static ThenStep Of(string description, Expression<Func<Exception, bool>> exceptionAssertion) => new(description, null, null, null, exceptionAssertion, null, null, null);
        public static ThenStep Of(string description, Func<Task> asyncAction) => new(description, null, null, null, null, asyncAction, null, null);
        public static ThenStep Of(string description, Func<Exception, Task> asyncExceptionAction) => new(description, null, null, null, null, null, asyncExceptionAction, null);
        public static ThenStep Of(string description, Type exceptionType) => new(description, null, null, null, null, null, null, exceptionType);
        public static ThenStep Of(string description, Action<Exception> exceptionAction, Type exceptionType) => new(description, null, exceptionAction, null, null, null, null, exceptionType);
        public static ThenStep Of(Step.ThenStep step) => new(step.Description, step.Action, step.ExceptionAction, step.Assertion, step.ExceptionAssertion, step.AsyncAction, step.AsyncExceptionAction, step.ExceptionType);
    }

    public class ThenStepExceptionAction : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        NotEqualAssertionProperty<Action<Exception>?> ExceptionAction { get; }
        [AssertionProperty]
        Expression<Func<bool>>? Assertion { get; }
        [AssertionProperty]
        Expression<Func<Exception, bool>>? ExceptionAssertion { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }
        [AssertionProperty]
        Func<Exception, Task>? AsyncExceptionAction { get; }
        [AssertionProperty]
        Type? ExceptionType { get; }

        public ThenStepExceptionAction(string description, Action? action, Action<Exception>? exceptionAction, Expression<Func<bool>>? assertion, Expression<Func<Exception, bool>>? exceptionAssertion, Func<Task>? asyncAction, Func<Exception, Task>? asyncExceptionAction, Type? exceptionType)
        {
            Description = description;
            Action = action;
            ExceptionAction = new NotEqualAssertionProperty<Action<Exception>?>(exceptionAction);
            Assertion = assertion;
            ExceptionAssertion = exceptionAssertion;
            AsyncAction = asyncAction;
            AsyncExceptionAction = asyncExceptionAction;
            ExceptionType = exceptionType;
        }

        public static ThenStepExceptionAction Of(string description, Type exceptionType) => new(description, null, null, null, null, null, null, exceptionType);
        public static ThenStepExceptionAction Of(Step.ThenStep step) => new(step.Description, step.Action, step.ExceptionAction, step.Assertion, step.ExceptionAssertion, step.AsyncAction, step.AsyncExceptionAction, step.ExceptionType);
    }

    public class ThenStepExceptionAssertion : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        Action<Exception>? ExceptionAction { get; }
        [AssertionProperty]
        Expression<Func<bool>>? Assertion { get; }
        [AssertionProperty]
        NotEqualAssertionProperty<Expression<Func<Exception, bool>>?> ExceptionAssertion { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }
        [AssertionProperty]
        Func<Exception, Task>? AsyncExceptionAction { get; }
        [AssertionProperty]
        Type? ExceptionType { get; }

        public ThenStepExceptionAssertion(string description, Action? action, Action<Exception>? exceptionAction, Expression<Func<bool>>? assertion, Expression<Func<Exception, bool>>? exceptionAssertion, Func<Task>? asyncAction, Func<Exception, Task>? asyncExceptionAction, Type? exceptionType)
        {
            Description = description;
            Action = action;
            ExceptionAction = exceptionAction;
            Assertion = assertion;
            ExceptionAssertion = new NotEqualAssertionProperty<Expression<Func<Exception, bool>>?>(exceptionAssertion);
            AsyncAction = asyncAction;
            AsyncExceptionAction = asyncExceptionAction;
            ExceptionType = exceptionType;
        }

        public static ThenStepExceptionAssertion Of(string description, Type exceptionType) => new(description, null, null, null, null, null, null, exceptionType);
        public static ThenStepExceptionAssertion Of(Step.ThenStep step) => new(step.Description, step.Action, step.ExceptionAction, step.Assertion, step.ExceptionAssertion, step.AsyncAction, step.AsyncExceptionAction, step.ExceptionType);
    }

    public class ThenStepAsyncExceptionAction : AssertionObject
    {
        [AssertionProperty]
        string Description { get; }
        [AssertionProperty]
        Action? Action { get; }
        [AssertionProperty]
        Action<Exception>? ExceptionAction { get; }
        [AssertionProperty]
        Expression<Func<bool>>? Assertion { get; }
        [AssertionProperty]
        Expression<Func<Exception, bool>>? ExceptionAssertion { get; }
        [AssertionProperty]
        Func<Task>? AsyncAction { get; }
        [AssertionProperty]
        NotEqualAssertionProperty<Func<Exception, Task>?> AsyncExceptionAction { get; }
        [AssertionProperty]
        Type? ExceptionType { get; }

        public ThenStepAsyncExceptionAction(string description, Action? action, Action<Exception>? exceptionAction, Expression<Func<bool>>? assertion, Expression<Func<Exception, bool>>? exceptionAssertion, Func<Task>? asyncAction, Func<Exception, Task>? asyncExceptionAction, Type? exceptionType)
        {
            Description = description;
            Action = action;
            ExceptionAction = exceptionAction;
            Assertion = assertion;
            ExceptionAssertion = exceptionAssertion;
            AsyncAction = asyncAction;
            AsyncExceptionAction = new NotEqualAssertionProperty<Func<Exception, Task>?>(asyncExceptionAction);
            ExceptionType = exceptionType;
        }

        public static ThenStepAsyncExceptionAction Of(string description, Type exceptionType) => new(description, null, null, null, null, null, null, exceptionType);
        public static ThenStepAsyncExceptionAction Of(Step.ThenStep step) => new(step.Description, step.Action, step.ExceptionAction, step.Assertion, step.ExceptionAssertion, step.AsyncAction, step.AsyncExceptionAction, step.ExceptionType);
    }
}