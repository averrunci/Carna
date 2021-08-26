// Copyright (C) 2017-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Specification("FixtureContainer Spec")]
    class FixtureContainerSpec
    {
        [Context]
        FixtureContainerSpec_EnsureParent EnsureParent { get; }

        [Context]
        FixtureContainerSpec_Ready Ready { get; }

        [Context]
        FixtureContainerSpec_CanRun CanRun { get; }

        [Context]
        FixtureContainerSpec_RunFixtures RunFixtures { get; }

        [Context]
        FixtureContainerSpec_Background Background { get; }
    }
}
