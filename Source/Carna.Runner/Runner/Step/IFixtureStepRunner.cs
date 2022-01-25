// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

/// <summary>
/// Provides the function to run a fixture step.
/// </summary>
public interface IFixtureStepRunner
{
    /// <summary>
    /// Runs a fixture step with the specified results of a fixture step.
    /// </summary>
    /// <param name="results">The results of the fixture step.</param>
    /// <returns>The results of the fixture step running.</returns>
    FixtureStepResult.Builder Run(FixtureStepResultCollection results);
}