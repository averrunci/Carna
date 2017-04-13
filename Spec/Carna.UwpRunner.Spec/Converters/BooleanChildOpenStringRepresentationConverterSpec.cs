// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.UwpRunner.Converters
{
    [Specification("BooleanChildOpenStringRepresentationConverter Spec")]
    class BooleanChildOpenStringRepresentationConverterSpec : FixtureSteppable
    {
        BooleanChildOpenStringRepresentationConverter Converter { get; } = new BooleanChildOpenStringRepresentationConverter();

        [Example("Convert")]
        [Sample(true, "-", Description = "When true is converterd")]
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
}
