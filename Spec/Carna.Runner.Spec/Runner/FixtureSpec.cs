// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Specification(
    $"{nameof(Fixture)} Spec",
    typeof(FixtureSpec_Ready),
    typeof(FixtureSpec_CanRun),
    typeof(FixtureSpec_RunFixture),
    typeof(FixtureSpec_RunFixtureAsync)
)]
class FixtureSpec;