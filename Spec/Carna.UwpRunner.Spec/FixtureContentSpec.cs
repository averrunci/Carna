// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;

namespace Carna.UwpRunner
{
    [Specification("FixtureContent Spec")]
    class FixtureContentSpec : FixtureSteppable
    {
        FixtureContent Content { get; } = new FixtureContent();

        [Example("Detemines whether a fixture is running for the specified fixture status")]
        [Sample(FixtureStatus.Ready, false, Description = "When FixtureStaus is Ready")]
        [Sample(FixtureStatus.Running, true, Description = "When FixtureStaus is Running")]
        [Sample(FixtureStatus.Passed, false, Description = "When FixtureStaus is Passed")]
        [Sample(FixtureStatus.Failed, false, Description = "When FixtureStaus is Failed")]
        [Sample(FixtureStatus.Pending, false, Description = "When FixtureStaus is Pending")]
        void Ex01(FixtureStatus status, bool isFixtureRunning)
        {
            When("status is set", () => Content.Status.Value = status);
            Then($"the value should be {isFixtureRunning}", () => Content.IsFixtureRunning.Value == isFixtureRunning);
        }

        [Example("Detemines whether a fixture status is visible for the specified fixture status")]
        [Sample(FixtureStatus.Ready, true, Description = "When FixtureStaus is Ready")]
        [Sample(FixtureStatus.Running, false, Description = "When FixtureStaus is Running")]
        [Sample(FixtureStatus.Passed, true, Description = "When FixtureStaus is Passed")]
        [Sample(FixtureStatus.Failed, true, Description = "When FixtureStaus is Failed")]
        [Sample(FixtureStatus.Pending, true, Description = "When FixtureStaus is Pending")]
        void Ex02(FixtureStatus status, bool isFixtureStatusVisible)
        {
            When("status is set", () => Content.Status.Value = status);
            Then($"the value should be {isFixtureStatusVisible}", () => Content.IsFixtureStatusVisible.Value == isFixtureStatusVisible);
        }
    }
}
