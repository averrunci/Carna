// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

using Carna.Runner;

namespace Carna.ConsoleRunner
{
    internal class FixtureResultContents : MarshalByRefObject
    {
        public FixtureDescriptorContents FixtureDescriptor { get; }
        public FixtureStatus Status { get; }
        public Exception Exception { get; }
        public IEnumerable<FixtureResultContents> Results { get; }
        public IEnumerable<FixtureStepResultContents> StepResults { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }

        public FixtureResultContents(FixtureResult result)
        {
            FixtureDescriptor = new FixtureDescriptorContents(result.FixtureDescriptor);
            Status = result.Status;
            Exception = result.Exception == null ? null : new WrapperAssertionException(result.Exception.GetType().ToString(), result.Exception.Message, result.Exception.StackTrace);
            Results = result.Results.Select(r => new FixtureResultContents(r)).ToList();
            StepResults = result.StepResults.Select(r => new FixtureStepResultContents(r)).ToList();
            StartTime = result.StartTime;
            EndTime = result.EndTime;
        }
    }

    internal static class FixtureResultContentsExtensions
    {
        public static FixtureResult ToFixtureResult(this FixtureResultContents @this)
        {
            var builder = new FixtureResult.Builder(@this.FixtureDescriptor.ToFixtureDescriptor());
            if (@this.StartTime.HasValue) builder.StartAt(@this.StartTime.Value);
            if (@this.EndTime.HasValue) builder.EndAt(@this.EndTime.Value);

            if (@this.Results.Any()) return builder.FinishedWith(@this.Results.Select(ToFixtureResult));
            if (@this.StepResults.Any()) return builder.FinishedWith(@this.StepResults.Select(stepResult => stepResult.ToFixtureStepResult()));

            switch (@this.Status)
            {
                case FixtureStatus.Ready: return builder.Ready();
                case FixtureStatus.Pending: return builder.Pending();
                case FixtureStatus.Running: return builder.Running();
                case FixtureStatus.Failed: return builder.Failed(@this.Exception);
                case FixtureStatus.Passed: return builder.Passed();
            }

            throw new InvalidOperationException();
        }
    }
}
