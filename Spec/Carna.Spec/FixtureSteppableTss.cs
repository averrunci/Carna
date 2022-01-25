// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;
using Carna.Step;

namespace Carna;

class FixtureSteppableTss : FixtureSteppable
{
    public FixtureSteppableTss(IFixtureStepper stepper) => ((IFixtureSteppable)this).Stepper = stepper;

    public void RunExpect(string description) => Expect(description);
    public void RunExpect(string description, Expression<Func<bool>> assertion) => Expect(description, assertion);
    public void RunExpect(string description, Action assertion) => Expect(description, assertion);
    public void RunExpect(string description, Func<Task> asyncAssertion) => Expect(description, asyncAssertion);

    public void RunGiven(string description) => Given(description);
    public void RunGiven(string description, Action arrangement) => Given(description, arrangement);
    public void RunGiven(string description, Func<Task> asyncArrangement) => Given(description, asyncArrangement);

    public void RunWhen(string description) => When(description);
    public void RunWhen(string description, Action action) => When(description, action);
    public void RunWhen(string description, Func<Task> asyncAction) => When(description, asyncAction);

    public void RunThen(string description) => Then(description);
    public void RunThen(string description, Expression<Func<bool>> assertion) => Then(description, assertion);
    public void RunThen(string description, Expression<Func<Exception, bool>> exceptionAssertion) => Then(description, exceptionAssertion);
    public void RunThen(string description, Action assertion) => Then(description, assertion);
    public void RunThen(string description, Action<Exception> exceptionAssertion) => Then(description, exceptionAssertion);
    public void RunThen(string description, Func<Task> asyncAssertion) => Then(description, asyncAssertion);
    public void RunThen(string description, Func<Exception, Task> asyncExceptionAssertion) => Then(description, asyncExceptionAssertion);
    public void RunThen<T>(string description) where T : Exception => Then<T>(description);
    public void RunThen<T>(string description, Expression<Func<T, bool>> exceptionAssertion) where T : Exception => Then(description, exceptionAssertion);
    public void RunThen<T>(string description, Action<T> exceptionAssertion) where T : Exception => Then(description, exceptionAssertion);
    public void RunThen<T>(string description, Func<T, Task> asyncExceptionAssertion) where T : Exception => Then(description, asyncExceptionAssertion);

    public void RunNote(string description) => Note(description);
}