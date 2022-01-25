// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Specification("FixtureContainer Spec")]
class FixtureContainerSpec
{
    [Context]
    FixtureContainerSpec_EnsureParent EnsureParent => default!;

    [Context]
    FixtureContainerSpec_Ready Ready => default!;

    [Context]
    FixtureContainerSpec_CanRun CanRun => default!;

    [Context]
    FixtureContainerSpec_RunFixtures RunFixtures => default!;

    [Context]
    FixtureContainerSpec_Background Background => default!;
}