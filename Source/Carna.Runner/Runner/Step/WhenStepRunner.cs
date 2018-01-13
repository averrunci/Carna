// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run a When step.
    /// </summary>
    public class WhenStepRunner : FixtureStepRunner<WhenStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteStepRunner"/> class
        /// with the specified When step.
        /// </summary>
        /// <param name="step">The When step to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public WhenStepRunner(WhenStep step) : base(step.RequireNonNull(nameof(step)))
        {
        }

        /// <summary>
        /// Runs a When step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the When step running.</returns>
        protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        {
            if (Step.Action == null && Step.AsyncAction == null)
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            if (results.HasExceptionAt<GivenStep>() || results.HasLatestExceptionAt<WhenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            try
            {
                Step.Action?.Invoke();
                Step.AsyncAction?.Invoke()?.GetAwaiter().GetResult();

                return FixtureStepResult.Of(Step).Passed();
            }
            catch (Exception exc)
            {
                return FixtureStepResult.Of(Step).Failed(exc);
            }
        }
    }
}
