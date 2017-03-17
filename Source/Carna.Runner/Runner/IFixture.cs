// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;

using Carna.Runner.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to run a fixture.
    /// </summary>
    public interface IFixture
    {
        /// <summary>
        /// Occurs when a fixture is ready.
        /// </summary>
        event FixtureRunEventHandler FixtureReady;

        /// <summary>
        /// Occurs when to start running a fixture.
        /// </summary>
        event FixtureRunEventHandler FixtureRunning;

        /// <summary>
        /// Occurs when to complete running a fixture.
        /// </summary>
        event FixtureRunEventHandler FixtureRun;

        /// <summary>
        /// Occurs when to start running a fixture step.
        /// </summary>
        event FixtureStepRunEventHandler FixtureStepRunning;

        /// <summary>
        /// Occurs when to complete running a fixture step.
        /// </summary>
        event FixtureStepRunEventHandler FixtureStepRun;

        /// <summary>
        /// Gets a descriptor of a fixture.
        /// </summary>
        FixtureDescriptor FixtureDescriptor { get; }

        /// <summary>
        /// Gets or sets a parent of a fixture.
        /// </summary>
        IFixture ParentFixture { get; set; }

        /// <summary>
        /// Gets the parameters in a fixture.
        /// </summary>
        IDictionary<string, object> Parameters { get; }

        /// <summary>
        /// Ensures a parent.
        /// </summary>
        /// <returns>The instance of the <see cref="IFixture"/>.</returns>
        IFixture EnsureParent();

        /// <summary>
        /// Readies a fixture state.
        /// </summary>
        void Ready();

        /// <summary>
        /// Gets a value that indicates whether to be able to run a fixture.
        /// </summary>
        /// <param name="filter">
        /// The filter that determines whether to run a fixture.
        /// </param>
        /// <returns>
        /// <c>true</c> if a fixture can be run; otherwise, <c>false</c>.
        /// </returns>
        bool CanRun(IFixtureFilter filter);

        /// <summary>
        /// Runs a fixture with the specified filter, step runner factory,
        /// and a value that indicates whether to run a fixture in parallel.
        /// </summary>
        /// <param name="filter">
        /// The filter that determines whether to run a fixture.
        /// </param>
        /// <param name="stepRunnerFactory">
        /// The factory to create a step runner.
        /// </param>
        /// <param name="parallel">
        /// <c>true</c> if a fixture is run in parallel; otherwise, <c>false</c>.
        /// </param>
        /// <returns>
        /// The result of a fixture running if a fixture can be run; otherwise, <c>null</c>.
        /// </returns>
        FixtureResult Run(IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel = false);
    }
}
