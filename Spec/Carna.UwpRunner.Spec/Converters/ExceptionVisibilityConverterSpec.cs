// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Windows.UI.Xaml;

namespace Carna.UwpRunner.Converters
{
    [Specification("ExceptionVisibilityConverter Spec")]
    class ExceptionVisibilityConverterSpec : FixtureSteppable
    {
        ExceptionVisibilityConverter Converter { get; } = new ExceptionVisibilityConverter();

        [Example("Convert")]
        [Sample(null, Visibility.Collapsed, Description = "When null is converted")]
        [Sample("", Visibility.Collapsed, Description = "When empty is converted")]
        [Sample("Exception", Visibility.Visible, Description = "When non null or empty string is converted")]
        void Ex01(object value, object expected)
        {
            Expect($"the converted value should be Visibility.{expected}", () => Converter.Convert(value, null, null, null).Equals(expected));
        }
    }
}
