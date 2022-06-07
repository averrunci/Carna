// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context(
    "Runs a fixture",
    typeof(FixtureSpec_RunFixture_NormalFixture),
    typeof(FixtureSpec_RunFixture_FixtureImplementedIDisposable),
    typeof(FixtureSpec_RunFixture_FixtureImplementedIAsyncDisposable),
    typeof(FixtureSpec_RunFixture_FixtureImplementedIFixtureSteppable),
    typeof(FixtureSpec_RunFixture_Parameter),
    typeof(FixtureSpec_RunFixture_AroundFixtureAttribute),
    typeof(FixtureSpec_RunFixture_RequiresSta)
)]
class FixtureSpec_RunFixture
{
}