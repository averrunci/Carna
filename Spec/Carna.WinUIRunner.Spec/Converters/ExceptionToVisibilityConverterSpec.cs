// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.UI.Xaml;

namespace Carna.WinUIRunner.Converters;

[Specification($"{nameof(ExceptionToVisibilityConverter)} Spec")]
class ExceptionToVisibilityConverterSpec : FixtureSteppable
{
    ExceptionToVisibilityConverter Converter { get; } = new();

    [Example("Convert")]
    [Sample(null, Visibility.Collapsed, Description = "When null is converted")]
    [Sample("", Visibility.Collapsed, Description = "When empty is converted")]
    [Sample("Exception", Visibility.Visible, Description = "When non null or empty string is converted")]
    void Ex01(object value, object expected)
    {
        Expect($"the converted value should be Visibility.{expected}", () => Converter.Convert(value, null, null, null).Equals(expected));
    }
}