// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Carna.Runner;
using Carna.Runner.Formatters;

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
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary
                {
                    Source = new Uri("ms-appx:///Carna.UwpRunner/Resources/Resources.xaml")
                }
            );
            Window.Current.Content = new ContentControl
            {
                Content = new CarnaUwpRunnerHost(formatter)
            };
        }
    }
}
