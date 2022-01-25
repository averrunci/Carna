// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Specification("FixtureResult Spec")]
class FixtureResultSpec
{
    [Context]
    FixtureResultSpec_FixtureStatus FixtureStatus => default!;

    [Context]
    FixtureResultSpec_Statistics Statistics => default!;
}