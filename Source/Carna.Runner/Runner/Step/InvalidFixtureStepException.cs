// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Represents errors that occur when to run an invalid fixture step.
    /// </summary>
    public class InvalidFixtureStepException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFixtureStepException"/> class.
        /// </summary>
        public InvalidFixtureStepException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFixtureStepException"/> class
        /// with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFixtureStepException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFixtureStepException"/> class
        /// with the specified error message and reference to the inner exception that is the
        /// cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidFixtureStepException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
