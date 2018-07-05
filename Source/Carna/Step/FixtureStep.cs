// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Step
{
    /// <summary>
    /// Represents a fixture step used in a fixture.
    /// </summary>
    public class FixtureStep
    {
        /// <summary>
        /// Gets a description of a fixture step
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets a caller type.
        /// </summary>
        public Type CallerType { get; }

        /// <summary>
        /// Gets a method name of the caller to the method.
        /// </summary>
        public string CallerMemberName { get; }

        /// <summary>
        /// Gets a full path of the source file that contains the caller.
        /// </summary>
        public string CallerFilePath { get; }

        /// <summary>
        /// Gets a line number in the source file at which the method is called.
        /// </summary>
        public int CallerLineNumber { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStep"/> class
        /// with the specified description, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a fixture step.</param>
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
        protected FixtureStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            Description = description;
            CallerType = callerType;
            CallerMemberName = callerMemberName;
            CallerFilePath = callerFilePath;
            CallerLineNumber = callerLineNumber;
        }
    }
}
