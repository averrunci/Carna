// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Runs a fixture")]
class FixtureSpec_RunFixture
{
    [Context]
    FixtureSpec_RunFixture_NormalFixture NormalFixture => default!;

    [Context]
    FixtureSpec_RunFixture_FixtureImplementedIDisposable FixtureImplementedIDisposable => default!;

    [Context]
    FixtureSpec_RunFixture_FixtureImplementedIFixtureSteppable FixtureImplementedIFixtureSteppable => default!;

    [Context]
    FixtureSpec_RunFixture_Parameter Parameter => default!;

    [Context]
    FixtureSpec_RunFixture_AroundFixtureAttribute AroundFixtureAttribute => default!;

    [Context]
    FixtureSpec_RunFixture_RequiresSta RequiresSta => default!;
}