// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

using Carna.Runner.Step;

namespace Carna.UwpRunner.Converters
{
    [Specification("FixtureStepStatusToBrushConverter Spec")]
    class FixtureStepStatusToBrushConverterSpec : FixtureSteppable
    {
        FixtureStepStatusToBrushConverter Converter { get; } = new FixtureStepStatusToBrushConverter();

        [Example("Convert")]
        [Sample(Source = typeof(FixtureStatusBrushSampleDataSource))]
        async Task Ex01(object value, object expected)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (expected.Equals(Colors.Transparent))
                {
                    expected = (Color)Application.Current.Resources["SystemAccentColor"];
                }

                Expect(
                    $"the converted color of brush should be {expected}",
                    () => (Converter.Convert(value, null, null, null) as SolidColorBrush).Color.Equals(expected)
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
            }
        }
    }
}
