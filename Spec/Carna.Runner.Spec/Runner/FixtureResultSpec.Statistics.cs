// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Context("Statistics")]
    class FixtureResultSpec_Statistics : FixtureSteppable
    {
        [Context]
        FixtureResultSpec_Statistics_TotalCount TotalCount { get; }

        [Context]
        FixtureResultSpec_Statistics_PassedCount PassedCount { get; }

        [Context]
        FixtureResultSpec_Statistics_FailedCount FailedCount { get; }

        [Context]
        FixtureResultSpec_Statistics_PendingCount PendingCount { get; }

        [Context]
        FixtureResultSpec_Statistics_StartTime StartTime { get; }

        [Context]
        FixtureResultSpec_Statistics_EndTime EndTime { get; }
    }
}
