// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run a fixture step.
    /// </summary>
    /// <typeparam name="T">The type of the fixture step to run.</typeparam>
    public abstract class FixtureStepRunner<T> : IFixtureStepRunner where T : FixtureStep
    {
        /// <summary>
        /// Gets a fixture step to run.
        /// </summary>
        protected T Step { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepRunner{T}"/> class
        /// with the specified fixture step.
        /// </summary>
        /// <param name="step">The fixture step to run.</param>
        protected FixtureStepRunner(T step)
        {
            Step = step.RequireNonNull(nameof(step));
        }

        /// <summary>
        /// Runs a fixture step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the fixture step running.</returns>
        protected abstract FixtureStepResult.Builder Run(FixtureStepResultCollection results);
        FixtureStepResult.Builder IFixtureStepRunner.Run(FixtureStepResultCollection results) => Run(results.RequireNonNull(nameof(results)));
    }
}
