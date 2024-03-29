﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

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
    public GivenStepRunner(GivenStep step) : base(step)
    {
    }

    /// <summary>
    /// Runs a Given step with the specified results of a fixture step.
    /// </summary>
    /// <param name="results">The results of the fixture step that was completed running.</param>
    /// <returns>The result of the Given step running.</returns>
    /// <exception cref="InvalidFixtureStepException">
    /// The <paramref name="results"/> does not have the <see cref="WhenStep"/> or the <see cref="ThenStep"/>.
    /// </exception>
    protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
    {
        if (results.Has(typeof(WhenStep), typeof(ThenStep)))
        {
            throw new InvalidFixtureStepException("Given must be before When or Then");
        }

        if (IsPending)
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
            Step.AsyncArrangement?.Invoke().GetAwaiter().GetResult();

            return FixtureStepResult.Of(Step).Passed();
        }
        catch (Exception exc)
        {
            return FixtureStepResult.Of(Step).Failed(exc);
        }
    }

    private bool IsPending => Step.Arrangement is null && Step.AsyncArrangement is null;
}