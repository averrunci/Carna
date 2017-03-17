﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
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

        public static ThenStep CreateThenStep() => CreateStep<ThenStep>();
        public static ThenStep CreateThenStep(string description) => CreateStep<ThenStep>(description);
        public static ThenStep CreateThenStep(Expression<Func<bool>> assertion) => CreateStep<ThenStep>(assertion);
        public static ThenStep CreateThenStep(Action assertion) => CreateStep<ThenStep>(assertion);
        public static ThenStep CreateThenStep(Expression<Func<Exception, bool>> assertion) => CreateStep<ThenStep>(assertion);
        public static ThenStep CreateThenStep(Action<Exception> assertion) => CreateStep<ThenStep>(assertion);
        public static ThenStep CreateThenStep(Func<Task> asyncAssertion) => CreateStep<ThenStep>(asyncAssertion);
        public static ThenStep CreateThenStep(Func<Exception, Task> asyncAssertion) => CreateStep<ThenStep>(asyncAssertion);

        public static ExpectStep CreateExpectStep() => CreateStep<ExpectStep>();
        public static ExpectStep CreateExpectStep(string description) => CreateStep<ExpectStep>(description);
        public static ExpectStep CreateExpectStep(Expression<Func<bool>> assertion) => CreateStep<ExpectStep>(assertion);
        public static ExpectStep CreateExpectStep(Action assertion) => CreateStep<ExpectStep>(assertion);
        public static ExpectStep CreateExpectStep(Func<Task> asyncAssertion) => CreateStep<ExpectStep>(asyncAssertion);

        public static NoteStep CreateNoteStep() => CreateStep<NoteStep>();
        public static NoteStep CreateNoteStep(string description) => CreateStep<NoteStep>(description);

        public static T CreateStep<T>() where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(string description) where T : FixtureStep => Activator.CreateInstance(typeof(T), description, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Expression<Func<bool>> assertion) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, assertion, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Action action) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, action, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Expression<Func<Exception, bool>> assertion) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, assertion, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Action<Exception> action) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, action, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Func<Task> asyncAction) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, asyncAction, null, string.Empty, string.Empty, 0) as T;
        public static T CreateStep<T>(Func<Exception, Task> asyncAction) where T : FixtureStep => Activator.CreateInstance(typeof(T), string.Empty, asyncAction, null, string.Empty, string.Empty, 0) as T;
    }
}
