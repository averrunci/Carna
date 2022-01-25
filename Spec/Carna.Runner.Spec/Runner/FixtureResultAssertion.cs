// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Assertions;

namespace Carna.Runner;

internal class FixtureResultAssertion : AssertionObject
{
    [AssertionProperty]
    bool StartTimeHasValue { get; }

    [AssertionProperty]
    bool EndTimeHasValue { get; }

    [AssertionProperty]
    bool DurationHasValue { get; }

    [AssertionProperty]
    AssertionProperty<Exception?> Exception { get; }

    [AssertionProperty]
    int StepResultsCount { get; }

    [AssertionProperty]
    int ResultsCount { get; }

    [AssertionProperty]
    FixtureStatus Status { get; }

    public FixtureResultAssertion(bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, AssertionProperty<Exception?> exception, int stepResultsCount, int resultsCount, FixtureStatus status)
    {
        StartTimeHasValue = startTimeHasValue;
        EndTimeHasValue = endTimeHasValue;
        DurationHasValue = durationHasValue;
        Exception = exception;
        StepResultsCount = stepResultsCount;
        ResultsCount = resultsCount;
        Status = status;
    }

    public static FixtureResultAssertion ForNullException(bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, int stepResultsCount, int resultsCount, FixtureStatus status) => new(startTimeHasValue, endTimeHasValue, durationHasValue, new EqualAssertionProperty<Exception?>(null), stepResultsCount, resultsCount, status);
    public static FixtureResultAssertion ForNotNullException(bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, int stepResultsCount, int resultsCount, FixtureStatus status) => new(startTimeHasValue, endTimeHasValue, durationHasValue, new NotEqualAssertionProperty<Exception?>(null), stepResultsCount, resultsCount, status);
    public static FixtureResultAssertion Of(FixtureResult result) => new(result.StartTime.HasValue, result.EndTime.HasValue, result.Duration.HasValue, new ActualValueProperty<Exception?>(result.Exception), result.StepResults.Count(), result.Results.Count(), result.Status);
}