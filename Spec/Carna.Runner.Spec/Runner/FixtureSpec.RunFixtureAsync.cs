// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context(
    "Runs a fixture asynchronously",
    typeof(FixtureSpec_RunFixtureAsync_NormalFixture),
    typeof(FixtureSpec_RunFixtureAsync_FixtureImplementedIDisposable),
    typeof(FixtureSpec_RunFixtureAsync_FixtureImplementedIAsyncDisposable),
    typeof(FixtureSpec_RunFixtureAsync_FixtureImplementedIFixtureSteppable)
)]
class FixtureSpec_RunFixtureAsync
{
}