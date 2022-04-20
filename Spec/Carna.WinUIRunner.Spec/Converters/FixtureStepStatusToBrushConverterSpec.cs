// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Windows.UI;
using Carna.Runner.Step;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Carna.WinUIRunner.Converters;

[Specification("FixtureStepStatusToBrushConverter Spec")]
class FixtureStepStatusToBrushConverterSpec : FixtureSteppable
{
    FixtureStepStatusToBrushConverter Converter { get; } = new();

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
            yield return new { Description = "When FixtureStepStatus.Ready is converted", Value = FixtureStepStatus.Ready, Expected = Colors.Gray };
            yield return new { Description = "When FixtureStepStatus.Running is converted", Value = FixtureStepStatus.Running, Expected = Colors.Transparent };
            yield return new { Description = "When FixtureStepStatus.Passed is converted", Value = FixtureStepStatus.Passed, Expected = Colors.Lime };
            yield return new { Description = "When FixtureStepStatus.Failed is converted", Value = FixtureStepStatus.Failed, Expected = Colors.Red };
            yield return new { Description = "When FixtureStepStatus.Pending is converted", Value = FixtureStepStatus.Pending, Expected = Colors.Yellow };
            yield return new { Description = "When FixtureStepStatus.None is converted", Value = FixtureStepStatus.None, Expected = Colors.Yellow };
        }
    }
}