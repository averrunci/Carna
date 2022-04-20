// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.UI.Xaml.Data;

namespace Carna.WinUIRunner.Converters;

/// <summary>
/// Provides the appropriate child open string representation for the boolean value.
/// </summary>
public sealed class BooleanToChildOpenStringRepresentationConverter : IValueConverter
{
    /// <summary>
    /// Returns the child open string representation if the value is <c>true</c>,
    /// otherwise returns the child close string representation.
    /// </summary>
    /// <param name="value">The source data being passed to the target.</param>
    /// <param name="targetType">The type of data expected by the target dependency property.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
    /// <param name="language">The culture of the conversion.</param>
    /// <returns>
    /// The child open string representation if the value is <c>true</c>,
    /// otherwise the child close string representation.
    /// </returns>
    public object Convert(object? value, Type? targetType, object? parameter, string? language)
        => (bool?)value is true ? "-" : "+";

    /// <summary>
    /// Returns <c>true</c> if the value is the child open string representation,
    /// otherwise returns <c>false</c>.
    /// </summary>
    /// <param name="value">The target data being passed to the source.</param>
    /// <param name="targetType">The type of data expected by the source object.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
    /// <param name="language">The culture of the conversion.</param>
    /// <returns>
    /// <c>true</c> if the value is the child open string representation, otherwise <c>false</c>.
    /// </returns>
    public object ConvertBack(object? value, Type? targetType, object? parameter, string? language)
        => value?.ToString() == "-";
}