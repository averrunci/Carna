// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Formatters
{
    [Specification("JapaneseFixtureFormatter Spec")]
    class JapaneseFixtureFormatterSpec
    {
        [Context]
        JapaneseFixtureFormatterSpec_FormatFixture FormatFixture { get; }

        [Context]
        JapaneseFixtureFormatterSpec_FormatFixtureStep FormatFixtureStep { get; }
    }
}
