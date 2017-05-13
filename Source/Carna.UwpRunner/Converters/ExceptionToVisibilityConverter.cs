// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Carna.UwpRunner.Converters
{
    /// <summary>
    /// Provides the appropriate visibility for the exception.
    /// </summary>
    public class ExceptionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns <see cref="Visibility.Collapsed"/> if the value is null or empty,
        /// otherwise returns <see cref="Visibility.Visible"/>.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The type of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>
        /// <see cref="Visibility.Collapsed"/> if the value is null or empty,
        /// otherwise <see cref="Visibility.Visible"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
            => string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotSupportedException();
    }
}
