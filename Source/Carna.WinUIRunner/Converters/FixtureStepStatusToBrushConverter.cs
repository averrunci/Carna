// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Windows.UI;
using Carna.Runner.Step;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Carna.WinUIRunner.Converters;

/// <summary>
/// Provides the appropriate brush for the fixture step status.
/// </summary>
public class FixtureStepStatusToBrushConverter : IValueConverter
{
    /// <summary>
    /// Returns the appropriate brush for the fixture step status.
    /// </summary>
    /// <param name="value">The source data being passed to the target.</param>
    /// <param name="targetType">The type of data expected by the target dependency property.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
    /// <param name="language">The culture of the conversion.</param>
    /// <returns>The appropriate brush for the fixture step status.</returns>
    public object Convert(object? value, Type? targetType, object? parameter, string? language)
        => (FixtureStepStatus?)value switch
        {
            FixtureStepStatus.Ready => new SolidColorBrush(Colors.Gray),
            FixtureStepStatus.Running => new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]),
            FixtureStepStatus.Passed => new SolidColorBrush(Colors.Lime),
            FixtureStepStatus.Failed => new SolidColorBrush(Colors.Red),
            FixtureStepStatus.Pending => new SolidColorBrush(Colors.Yellow),
            FixtureStepStatus.None => new SolidColorBrush(Colors.Yellow),
            _ => new SolidColorBrush(Colors.Black)
        };

    object IValueConverter.ConvertBack(object? value, Type? targetType, object? parameter, string? language)
        => throw new NotSupportedException();
}