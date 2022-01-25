// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

/// <summary>
/// Represents the method that handles the <see cref="FixtureBase.FixtureReady"/>,
/// <see cref="FixtureBase.FixtureRunning"/>, or <see cref="FixtureBase.FixtureRun"/>
/// event.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="FixtureRunEventArgs"/> that contains the event data.</param>
public delegate void FixtureRunEventHandler(object? sender, FixtureRunEventArgs e);

/// <summary>
/// Provides the data for the <see cref="FixtureBase.FixtureReady"/>,
/// <see cref="FixtureBase.FixtureRunning"/>, or <see cref="FixtureBase.FixtureRun"/>
/// event.
/// </summary>
public class FixtureRunEventArgs : EventArgs
{
    /// <summary>
    /// Gets a fixture running result.
    /// </summary>
    public FixtureResult Result { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureRunEventArgs"/> class
    /// with the specified fixture running result.
    /// </summary>
    /// <param name="result">The fixture running result.</param>
    public FixtureRunEventArgs(FixtureResult result)
    {
        Result = result;
    }
}