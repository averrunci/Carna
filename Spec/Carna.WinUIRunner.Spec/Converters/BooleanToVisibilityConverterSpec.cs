// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.UI.Xaml;

namespace Carna.WinUIRunner.Converters;

[Specification($"{nameof(BooleanToVisibilityConverter)} Spec")]
class BooleanToVisibilityConverterSpec : FixtureSteppable
{
    BooleanToVisibilityConverter Converter { get; } = new();

    [Example("Convert")]
    [Sample(true, Visibility.Visible, Description = "When true is converted")]
    [Sample(false, Visibility.Collapsed, Description = "When false is converted")]
    void Ex01(object value, object expected)
    {
        Expect($"the converted value should be Visibility.{expected}", () => Converter.Convert(value, null, null, null).Equals(expected));
    }

    [Example("Convert back")]
    [Sample(Visibility.Visible, true, Description = "When Visibility.Visible is converted back")]
    [Sample(Visibility.Collapsed, false, Description = "When Visibility.Collapsed is converted back")]
    void Ex02(object value, object expected)
    {
        Expect($"the converted back value should be {expected}", () => Converter.ConvertBack(value, null, null, null).Equals(expected));
    }
}