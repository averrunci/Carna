// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Carna.Step
{
    /// <summary>
    /// Represents a step that indicates a Then.
    /// </summary>
    public class ThenStep : FixtureStep
    {
        /// <summary>
        /// Gets an assertion that returns a boolean value.
        /// </summary>
        public Expression<Func<bool>> Assertion { get; }

        /// <summary>
        /// Gets an exception assertion that returns a boolean value.
        /// </summary>
        public Expression<Func<Exception, bool>> ExceptionAssertion { get; }

        /// <summary>
        /// Gets an action that throws an exception if an assertion is failed.
        /// </summary>
        public Action Action { get; }

        /// <summary>
        /// Gets an exception action that throws an exception if an assertion is failed.
        /// </summary>
        public Action<Exception> ExceptionAction { get; }

        /// <summary>
        /// Gets an asynchronous action that throws an exception if an assertion is failed.
        /// </summary>
        public Func<Task> AsyncAction { get; }

        /// <summary>
        /// Gets an asynchronous exception action that throws an exception if an assertion is failed.
        /// </summary>
        public Func<Exception, Task> AsyncExceptionAction { get; }

        /// <summary>
        /// Gets a type of an exception that is thrown at When step.
        /// </summary>
        public Type ExceptionType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, assertion that returns a boolean value,
        /// caller type, method name, full path of the source file, and line number
        /// in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="assertion">
        /// The assertion that returns a boolean value.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Expression<Func<bool>> assertion, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            Assertion = assertion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, exception assertion that returns
        /// a boolean value, caller type, method name, full path of the source
        /// file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAssertion">
        /// The exception assertion that returns a boolean value.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Expression<Func<Exception, bool>> exceptionAssertion, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionAssertion = exceptionAssertion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, action that throws an exception
        /// if an assertion is failed, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
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
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, exception action that throws an
        /// exception if an assertion is failed, caller type, method name,
        /// full path of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionAction">
        /// The exception action that throws an exception if an assertion is failed.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Action<Exception> exceptionAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionAction = exceptionAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, asynchronous action that throws
        /// an exception if an assertion is failed, caller type, method name,
        /// full path of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
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
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            AsyncAction = asyncAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, asynchronous exception action
        /// that throws an exception if an assertion is failed, caller type,
        /// method name, full path of the source file, and line number in the
        /// source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="asyncExceptionAction">
        /// The asynchronous exception action that throws an exception if an assertion is failed.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Func<Exception, Task> asyncExceptionAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            AsyncExceptionAction = asyncExceptionAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, exception type, caller type, 
        /// method name, full path of the source file, and line number in
        /// the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionType">The type of the exception that is thrown at When step.</param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Type exceptionType, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionType = exceptionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description exception type, exception assertion
        /// that returns a boolean value, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionType">The type of the exception that is thrown at When step.</param>
        /// <param name="exceptionAssertion">
        /// The exception assertion that returns a boolean value.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Type exceptionType, Expression<Func<Exception, bool>> exceptionAssertion, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionType = exceptionType;
            ExceptionAssertion = exceptionAssertion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, exception type, exception action
        /// that throws an exception if an assertion is failed, caller type,
        /// method name, full path of the source file, and line number in the
        /// source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionType">The type of the exception that is thrown at When step.</param>
        /// <param name="exceptionAction">
        /// The exception action that throws an exception if an assertion is failed.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Type exceptionType, Action<Exception> exceptionAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionType = exceptionType;
            ExceptionAction = exceptionAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStep"/> class
        /// with the specified description, exception type, asynchronous
        /// exception action that throws an exception if an assertion is
        /// failed, caller type, method name, full path of the source file,
        /// and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Then step.</param>
        /// <param name="exceptionType">The type of the exception that is thrown at When step.</param>
        /// <param name="asyncExceptionAction">
        /// The asynchronous exception action that throws an exception if an assertion is failed.
        /// </param>
        /// <param name="callerType">The caller type.</param>
        /// <param name="callerMemberName">
        /// The method name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// The full path of the source file that contains the caller.
        /// </param>
        /// <param name="callerLineNumber">
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public ThenStep(string description, Type exceptionType, Func<Exception, Task> asyncExceptionAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            ExceptionType = exceptionType;
            AsyncExceptionAction = asyncExceptionAction;
        }
    }
}
