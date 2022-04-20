// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Windows.UI;
using Carna.Runner;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Carna.WinUIRunner.Converters;

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
    public object Convert(object? value, Type? targetType, object? parameter, string? language)
        => (FixtureStatus?)value switch
        {
            FixtureStatus.Ready => new SolidColorBrush(Colors.Gray),
            FixtureStatus.Running => new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]),
            FixtureStatus.Passed => new SolidColorBrush(Colors.Lime),
            FixtureStatus.Failed => new SolidColorBrush(Colors.Red),
            FixtureStatus.Pending => new SolidColorBrush(Colors.Yellow),
            _ => new SolidColorBrush(Colors.Black)
        };

    object IValueConverter.ConvertBack(object? value, Type? targetType, object? parameter, string? language)
        => throw new NotSupportedException();
}