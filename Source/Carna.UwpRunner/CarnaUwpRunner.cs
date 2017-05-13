// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Windows.UI.Xaml;

using Carna.Runner;
using Carna.Runner.Formatters;

using Carna.UwpRunner.Converters;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a UWP runner of Carna.
    /// </summary>
    public static class CarnaUwpRunner
    {
        /// <summary>
        /// Runs fixtures.
        /// </summary>
        public static void Run() => Run(new FixtureFormatter());

        /// <summary>
        /// Runs fixtures with the specified formatter.
        /// </summary>
        /// <param name="formatter">The formatter to format a description of a fixture.</param>
        public static void Run(IFixtureFormatter formatter)
        {
            Application.Current.Resources["BooleanToVisibilityConverter"] = new BooleanToVisibilityConverter();
            Application.Current.Resources["BooleanToChildOpenStringRepresentationConverter"] = new BooleanToChildOpenStringRepresentationConverter();
            Application.Current.Resources["ExceptionToVisibilityConverter"] = new ExceptionToVisibilityConverter();
            Application.Current.Resources["FixtureStatusToBrushConverter"] = new FixtureStatusToBrushConverter();
            Application.Current.Resources["FixtureStepStatusToBrushConverter"] = new FixtureStepStatusToBrushConverter();

            Window.Current.Content = new CarnaUwpRunnerHostView(new CarnaUwpRunnerHost(formatter));
        }
    }
}
