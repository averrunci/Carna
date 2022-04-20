// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Assertions;

namespace Carna.WinUIRunner.AssertionObjects;

internal class FixtureSummaryAssertion : AssertionObject
{
    [AssertionProperty]
    long TotalCount { get; }

    [AssertionProperty]
    long PassedCount { get; }

    [AssertionProperty]
    long FailedCount { get; }

    [AssertionProperty]
    long PendingCount { get; }

    [AssertionProperty]
    bool IsFixtureBuilding { get; }

    [AssertionProperty]
    bool IsFixtureBuilt { get; }

    [AssertionProperty]
    bool IsFixtureRunning { get; }

    [AssertionProperty]
    string StartDateTime { get; }

    [AssertionProperty]
    string EndDateTime { get; }

    [AssertionProperty]
    string Duration { get; }

    private FixtureSummaryAssertion(long totalCount, long passedCount, long failedCount, long pendingCount, bool isFixtureBuilding, bool isFixtureBuilt, bool isFixtureRunning, string startDateTime, string endDateTime, string duration)
    {
        TotalCount = totalCount;
        PassedCount = passedCount;
        FailedCount = failedCount;
        PendingCount = pendingCount;
        IsFixtureBuilding = isFixtureBuilding;
        IsFixtureBuilt = isFixtureBuilt;
        IsFixtureRunning = isFixtureRunning;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }

    public static FixtureSummaryAssertion Of(long totalCount, long passedCount, long failedCount, long pendingCount, bool isFixtureBuilding, bool isFixtureBuilt, bool isFixtureRunning, string startDateTime, string endDateTime, string duration)
        => new(totalCount, passedCount, failedCount, pendingCount, isFixtureBuilding, isFixtureBuilt, isFixtureRunning, startDateTime, endDateTime, duration);
    public static FixtureSummaryAssertion Of(FixtureSummary summary)
        => new(summary.TotalCount, summary.PassedCount, summary.FailedCount, summary.PendingCount, summary.IsFixtureBuilding, summary.IsFixtureBuilt, summary.IsFixtureRunning, summary.StartDateTime, summary.EndDateTime, summary.Duration);
}