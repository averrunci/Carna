﻿// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run an Expect step.
    /// </summary>
    public class ExpectStepRunner : FixtureStepRunner<ExpectStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectStepRunner"/> class
        /// with the specified Expect step.
        /// </summary>
        /// <param name="step">The Expect step to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public ExpectStepRunner(ExpectStep step) : base(step.RequireNonNull(nameof(step)))
        {
        }

        /// <summary>
        /// Runs an Expect step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the Expect step running.</returns>
        protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        {
            if (IsPending)
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            if (results.HasExceptionAt<GivenStep>() || results.HasLatestExceptionAt<WhenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            if (results.HasStatusAt<GivenStep>(FixtureStepStatus.Ready) || results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Ready))
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            if (results.HasStatusAt<GivenStep>(FixtureStepStatus.Pending) || results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Pending))
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            try
            {
                Step.ExecuteAssertion(Step.Assertion);
                Step.Action?.Invoke();
                Step.AsyncAction?.Invoke()?.GetAwaiter().GetResult();

                return FixtureStepResult.Of(Step).Passed();
            }
            catch (Exception exc)
            {
                return FixtureStepResult.Of(Step).Failed(exc);
            }
        }

        private bool IsPending => Step.Action == null && Step.Assertion == null && Step.AsyncAction == null;
    }
}
