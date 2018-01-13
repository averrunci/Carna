// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run a Given step.
    /// </summary>
    public class GivenStepRunner : FixtureStepRunner<GivenStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStepRunner"/> class
        /// with the specified Given step.
        /// </summary>
        /// <param name="step">The Given step to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public GivenStepRunner(GivenStep step) : base(step.RequireNonNull(nameof(step)))
        {
        }

        /// <summary>
        /// Runs a Given step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the Given step running.</returns>
        protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        {
            if (results.Has(typeof(WhenStep), typeof(ThenStep)))
            {
                throw new InvalidFixtureStepException("Given must be before When or Then");
            }

            if (Step.Arrangement == null && Step.AsyncArrangement == null)
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            if (results.HasExceptionAt<GivenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            try
            {
                Step.Arrangement?.Invoke();
                Step.AsyncArrangement?.Invoke()?.GetAwaiter().GetResult();

                return FixtureStepResult.Of(Step).Passed();
            }
            catch (Exception exc)
            {
                return FixtureStepResult.Of(Step).Failed(exc);
            }
        }
    }
}
