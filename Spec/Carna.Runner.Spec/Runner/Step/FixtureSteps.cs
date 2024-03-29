﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;
using Carna.Step;

namespace Carna.Runner.Step;

internal static class FixtureSteps
{
    public static GivenStep CreateGivenStep() => CreateStep<GivenStep>();
    public static GivenStep CreateGivenStep(string description) => CreateStep<GivenStep>(description);
    public static GivenStep CreateGivenStep(Action arrangement) => CreateStep<GivenStep>(arrangement);
    public static GivenStep CreateGivenStep(Func<Task> asyncArrangement) => CreateStep<GivenStep>(asyncArrangement);

    public static WhenStep CreateWhenStep() => CreateStep<WhenStep>();
    public static WhenStep CreateWhenStep(string description) => CreateStep<WhenStep>(description);
    public static WhenStep CreateWhenStep(Action action) => CreateStep<WhenStep>(action);
    public static WhenStep CreateWhenStep(Func<Task> asyncAction) => CreateStep<WhenStep>(asyncAction);
    public static WhenStep CreateWhenStep(double millisecondsTimeout, Action action) => new(string.Empty, millisecondsTimeout, action, typeof(FixtureSteps), string.Empty, string.Empty, 0);
    public static WhenStep CreateWhenStep(TimeSpan timeout, Action action) => new(string.Empty, timeout, action, typeof(FixtureSteps), string.Empty, string.Empty, 0);
    public static WhenStep CreateWhenStep(double millisecondsTimeout, Func<Task> asyncAction) => new(string.Empty, millisecondsTimeout, asyncAction, typeof(FixtureSteps), string.Empty, string.Empty, 0);
    public static WhenStep CreateWhenStep(TimeSpan timeout, Func<Task> asyncAction) => new(string.Empty, timeout, asyncAction, typeof(FixtureSteps), string.Empty, string.Empty, 0);

    public static ThenStep CreateThenStep() => CreateStep<ThenStep>();
    public static ThenStep CreateThenStep(string description) => CreateStep<ThenStep>(description);
    public static ThenStep CreateThenStep(Expression<Func<bool>> assertion) => CreateStep<ThenStep>(assertion);
    public static ThenStep CreateThenStep(Action assertion) => CreateStep<ThenStep>(assertion);
    public static ThenStep CreateThenStep(Expression<Func<Exception, bool>> assertion) => CreateStep<ThenStep>(assertion);
    public static ThenStep CreateThenStep(Action<Exception> assertion) => CreateStep<ThenStep>(assertion);
    public static ThenStep CreateThenStep(Func<Task> asyncAssertion) => CreateStep<ThenStep>(asyncAssertion);
    public static ThenStep CreateThenStep(Func<Exception, Task> asyncAssertion) => CreateStep<ThenStep>(asyncAssertion);
    public static ThenStep CreateThenStep<T>() where T : Exception => CreateStep<ThenStep, T>();
    public static ThenStep CreateThenStep<T>(string description) where T : Exception => CreateStep<ThenStep, T>(description);
    public static ThenStep CreateThenStep<T>(Expression<Func<T, bool>> assertion) where T : Exception
    {
        var parameter = Expression.Parameter(typeof(Exception));
        Expression<Func<Exception, T>> convert = exc => (T)exc;
        return CreateStep<ThenStep, T>(Expression.Lambda<Func<Exception, bool>>(Expression.Invoke(assertion, Expression.Invoke(convert, parameter)), parameter));
    }
    public static ThenStep CreateThenStep<T>(Action<T> assertion) where T : Exception => CreateStep<ThenStep, T>(exc => assertion((T)exc));
    public static ThenStep CreateThenStep<T>(Func<T, Task> asyncAssertion) where T : Exception => CreateStep<ThenStep, T>(exc => asyncAssertion((T)exc));

    public static ExpectStep CreateExpectStep() => CreateStep<ExpectStep>();
    public static ExpectStep CreateExpectStep(string description) => CreateStep<ExpectStep>(description);
    public static ExpectStep CreateExpectStep(Expression<Func<bool>> assertion) => CreateStep<ExpectStep>(assertion);
    public static ExpectStep CreateExpectStep(Action assertion) => CreateStep<ExpectStep>(assertion);
    public static ExpectStep CreateExpectStep(Func<Task> asyncAssertion) => CreateStep<ExpectStep>(asyncAssertion);

    public static NoteStep CreateNoteStep() => CreateStep<NoteStep>();
    public static NoteStep CreateNoteStep(string description) => CreateStep<NoteStep>(description);

    public static T CreateStep<T>() where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(string description) where T : FixtureStep => Activator.CreateInstance(typeof(T), description, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Expression<Func<bool>> assertion) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, assertion, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Action action) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, action, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Expression<Func<Exception, bool>> assertion) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, assertion, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Action<Exception> action) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, action, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Func<Task> asyncAction) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, asyncAction, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T>(Func<Exception, Task> asyncAction) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, asyncAction, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>() where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), string.Empty, typeof(TException), null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>(string description) where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), description, typeof(TException), null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>(Expression<Func<Exception, bool>> assertion) where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), string.Empty, typeof(TException), assertion, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>(Action<Exception> action) where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), string.Empty, typeof(TException), action, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>(Func<Task> asyncAction) where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), string.Empty, typeof(TException), asyncAction, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
    public static T CreateStep<T, TException>(Func<Exception, Task> asyncAction) where T : FixtureStep where TException : Exception => Activator.CreateInstance(typeof(T), string.Empty, typeof(TException), asyncAction, null, string.Empty, string.Empty, 0) as T ?? throw new InvalidOperationException();
}