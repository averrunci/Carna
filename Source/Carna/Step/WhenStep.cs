// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

namespace Carna.Step
{
    /// <summary>
    /// Represents a step that indicates a When.
    /// </summary>
    public class WhenStep : FixtureStep
    {
        /// <summary>
        /// Gets an action.
        /// </summary>
        public Action Action { get; }

        /// <summary>
        /// Gets an asynchronous action.
        /// </summary>
        public Func<Task> AsyncAction { get; }

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
        /// The line number in the source file at whiche the method is called.
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
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public WhenStep(string description, Action action, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
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
        /// The line number in the source file at whiche the method is called.
        /// </param>
        public WhenStep(string description, Func<Task> asyncAction, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            AsyncAction = asyncAction;
        }
    }
}
