// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

namespace Carna.Step
{
    /// <summary>
    /// Represents a step that indicates a Given.
    /// </summary>
    public class GivenStep : FixtureStep
    {
        /// <summary>
        /// Gets an arrangement action.
        /// </summary>
        public Action Arrangement { get; }

        /// <summary>
        /// Gets an asynchronous arrangement action.
        /// </summary>
        public Func<Task> AsyncArrangement { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStep"/> class
        /// with the specified description, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
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
        public GivenStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStep"/> class
        /// with the specified description, arrangement action, caller type,
        /// method name, full path of the source file, and line number in
        /// the source file
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
        /// <param name="arrangement">The arrangement action.</param>
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
        public GivenStep(string description, Action arrangement, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            Arrangement = arrangement;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStep"/> class
        /// with the specified description, asynchronous arrangement, caller
        /// type, method name, full path of the source file, and line number
        /// in the source file.
        /// </summary>
        /// <param name="description">The description of a Given step.</param>
        /// <param name="asyncArrangement">The asynchronous arrangement action.</param>
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
        public GivenStep(string description, Func<Task> asyncArrangement, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
            AsyncArrangement = asyncArrangement;
        }
    }
}
