// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Represents a result of a fixture step running.
    /// </summary>
    public class FixtureStepResult
    {
        /// <summary>
        /// Gets a fixture step.
        /// </summary>
        public FixtureStep Step { get; }

        /// <summary>
        /// Gets a status of a fixture step running.
        /// </summary>
        public FixtureStepStatus Status { get; protected set; }

        /// <summary>
        /// Gets an exception that is thrown during running a fixture step.
        /// </summary>
        public Exception Exception { get; protected set; }

        /// <summary>
        /// Gets a start time at which to start running a fixture step.
        /// </summary>
        public DateTime? StartTime { get; }

        /// <summary>
        /// Gets an end time at which to complete running a fixture step.
        /// </summary>
        public DateTime? EndTime { get; }

        /// <summary>
        /// Gets a duration during running a fixture step.
        /// </summary>
        public TimeSpan? Duration => EndTime.HasValue && StartTime.HasValue ? new TimeSpan?(EndTime.Value.Subtract(StartTime.Value)) : null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepResult"/> class
        /// with the specified <see cref="Builder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="Builder"/> to build a new instance of the <see cref="FixtureStepResult"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public FixtureStepResult(Builder builder)
        {
            builder.RequireNonNull(nameof(builder));

            Step = builder.Step;
            Status = builder.Status;
            Exception = builder.Exception;

            StartTime = builder.StartTime;
            EndTime = builder.EndTime;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Builder"/> class
        /// with the specified a fixture step.
        /// </summary>
        /// <param name="step">The fixture step.</param>
        /// <returns>
        /// The new instance of the <see cref="Builder"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public static Builder Of(FixtureStep step) => new Builder(step.RequireNonNull(nameof(step)));

        /// <summary>
        /// Clears the exception and sets the status to Passed.
        /// </summary>
        public void ClearException()
        {
            Status = FixtureStepStatus.Passed;
            Exception = null;
        }

        /// <summary>
        /// Provides the function to build the <see cref="FixtureStepResult"/> class.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Gets a fixture step.
            /// </summary>
            public FixtureStep Step { get; }

            /// <summary>
            /// Gets a status of a fixture step running.
            /// </summary>
            public FixtureStepStatus Status { get; protected set; }

            /// <summary>
            /// Gets an exception that is thrown during running a fixture step.
            /// </summary>
            public Exception Exception { get; protected set; }

            /// <summary>
            /// Gets a start time at which to start running a fixture step.
            /// </summary>
            public DateTime? StartTime { get; protected set; }

            /// <summary>
            /// Gets an end time at which to complete running a fixture step.
            /// </summary>
            public DateTime? EndTime { get; protected set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Builder"/> class
            /// with the specified fixture step.
            /// </summary>
            /// <param name="step">The fixture step.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="step"/> is <c>null</c>.
            /// </exception>
            public Builder(FixtureStep step)
            {
                Step = step.RequireNonNull(nameof(step));
            }

            /// <summary>
            /// Sets a fixture step status to None.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder None()
            {
                Status = FixtureStepStatus.None;
                return this;
            }

            /// <summary>
            /// Sets a fixture step status to Ready.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder Ready()
            {
                Status = FixtureStepStatus.Ready;
                return this;
            }

            /// <summary>
            /// Sets a fixture step status to Pending.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder Pending()
            {
                Status = FixtureStepStatus.Pending;
                return this;
            }

            /// <summary>
            /// Sets a fixture step status to Running.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder Running()
            {
                Status = FixtureStepStatus.Running;
                return this;
            }

            /// <summary>
            /// Sets a fixture step status to Passed.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder Passed()
            {
                Status = FixtureStepStatus.Passed;
                return this;
            }

            /// <summary>
            /// Sets a fixture step status to Failed.
            /// </summary>
            /// <returns>The new instance of the <see cref="Builder"/> class.</returns>
            public Builder Failed(Exception exception)
            {
                Status = FixtureStepStatus.Failed;
                Exception = exception;
                return this;
            }

            /// <summary>
            /// Sets the specified start time at which to start running a fixture step.
            /// </summary>
            /// <param name="startTime">
            /// The start time at which to start running a fixture step.
            /// </param>
            /// <returns>The instance of the <see cref="Builder"/> class.</returns>
            public Builder StartAt(DateTime startTime)
            {
                StartTime = startTime;
                return this;
            }

            /// <summary>
            /// Sets the specified end time at which to complete running a fixture step.
            /// </summary>
            /// <param name="endTime">
            /// The end time at which to complete running a fixture step.
            /// </param>
            /// <returns>The new instance of the <see cref="FixtureStepResult"/> class.</returns>
            public FixtureStepResult EndAt(DateTime endTime)
            {
                EndTime = endTime;
                return new FixtureStepResult(this);
            }

            /// <summary>
            /// Builds a new instance of the <see cref="FixtureStepResult"/> class.
            /// </summary>
            /// <returns>The new instance of the <see cref="FixtureStepResult"/> class.</returns>
            public FixtureStepResult Build() => new FixtureStepResult(this);
        }
    }
}
