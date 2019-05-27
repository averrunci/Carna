// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna
{
    /// <summary>
    /// Represents a fixture that can specify steps.
    /// </summary>
    public class FixtureSteppable : IFixtureSteppable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureSteppable"/> class.
        /// </summary>
        protected FixtureSteppable()
        {
        }

        /// <summary>
        /// Specifies an Expect step with the specified description.
        /// </summary>
        /// <param name="description">The description of an Expect step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Expect(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ExpectStep(description, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies an Expect step with the specified description and assertion.
        /// </summary>
        /// <param name="description">The description of an Expect step.</param>
        /// <param name="assertion">The assertion that is used in an Expect step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Expect(string description, Expression<Func<bool>> assertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ExpectStep(description, assertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies an Expect step with the specified description and assertion.
        /// </summary>
        /// <param name="description">The description of an Expect step.</param>
        /// <param name="assertion">The assertion that is used in an Expect step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Expect(string description, Action assertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ExpectStep(description, assertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies an Expect step with the specified description and asynchronous assertion.
        /// </summary>
        /// <param name="description">The description of an Expect step.</param>
        /// <param name="asyncAssertion">The asynchronous assertion that is used in an Expect step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Expect(string description, Func<Task> asyncAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ExpectStep(description, asyncAssertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Given step with the specified description.
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Given(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new GivenStep(description, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Given step with the specified description and arrangement.
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
        /// <param name="arrangement">The arrangement that is used in a Given step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Given(string description, Action arrangement, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new GivenStep(description, arrangement, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Given step with the specified description and asynchronous arrangement.
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
        /// <param name="asyncArrangement">The asynchronous arrangement that is used in a Given step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Given(string description, Func<Task> asyncArrangement, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new GivenStep(description, asyncArrangement, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description and action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="action">The action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, Action action, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, action, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description, time-out and action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="millisecondsTimeout">The time-out value in milliseconds for running a When step.</param>
        /// <param name="action">The action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, double millisecondsTimeout, Action action, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, millisecondsTimeout, action, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description, time-out and action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="timeout">The time-out value for running a When step.</param>
        /// <param name="action">The action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, TimeSpan timeout, Action action, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, timeout, action, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description and asynchronous action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="asyncAction">The asynchronous action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, Func<Task> asyncAction, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, asyncAction, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description, time-out and asynchronous action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="millisecondsTimeout">The time-out value in milliseconds for running a When step.</param>
        /// <param name="asyncAction">The asynchronous action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, double millisecondsTimeout, Func<Task> asyncAction, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, millisecondsTimeout, asyncAction, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a When step with the specified description, time-out and asynchronous action.
        /// </summary>
        /// <param name="description">The description of a When step.</param>
        /// <param name="timeout">The time-out value for running a When step.</param>
        /// <param name="asyncAction">The asynchronous action that is used in a When step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void When(string description, TimeSpan timeout, Func<Task> asyncAction, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new WhenStep(description, timeout, asyncAction, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and assertion.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="assertion">The assertion that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Expression<Func<bool>> assertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, assertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and assertion for an exception.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAssertion">The assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Expression<Func<Exception, bool>> exceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, exceptionAssertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and assertion.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="assertion">The assertion that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Action assertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, assertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and assertion for an exception.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAssertion">The assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Action<Exception> exceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, exceptionAssertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and asynchronous assertion.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="asyncAssertion">The asynchronous assertion that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Func<Task> asyncAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, asyncAssertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified description and asynchronous assertion for an exception.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="asyncExceptionAssertion">The asynchronous assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then(string description, Func<Exception, Task> asyncExceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, asyncExceptionAssertion, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified type of an exception and description.
        /// </summary>
        /// <typeparam name="T">The type of an exception.</typeparam>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then<T>(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) where T : Exception
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, typeof(T), GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified type of an exception, description, and assertion for an exception.
        /// </summary>
        /// <typeparam name="T">The type of an exception.</typeparam>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAssertion">The assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then<T>(string description, Expression<Func<T, bool>> exceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) where T : Exception
        {
            var parameter = Expression.Parameter(typeof(Exception));
            Expression<Func<Exception, T>> convert = exc => (T)exc;
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, typeof(T), Expression.Lambda<Func<Exception, bool>>(Expression.Invoke(exceptionAssertion, Expression.Invoke(convert, parameter)), parameter), GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified type of an exception, description, and assertion for an exception.
        /// </summary>
        /// <typeparam name="T">The type of an exception.</typeparam>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAssertion">The assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then<T>(string description, Action<T> exceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) where T : Exception
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, typeof(T), exc => exceptionAssertion((T)exc), GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Then step with the specified type of an exception, description, and asynchronous assertion for an exception.
        /// </summary>
        /// <typeparam name="T">The type of an exception.</typeparam>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="asyncExceptionAssertion">The asynchronous assertion for an exception that is used in a Then step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Then<T>(string description, Func<T, Task> asyncExceptionAssertion, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) where T : Exception
        {
            (this as IFixtureSteppable).Stepper?.Take(new ThenStep(description, typeof(T), exc => asyncExceptionAssertion((T)exc), GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        /// <summary>
        /// Specifies a Note step with the specified description.
        /// </summary>
        /// <param name="description">The description of a Note step.</param>
        /// <param name="callerMemberName">The method name of the caller to the method.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called.</param>
        protected virtual void Note(string description, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            (this as IFixtureSteppable).Stepper?.Take(new NoteStep(description, GetType(), callerMemberName, callerFilePath, callerLineNumber));
        }

        IFixtureStepper IFixtureSteppable.Stepper { get; set; }
    }
}
