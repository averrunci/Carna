// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Specification("Fixture Spec")]
    class FixtureSpec
    {
        [Context]
        FixtureSpec_Ready Ready { get; }

        [Context]
        FixtureSpec_CanRun CanRun { get; }

        [Context]
        FixtureSpec_RunFixture RunFixture { get; }

        [Context]
        FixtureSpec_RunFixtureAsync RunFixtureAsync{ get; }
    }
}
