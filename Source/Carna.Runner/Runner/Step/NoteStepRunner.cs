// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

/// <summary>
/// Provides the function to run a Note step.
/// </summary>
public class NoteStepRunner : FixtureStepRunner<NoteStep>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoteStepRunner"/> class
    /// with the specified Note step.
    /// </summary>
    /// <param name="step">The Note step to run.</param>
    public NoteStepRunner(NoteStep step) : base(step)
    {
    }

    /// <summary>
    /// Runs a Note step with the specified results of a fixture step.
    /// </summary>
    /// <param name="results">The results of the fixture step that was completed running.</param>
    /// <returns>The result of the Note step running.</returns>
    protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        => FixtureStepResult.Of(Step).None();
}