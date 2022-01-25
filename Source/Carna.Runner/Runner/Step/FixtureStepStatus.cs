// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

/// <summary>
/// Represents a fixture step status.
/// </summary>
public enum FixtureStepStatus
{
    /// <summary>
    /// The status is none.
    /// </summary>
    None,

    /// <summary>
    /// The fixture step is ready.
    /// </summary>
    Ready,

    /// <summary>
    /// The fixture step is pending.
    /// </summary>
    Pending,

    /// <summary>
    /// The fixture step is running.
    /// </summary>
    Running,

    /// <summary>
    /// The fixture step is passed.
    /// </summary>
    Passed,

    /// <summary>
    /// The fixture step is failed.
    /// </summary>
    Failed
}