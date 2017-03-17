// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Represents errors that occur when a fixture step runner is not found.
    /// </summary>
    public class FixtureStepRunnerNotFoundException : Exception
    {
        /// <summary>
        /// The type of the fixture step.
        /// </summary>
        public Type StepType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepRunnerNotFoundException"/> class
        /// with the specified fixture step type.
        /// </summary>
        /// <param name="stepType">The type of the fixture step.</param>
        public FixtureStepRunnerNotFoundException(Type stepType) : this(stepType, $"The type of FixtureStepRunner for {stepType} is not found")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepRunnerNotFoundException"/> class
        /// with the specified fixture step type and error message.
        /// </summary>
        /// <param name="stepType">The type of the fixture step.</param>
        /// <param name="message">The message that describes the error.</param>
        public FixtureStepRunnerNotFoundException(Type stepType, string message) : this(stepType, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepRunnerNotFoundException"/> class
        /// with the specified fixture step type, error message, and reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="stepType">The type of the fixture step.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public FixtureStepRunnerNotFoundException(Type stepType, string message, Exception innerException) : base(message, innerException)
        {
            StepType = stepType;
        }
    }
}
