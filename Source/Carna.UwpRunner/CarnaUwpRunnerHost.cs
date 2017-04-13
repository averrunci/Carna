// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using System.Reflection;

using Fievus.Windows.Mvc.Bindings;

using Carna.Runner;
using Carna.Runner.Formatters;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a host of CarnaUwpRunner.
    /// </summary>
    public class CarnaUwpRunnerHost
    {
        /// <summary>
        /// Gets a title of CarnaUwpRunner.
        /// </summary>
        public string Title => $"{typeof(CarnaUwpRunner).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {typeof(CarnaUwpRunner).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version}";

        /// <summary>
        /// Gets a maximum width of a fixture content.
        /// </summary>
        public ObservableProperty<double> FixtureContentMaxWidth { get; } = new ObservableProperty<double>();

        /// <summary>
        /// Gets a fixture summary.
        /// </summary>
        public FixtureSummary Summary { get; } = new FixtureSummary();

        /// <summary>
        /// Gets fixture contents.
        /// </summary>
        public ObservableCollection<FixtureContent> Fixtures { get; } = new ObservableCollection<FixtureContent>();

        /// <summary>
        /// Gets a formatter of a fixture.
        /// </summary>
        public IFixtureFormatter Formatter { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarnaUwpRunner"/> class
        /// with the specified formatter.
        /// </summary>
        /// <param name="formatter">
        /// The formatter of a fixture. If <c>null</c> is specified, <see cref="FixtureFormatter"/> is used.
        /// </param>
        public CarnaUwpRunnerHost(IFixtureFormatter formatter) => Formatter = formatter ?? new FixtureFormatter();
    }
}
