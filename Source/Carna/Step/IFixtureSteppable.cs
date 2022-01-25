// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Step;

/// <summary>
/// Provides the function to run a fixture using fixture steps.
/// </summary>
public interface IFixtureSteppable
{
    /// <summary>
    /// Gets a fixture stepper that takes a fixture step.
    /// </summary>
    IFixtureStepper? Stepper { get; set; }
}