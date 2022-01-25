// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("FixtureStatus")]
class FixtureResultSpec_FixtureStatus
{
    [Context]
    FixtureResultSpec_FixtureStatus_FinishedWithFixtureResult FinishedWithFixtureResult => default!;

    [Context]
    FixtureResultSpec_FixtureStatus_FinishedWithFixtureStepResult FinishedWithFixtureStepResult => default!;
}