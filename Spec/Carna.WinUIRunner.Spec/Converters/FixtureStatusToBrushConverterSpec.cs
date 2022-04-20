// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Windows.UI;
using Carna.Runner;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Carna.WinUIRunner.Converters;

[Specification("FixtureStatusToBrushConverter Spec")]
class FixtureStatusToBrushConverterSpec : FixtureSteppable
{
    FixtureStatusToBrushConverter Converter { get; } = new();

    [Example("Convert")]
    [Sample(Source = typeof(FixtureStatusBrushSampleDataSource))]
    async Task Ex01(object value, object expected)
    {
        await CarnaWinUIRunner.Window.DispatcherQueue.RunAsync(() =>
        {
            if (expected.Equals(Colors.Transparent))
            {
                expected = (Color)Application.Current.Resources["SystemAccentColor"];
            }

            Expect(
                $"the converted color of brush should be {expected}",
                () => ((SolidColorBrush)Converter.Convert(value, null, null, null)).Color.Equals(expected)
            );
        });
    }

    class FixtureStatusBrushSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new { Description = "When FixtureStatus.Ready is converted", Value = FixtureStatus.Ready, Expected = Colors.Gray };
            yield return new { Description = "When FixtureStatus.Running is converted", Value = FixtureStatus.Running, Expected = Colors.Transparent };
            yield return new { Description = "When FixtureStatus.Passed is converted", Value = FixtureStatus.Passed, Expected = Colors.Lime };
            yield return new { Description = "When FixtureStatus.Failed is converted", Value = FixtureStatus.Failed, Expected = Colors.Red };
            yield return new { Description = "When FixtureStatus.Pending is converted", Value = FixtureStatus.Pending, Expected = Colors.Yellow };
        }
    }
}