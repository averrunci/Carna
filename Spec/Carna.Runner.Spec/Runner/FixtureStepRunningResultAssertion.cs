// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Assertions;
using Carna.Runner.Step;
using Carna.Step;

namespace Carna.Runner
{
    internal class FixtureStepRunningResultAssertion : AssertionObject
    {
        [AssertionProperty]
        FixtureStep Step { get; }

        [AssertionProperty]
        bool StartTimeHasValue { get; }

        [AssertionProperty]
        bool EndTimeHasValue { get; }

        [AssertionProperty]
        bool DurationHasValue { get; }

        [AssertionProperty]
        Exception Exception { get; }

        [AssertionProperty]
        FixtureStepStatus Status { get; }

        private FixtureStepRunningResultAssertion(FixtureStep step, bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, Exception exception, FixtureStepStatus status)
        {
            Step = step;
            StartTimeHasValue = startTimeHasValue;
            EndTimeHasValue = endTimeHasValue;
            DurationHasValue = durationHasValue;
            Exception = exception;
            Status = status;
        }

        public static FixtureStepRunningResultAssertion Of(FixtureStep step, bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, Exception exception, FixtureStepStatus status) => new FixtureStepRunningResultAssertion(step, startTimeHasValue, endTimeHasValue, durationHasValue, exception, status);
        public static FixtureStepRunningResultAssertion Of(FixtureStepResult stepResult) => new FixtureStepRunningResultAssertion(stepResult.Step, stepResult.StartTime.HasValue, stepResult.EndTime.HasValue, stepResult.Duration.HasValue, stepResult.Exception, stepResult.Status);
    }
}
