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
    /// Provides the appropriate visibility for the boolean value.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns <see cref="Visibility.Visible"/> if the value is <c>true</c>,
        /// otherwise returns <see cref="Visibility.Collapsed"/>.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The type of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>
        /// <see cref="Visibility.Visible"/> if the value is <c>true</c>,
        /// otherwise <see cref="Visibility.Collapsed"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
            => (bool)value ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Returns <c>true</c> if the value is <see cref="Visibility.Visible"/>,
        /// otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The type of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>
        /// <c>true</c> if the value is <see cref="Visibility.Visible"/>, otherwise <c>false</c>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => (Visibility)value == Visibility.Visible;
    }
}
