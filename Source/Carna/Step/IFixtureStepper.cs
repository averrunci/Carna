// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Step;

/// <summary>
/// Provides the function to take a fixture step.
/// </summary>
public interface IFixtureStepper
{
    /// <summary>
    /// Takes the specified fixture step.
    /// </summary>
    /// <param name="step">The fixture step to take.</param>
    void Take(FixtureStep step);
}