// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Runtime.Serialization;
using Carna.Runner.Configuration;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents the configuration of CarnaWinUIRunner.
/// </summary>
[DataContract]
public class CarnaWinUIRunnerConfiguration : CarnaRunnerConfiguration
{
    /// <summary>
    /// Gets or sets a value that indicates whether to exit the application automatically.
    /// </summary>
    [DataMember(Name = "autoExit")]
    public bool AutoExit { get; set; }

    /// <summary>
    /// Gets or sets the configuration of a formatter.
    /// </summary>
    [DataMember(Name = "formatter")]
    public CarnaConfiguration? Formatter { get; set; }
}