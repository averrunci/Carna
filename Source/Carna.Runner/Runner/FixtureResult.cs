// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

using Carna.Runner.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Represents a result of a fixture running.
    /// </summary>
    public class FixtureResult
    {
        /// <summary>
        /// Gets a descriptor of a fixture.
        /// </summary>
        public FixtureDescriptor FixtureDescriptor { get; }

        /// <summary>
        /// Gets a status of a fixture running.
        /// </summary>
        public FixtureStatus Status { get; }

        /// <summary>
        /// Gets an exception that is thrown during running a fixture.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets exceptions that are thrown during running a fixture step.
        /// </summary>
        public IEnumerable<Exception> StepExceptions => StepResults.Select(step => step.Exception).Where(exception => exception != null);

        /// <summary>
        /// Gets containing fixture running results.
        /// </summary>
        public IEnumerable<FixtureResult> Results { get; }

        /// <summary>
        /// Gets fixture step running results.
        /// </summary>
        public IEnumerable<FixtureStepResult> StepResults { get; }

        /// <summary>
        /// Gets a start time at which to start running a fixture.
        /// </summary>
        public DateTime? StartTime { get; }

        /// <summary>
        /// Gets an end time at which to complete running a fixture.
        /// </summary>
        public DateTime? EndTime { get; }

        /// <summary>
        /// Gets a duration during running a fixture.
        /// </summary>
        public TimeSpan? Duration => EndTime.HasValue && StartTime.HasValue ? new TimeSpan?(EndTime.Value.Subtract(StartTime.Value)) : null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureResult"/> class
        /// with the specified <see cref="Builder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="Builder"/> to build a new instance of the <see cref="FixtureResult"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        protected FixtureResult(Builder builder)
        {
            builder.RequireNonNull(nameof(builder));

            FixtureDescriptor = builder.FixtureDescriptor;
            Status = builder.Status;
            Exception = builder.Exception;
            StepResults = builder.StepResults;
            Results = builder.Results;
            StartTime = builder.StartTime;
            EndTime = builder.EndTime;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Builder"/> class
        /// with the specified descriptor of a fixture.
        /// </summary>
        /// <param name="descriptor">The descriptor of a fixture.</param>
        /// <returns>
        /// The new instance of the <see cref="Builder"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="descriptor"/> is <c>null</c>.
        /// </exception>
        public static Builder Of(FixtureDescriptor descriptor) => new Builder(descriptor.RequireNonNull(nameof(descriptor)));

        /// <summary>
        /// Provides the function to build the <see cref="FixtureResult"/> class.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Gets a descriptor of a fixture.
            /// </summary>
            public FixtureDescriptor FixtureDescriptor { get; }

            /// <summary>
            /// Gets a status of a fixture running.
            /// </summary>
            public FixtureStatus Status { get; protected set; }

            /// <summary>
            /// Gets an exception that is thrown during running a fixture.
            /// </summary>
            public Exception Exception { get; protected set; }

            /// <summary>
            /// Gets containing fixture running results.
            /// </summary>
            public IEnumerable<FixtureResult> Results { get; protected set; } = Enumerable.Empty<FixtureResult>();

            /// <summary>
            /// Gets fixture step running results.
            /// </summary>
            public IEnumerable<FixtureStepResult> StepResults { get; protected set; } = Enumerable.Empty<FixtureStepResult>();

            /// <summary>
            /// Gets a start time at which to start running a fixture.
            /// </summary>
            public DateTime? StartTime { get; protected set; }

            /// <summary>
            /// Gets an end time at which to complete running a fixture.
            /// </summary>
            public DateTime? EndTime { get; protected set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Builder"/> class
            /// with the specified descriptor of a fixture.
            /// </summary>
            /// <param name="descriptor">The descriptor of a fixture.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="descriptor"/> is <c>null</c>.
            /// </exception>
            public Builder(FixtureDescriptor descriptor)
            {
                FixtureDescriptor = descriptor.RequireNonNull(nameof(descriptor));
            }

            /// <summary>
            /// Sets the specified start time at which to start running a fixture.
            /// </summary>
            /// <param name="startTime">
            /// The start time at which to start running a fixture.
            /// </param>
            /// <returns>The instance of the <see cref="Builder"/> class.</returns>
            public Builder StartAt(DateTime startTime)
            {
                StartTime = startTime;
                return this;
            }

            /// <summary>
            /// Sets the specified end time at which to complete running a fixture.
            /// </summary>
            /// <param name="endTime">
            /// The end time at which to complete running a fixture.
            /// </param>
            /// <returns>The instance of the <see cref="Builder"/> class.</returns>
            public Builder EndAt(DateTime endTime)
            {
                EndTime = endTime;
                return this;
            }

            /// <summary>
            /// Sets a fixture status to Ready.
            /// </summary>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult Ready()
            {
                Status = FixtureStatus.Ready;
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets a fixture status to Pending.
            /// </summary>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult Pending()
            {
                Status = FixtureStatus.Pending;
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets a fixture status to Running.
            /// </summary>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult Running()
            {
                Status = FixtureStatus.Running;
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets a fixture status to Passed.
            /// </summary>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult Passed()
            {
                Status = FixtureStatus.Passed;
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets a fixture status to Failed with the specified exception.
            /// </summary>
            /// <param name="exception">
            /// The exception that is the cause to fail a fixture.
            /// </param>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult Failed(Exception exception)
            {
                Status = FixtureStatus.Failed;
                Exception = exception;
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets the fixture step results and status that is determined with them.
            /// </summary>
            /// <param name="stepResults">The fixture step running results.</param>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult FinishedWith(IEnumerable<FixtureStepResult> stepResults)
            {
                StepResults = stepResults?.ToList() ?? Enumerable.Empty<FixtureStepResult>();
                Status = ResolveStatus(StepResults.Select(step => step.Status));
                return new FixtureResult(this);
            }

            /// <summary>
            /// Sets the fixture results and status that is determined with them.
            /// </summary>
            /// <param name="results">The fixture running results.</param>
            /// <returns>The new instance of the <see cref="FixtureResult"/> class.</returns>
            public FixtureResult FinishedWith(IEnumerable<FixtureResult> results)
            {
                Results = results?.ToList() ?? Enumerable.Empty<FixtureResult>();
                Status = ResolveStatus(Results.Select(result => result.Status));
                return new FixtureResult(this);
            }

            /// <summary>
            /// Resolves status with the specified fixture step status.
            /// </summary>
            /// <param name="statuses">The fixture step status to be resolved.</param>
            /// <returns>
            /// The fixture status that is determined with the specified fixture step status.
            /// </returns>
            protected virtual FixtureStatus ResolveStatus(IEnumerable<FixtureStepStatus> statuses)
            {
                var statusList = statuses?.Where(status => status != FixtureStepStatus.None).ToList();
                return statusList == null || !statusList.Any() ? FixtureStatus.Ready :
                    statusList.All(status => status == FixtureStepStatus.Ready) ? FixtureStatus.Ready :
                    statusList.All(status => status == FixtureStepStatus.Pending) ? FixtureStatus.Pending :
                    statusList.Any(status => status == FixtureStepStatus.Failed) ? FixtureStatus.Failed :
                    FixtureStatus.Passed;
            }

            /// <summary>
            /// Resolves status with the specified fixture status.
            /// </summary>
            /// <param name="statuses">The fixture status to be resolved.</param>
            /// <returns>
            /// The fixture status that is determined with the specified fixture status.
            /// </returns>
            protected virtual FixtureStatus ResolveStatus(IEnumerable<FixtureStatus> statuses)
            {
                var statusList = statuses?.ToList();
                return statusList == null || !statusList.Any() ? FixtureStatus.Ready :
                    statusList.All(status => status == FixtureStatus.Ready) ? FixtureStatus.Ready :
                    statusList.All(status => status == FixtureStatus.Pending) ? FixtureStatus.Pending :
                    statusList.Any(status => status == FixtureStatus.Failed) ? FixtureStatus.Failed :
                    FixtureStatus.Passed;
            }
        }
    }

    /// <summary>
    /// Provides extension methods of the <see cref="IEnumerable{FixtureResult}"/>.
    /// </summary>
    public static class FixtureResults
    {
        /// <summary>
        /// Gets a total count of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The total count of the fixture results.</returns>
        public static int TotalCount(this IEnumerable<FixtureResult> @this)
            => @this.FixtureResultCount(result => true);

        /// <summary>
        /// Gets a passed count of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The passed count of the fixture results.</returns>
        public static int PassedCount(this IEnumerable<FixtureResult> @this)
            => @this.FixtureResultCount(result => result.Status == FixtureStatus.Passed);

        /// <summary>
        /// Gets a failed count of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The failed count of the fixture results.</returns>
        public static int FailedCount(this IEnumerable<FixtureResult> @this)
            => @this.FixtureResultCount(result => result.Status == FixtureStatus.Failed);

        /// <summary>
        /// Gets a pending count of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The pending count of the fixture results.</returns>
        public static int PendingCount(this IEnumerable<FixtureResult> @this)
            => @this.FixtureResultCount(result => result.Status == FixtureStatus.Pending);

        /// <summary>
        /// Gets a start time of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The start time of the fixture results.</returns>
        public static DateTime StartTime(this IEnumerable<FixtureResult> @this)
            => @this.Where(result => result.StartTime.HasValue)
                .Select(result => result.StartTime.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Min();

        /// <summary>
        /// Gets an end time of the fixture results.
        /// </summary>
        /// <param name="this">The fixture results.</param>
        /// <returns>The end time of the fixture results.</returns>
        public static DateTime EndTime(this IEnumerable<FixtureResult> @this)
            => @this.IsEmpty() ? DateTime.MinValue :
                @this.Where(result => result.EndTime.HasValue)
                    .Select(result => result.EndTime.Value)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Max();

        private static int FixtureResultCount(this IEnumerable<FixtureResult> @this, Func<FixtureResult, bool> filter)
            => @this.Where(result => result.Results.IsEmpty())
                .Count(filter) + @this.Sum(result => result.Results.FixtureResultCount(filter));
    }
}
