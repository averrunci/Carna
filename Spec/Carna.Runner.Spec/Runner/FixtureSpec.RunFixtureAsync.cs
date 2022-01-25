// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Runs a fixture asynchronously")]
class FixtureSpec_RunFixtureAsync : FixtureSteppable
{
    [Context]
    FixtureSpec_RunFixtureAsync_NormalFixture NormalFixture => default!;

    [Context]
    FixtureSpec_RunFixtureAsync_FixtureImplementedIDisposable FixtureImplementedIDisposable => default!;

    [Context]
    FixtureSpec_RunFixtureAsync_FixtureImplementedIFixtureSteppable FixtureImplementedIFixtureSteppable => default!;
}