// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;

namespace Carna.WinUIRunner;

[Specification($"{nameof(FixtureContent)} Spec")]
class FixtureContentSpec : FixtureSteppable
{
    FixtureContent Content { get; } = new();

    [Example("Determines whether a fixture is running for the specified fixture status")]
    [Sample(FixtureStatus.Ready, false, Description = "When FixtureStatus is Ready")]
    [Sample(FixtureStatus.Running, true, Description = "When FixtureStatus is Running")]
    [Sample(FixtureStatus.Passed, false, Description = "When FixtureStatus is Passed")]
    [Sample(FixtureStatus.Failed, false, Description = "When FixtureStatus is Failed")]
    [Sample(FixtureStatus.Pending, false, Description = "When FixtureStatus is Pending")]
    void Ex01(FixtureStatus status, bool isFixtureRunning)
    {
        When("status is set", () => Content.Status = status);
        Then($"the value should be {isFixtureRunning}", () => Content.IsFixtureRunning == isFixtureRunning);
    }

    [Example("Determines whether a fixture status is visible for the specified fixture status")]
    [Sample(FixtureStatus.Ready, true, Description = "When FixtureStatus is Ready")]
    [Sample(FixtureStatus.Running, false, Description = "When FixtureStatus is Running")]
    [Sample(FixtureStatus.Passed, true, Description = "When FixtureStatus is Passed")]
    [Sample(FixtureStatus.Failed, true, Description = "When FixtureStatus is Failed")]
    [Sample(FixtureStatus.Pending, true, Description = "When FixtureStatus is Pending")]
    void Ex02(FixtureStatus status, bool isFixtureStatusVisible)
    {
        When("status is set", () => Content.Status = status);
        Then($"the value should be {isFixtureStatusVisible}", () => Content.IsFixtureStatusVisible == isFixtureStatusVisible);
    }
}