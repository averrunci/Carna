// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Specification("FixtureResult Spec")]
    class FixtureResultSpec
    {
        [Context]
        FixtureResultSpec_FixtureStatus FixtureStatus { get; }

        [Context]
        FixtureResultSpec_Statistics Statistics { get; }
    }
}
