// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

using Carna.Runner;

namespace Carna.UwpRunner.Converters
{
    /// <summary>
    /// Provides the appropriate brush for the fixture status.
    /// </summary>
    public class FixtureStatusToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Returns the appropriate brush for the fixture status.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The type of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>The appropriate brush for the fixture status.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((FixtureStatus)value)
            {
                case FixtureStatus.Ready: return new SolidColorBrush(Colors.Gray);
                case FixtureStatus.Running: return new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
                case FixtureStatus.Passed: return new SolidColorBrush(Colors.Lime);
                case FixtureStatus.Failed: return new SolidColorBrush(Colors.Red);
                case FixtureStatus.Pending: return new SolidColorBrush(Colors.Yellow);
                default: return new SolidColorBrush(Colors.Black);
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotSupportedException();
    }
}
