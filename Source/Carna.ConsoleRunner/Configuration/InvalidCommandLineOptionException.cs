// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.ConsoleRunner.Configuration
{
    /// <summary>
    /// Represents errors that occur when the invalid command line option is specified.
    /// </summary>
    public class InvalidCommandLineOptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandLineOptionException"/> class.
        /// </summary>
        public InvalidCommandLineOptionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandLineOptionException"/> class
        /// with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCommandLineOptionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandLineOptionException"/> class
        /// with the specified error message and reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// </param>
        public InvalidCommandLineOptionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
