// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

/// <summary>
/// Represents the method that handles the <see cref="FixtureStepper.FixtureStepRunning"/>
/// or <see cref="FixtureStepper.FixtureStepRun"/> event.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="FixtureStepRunEventArgs"/> that contains the event data.</param>
public delegate void FixtureStepRunEventHandler(object? sender, FixtureStepRunEventArgs e);

/// <summary>
/// Provides the data for the <see cref="FixtureStepper.FixtureStepRunning"/>
/// or <see cref="FixtureStepper.FixtureStepRun"/> event.
/// </summary>
public class FixtureStepRunEventArgs : EventArgs
{
    /// <summary>
    /// Gets a fixture step running result.
    /// </summary>
    public FixtureStepResult Result { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureStepRunEventArgs"/> class
    /// with the specified fixture step running result.
    /// </summary>
    /// <param name="result">The fixture step running result.</param>
    public FixtureStepRunEventArgs(FixtureStepResult result)
    {
        Result = result;
    }
}