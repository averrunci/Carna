﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run a Then step.
    /// </summary>
    public class ThenStepRunner : FixtureStepRunner<ThenStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteStepRunner"/> class
        /// with the specified Then step.
        /// </summary>
        /// <param name="step">The Then step to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public ThenStepRunner(ThenStep step) : base(step.RequireNonNull(nameof(step)))
        {
        }

        /// <summary>
        /// Runs a Then step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the Then step running.</returns>
        protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        {
            if (!results.Has(typeof(WhenStep)))
            {
                throw new InvalidFixtureStepException("Then must be after When.");
            }

            if (IsPending)
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            if (results.HasExceptionAt<GivenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            if (HasAssertionWithoutException && results.HasLatestExceptionAt<WhenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            try
            {
                Step.ExecuteAssertion(Step.Assertion);
                Step.Action?.Invoke();
                Step.AsyncAction?.Invoke()?.Wait();

                if (HasAssertionWithException)
                {
                    var exception = results.GetLatestExceptionAt<WhenStep>();
                    Step.ExecuteAssertion(Step.ExceptionAssertion, exception);
                    Step.ExceptionAction?.Invoke(exception);
                    Step.AsyncExceptionAction?.Invoke(exception)?.Wait();
                    results.ClearException(exception);
                }

                return FixtureStepResult.Of(Step).Passed();
            }
            catch (Exception exc)
            {
                return FixtureStepResult.Of(Step).Failed(exc);
            }
        }

        private bool IsPending =>!HasAssertionWithoutException && !HasAssertionWithException;
        private bool HasAssertionWithoutException => Step.Assertion != null || Step.Action != null || Step.AsyncAction != null;
        private bool HasAssertionWithException => Step.ExceptionAssertion != null || Step.ExceptionAction != null || Step.AsyncExceptionAction != null;
    }
}
