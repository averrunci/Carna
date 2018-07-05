// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Step
{
    /// <summary>
    /// Represents a step that indicates a Note.
    /// </summary>
    public class NoteStep : FixtureStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteStep"/> class
        /// with the specified description, caller type, method name, full path
        /// of the source file, and line number in the source file.
        /// </summary>
        /// <param name="description">The description of a Note step.</param>
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
        public NoteStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
        }
    }
}
