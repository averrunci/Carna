// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Statistics")]
class FixtureResultSpec_Statistics : FixtureSteppable
{
    [Context]
    FixtureResultSpec_Statistics_TotalCount TotalCount => default!;

    [Context]
    FixtureResultSpec_Statistics_PassedCount PassedCount => default!;

    [Context]
    FixtureResultSpec_Statistics_FailedCount FailedCount => default!;

    [Context]
    FixtureResultSpec_Statistics_PendingCount PendingCount => default!;

    [Context]
    FixtureResultSpec_Statistics_StartTime StartTime => default!;

    [Context]
    FixtureResultSpec_Statistics_EndTime EndTime => default!;
}