// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context(
    "Statistics",
    typeof(FixtureResultSpec_Statistics_TotalCount),
    typeof(FixtureResultSpec_Statistics_PassedCount),
    typeof(FixtureResultSpec_Statistics_FailedCount),
    typeof(FixtureResultSpec_Statistics_PendingCount),
    typeof(FixtureResultSpec_Statistics_StartTime),
    typeof(FixtureResultSpec_Statistics_EndTime)
)]
class FixtureResultSpec_Statistics : FixtureSteppable
{
}