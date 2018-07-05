// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using Carna.Runner.Configuration;
using Carna.Runner.Formatters;
using Carna.Runner.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Represents a fixture engine.
    /// </summary>
    public class FixtureEngine
    {
        /// <summary>
        /// Occurs when to start a fixture engine.
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// Occurs when to start running a fixture.
        /// </summary>
        public event EventHandler FixtureRunning;

        /// <summary>
        /// Occurs when to complete running a fixture.
        /// </summary>
        public event EventHandler FixtureRun;

        /// <summary>
        /// Occurs when to start reporting a fixture result.
        /// </summary>
        public event EventHandler Reporting;

        /// <summary>
        /// Occurs when to complete reporting a fixture result.
        /// </summary>
        public event EventHandler Reported;

        /// <summary>
        /// Gets assemblies in which fixtures exist.
        /// </summary>
        public List<Assembly> Assemblies { get; } = new List<Assembly>();

        /// <summary>
        /// Gets or sets a filter that determines whether to run a fixture.
        /// </summary>
        public IFixtureFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets a finder that finds a fixture type.
        /// </summary>
        public IFixtureTypeFinder TypeFinder { get; set; } = new FixtureTypeFinder();

        /// <summary>
        /// Gets or sets a builder that builds a fixture.
        /// </summary>
        public IFixtureBuilder Builder { get; set; } = new FixtureBuilder();

        /// <summary>
        /// Gets or sets a factory that creates a step runner.
        /// </summary>
        public IFixtureStepRunnerFactory StepRunnerFactory { get; set; } = new FixtureStepRunnerFactory();

        /// <summary>
        /// Gets reporters that report a fixture result.
        /// </summary>
        public ICollection<IFixtureReporter> Reporters { get; protected set; } = new Collection<IFixtureReporter>();

        /// <summary>
        /// Gets or sets a value that indicates whether to run a fixture in parallel.
        /// The default value is <c>true</c>.
        /// </summary>
        public bool Parallel { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureEngine"/> class.
        /// </summary>
        public FixtureEngine()
        {
        }

        /// <summary>
        /// Adds a reporter that report a fixture result.
        /// </summary>
        /// <param name="reporter">The reporter to be added.</param>
        /// <returns>The instance of the <see cref="FixtureEngine"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reporter"/> is <c>null</c>.
        /// </exception>
        public FixtureEngine AddReporter(IFixtureReporter reporter)
        {
            Reporters.Add(reporter.RequireNonNull(nameof(reporter)));
            return this;
        }

        /// <summary>
        /// Removes the specified reporter.
        /// </summary>
        /// <param name="reporter">The reporter to be removed.</param>
        /// <returns>The instance of the <see cref="FixtureEngine"/>.</returns>
        public FixtureEngine RemoveReporter(IFixtureReporter reporter)
        {
            Reporters.Remove(reporter);
            return this;
        }

        /// <summary>
        /// Clears all reporters.
        /// </summary>
        /// <returns>The instance of the <see cref="FixtureEngine"/>.</returns>
        public FixtureEngine ClearReporters()
        {
            Reporters.Clear();
            return this;
        }

        /// <summary>
        /// Configures the fixture engine with the specified configuration.
        /// </summary>
        /// <param name="configuration">
        /// The configuration used to configure the fixture engine.
        /// </param>
        /// <returns>The fixture engine to be configured.</returns>
        public FixtureEngine Configure(CarnaRunnerConfiguration configuration)
        {
            if (configuration == null) return this;

            configuration.Assemblies.IfPresent(assemblies => Assemblies.AddRange(assemblies));
            configuration.Filter.IfPresent(filter =>
            {
                filter.Type.IfAbsent(() => filter.Type = typeof(FixtureFilter).ToString());
                Filter = Create<IFixtureFilter>(filter);
            });
            configuration.Finder.IfPresent(finder => TypeFinder = Create<IFixtureTypeFinder>(finder));
            configuration.Builder.IfPresent(builder => Builder = Create<IFixtureBuilder>(builder));
            configuration.StepRunnerFactory.IfPresent(stepRunnerFactory => StepRunnerFactory = Create<IFixtureStepRunnerFactory>(stepRunnerFactory));

            configuration.Reporters.ForEach(config =>
            {
                var reporter = Create<IFixtureReporter>(config.Reporter);
                reporter.FixtureFormatter = config.Formatter == null ? new FixtureFormatter() : Create<IFixtureFormatter>(config.Formatter);
                AddReporter(reporter);
            });

            Parallel = configuration.Parallel;

            return this;
        }

        /// <summary>
        /// Creates a new instance of the type that is defined in the specified configuration.
        /// </summary>
        /// <typeparam name="T">The type of an instance to create.</typeparam>
        /// <param name="configuration">
        /// The configuration that defines the type of an instance to create.
        /// </param>
        /// <returns>
        /// The new instance of the type that is defined in the specified configuration.
        /// </returns>
        /// <exception cref="TypeNotFoundException">
        /// The type that is defined in the <paramref name="configuration"/> is not found.
        /// </exception>
        protected virtual T Create<T>(CarnaConfiguration configuration) where T : class => configuration.Create<T>(Assemblies);

        /// <summary>
        /// Starts a fixture engine.
        /// </summary>
        /// <returns>
        /// <c>true</c> if all fixtures are passed; otherwise, <c>false</c>.
        /// </returns>
        public bool Start() => Start(null);

        /// <summary>
        /// Starts a fixture engine with the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies in which fixtures exist.</param>
        /// <returns>
        /// <c>true</c> if all fixtures are passed; otherwise, <c>false</c>.
        /// </returns>
        public bool Start(IEnumerable<Assembly> assemblies)
        {
            var targetAssemblies = new List<Assembly>(Assemblies);
            
            OnStarting(EventArgs.Empty);
            assemblies.IfPresent(_ => targetAssemblies.AddRange(assemblies));
            StepRunnerFactory.RegisterFrom(targetAssemblies);

            OnFixtureRunning(EventArgs.Empty);
            var results = RunFixtures(targetAssemblies);
            OnFixtureRun(EventArgs.Empty);

            OnReporting(EventArgs.Empty);
            Reporters.ForEach(reporter => reporter.Report(results));
            OnReported(EventArgs.Empty);

            return results.All(result => result.Status == FixtureStatus.Passed);
        }

        /// <summary>
        /// Builds fixtures with assemblies configured by the configuration.
        /// </summary>
        /// <returns>The fixtures that are built.</returns>
        public IEnumerable<IFixture> BuildFixtures() => BuildFixtures(Assemblies);

        /// <summary>
        /// Builds fixtures with the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies in which fixtures exist.</param>
        /// <returns>The fixtures that are built.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assemblies"/> is <c>null</c>.
        /// </exception>
        protected IEnumerable<IFixture> BuildFixtures(IEnumerable<Assembly> assemblies)
            => Builder.Build(TypeFinder.Find(assemblies.RequireNonNull(nameof(assemblies))));

        /// <summary>
        /// Runs the specified fixtures.
        /// </summary>
        /// <param name="fixtures">The fixtures to be run.</param>
        /// <returns>The fixture running results.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtures"/> is <c>null</c>.
        /// </exception>
        public IList<FixtureResult> RunFixtures(IEnumerable<IFixture> fixtures)
        {
            fixtures.RequireNonNull(nameof(fixtures));

            if (Parallel)
            {
                return fixtures
                    .AsParallel()
                    .Select(fixture => fixture.Run(Filter, StepRunnerFactory, Parallel))
                    .Where(result => result != null)
                    .ToList();
            }
            else
            {
                return fixtures
                    .Select(fixture => fixture.Run(Filter, StepRunnerFactory, Parallel))
                    .Where(result => result != null)
                    .ToList();
            }
        }

        /// <summary>
        /// Runs fixtures that exits in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies in which fixtures exist.</param>
        /// <returns>The fixture running results.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assemblies"/> is <c>null</c>.
        /// </exception>
        protected IList<FixtureResult> RunFixtures(IEnumerable<Assembly> assemblies)
            => RunFixtures(BuildFixtures(assemblies.RequireNonNull(nameof(assemblies))));

        /// <summary>
        /// Raises the <see cref="Starting"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnStarting(EventArgs e) => Starting?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureRunning"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureRunning(EventArgs e) => FixtureRunning?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureRun"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureRun(EventArgs e) => FixtureRun?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="Reporting"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnReporting(EventArgs e) => Reporting?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="Reported"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnReported(EventArgs e) => Reported?.Invoke(this, e);
    }
}
