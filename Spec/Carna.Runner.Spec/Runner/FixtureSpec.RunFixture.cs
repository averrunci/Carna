// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Context("Runs a fixture")]
    class FixtureSpec_RunFixture
    {
        [Context]
        FixtureSpec_RunFixture_NormalFixture NormalFixture { get; }

        [Context]
        FixtureSpec_RunFixture_FixtureImplementedIDisposable FixtureImplementedIDisposable { get; }

        [Context]
        FixtureSpec_RunFixture_FixtureImplementedIFixtureSteppable FixtureImplementedIFixtureSteppable { get; }

        [Context]
        FixtureSpec_RunFixture_Parameter Parameter { get; }

        [Context]
        FixtureSpec_RunFixture_AroundFixtureAttribute AroundFixtureAttribute { get; }
    }
}
