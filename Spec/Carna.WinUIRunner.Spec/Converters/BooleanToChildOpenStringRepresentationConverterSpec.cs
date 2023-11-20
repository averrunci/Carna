// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.WinUIRunner.Converters;

[Specification($"{nameof(BooleanToChildOpenStringRepresentationConverter)} Spec")]
class BooleanToChildOpenStringRepresentationConverterSpec : FixtureSteppable
{
    BooleanToChildOpenStringRepresentationConverter Converter { get; } = new();

    [Example("Convert")]
    [Sample(true, "-", Description = "When true is converted")]
    [Sample(false, "+", Description = "When false is converted")]
    void Ex01(object value, object expected)
    {
        Expect($"the converted value should be '{expected}'", () => Converter.Convert(value, null, null, null).Equals(expected));
    }

    [Example("Convert back")]
    [Sample("-", true, Description = "When '-' is converted back")]
    [Sample("+", false, Description = "When '+' is converted back")]
    void Ex02(object value, object expected)
    {
        Expect($"the converted back value should be {expected}", () => Converter.ConvertBack(value, null, null, null).Equals(expected));
    }
}