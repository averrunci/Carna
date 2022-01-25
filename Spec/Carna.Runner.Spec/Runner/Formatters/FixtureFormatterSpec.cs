// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Formatters;

[Specification("FixtureFormatter Spec")]
class FixtureFormatterSpec
{
    [Context]
    FixtureFormatterSpec_FormatFixture FormatFixture => default!;

    [Context]
    FixtureFormatterSpec_FormatFixtureStep FormatFixtureStep => default!;
}