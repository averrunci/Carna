// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Specification("Fixture Spec")]
class FixtureSpec
{
    [Context]
    FixtureSpec_Ready Ready => default!;

    [Context]
    FixtureSpec_CanRun CanRun => default!;

    [Context]
    FixtureSpec_RunFixture RunFixture => default!;

    [Context]
    FixtureSpec_RunFixtureAsync RunFixtureAsync => default!;
}