// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Runtime.Serialization;

namespace Carna.Runner.Configuration;

/// <summary>
/// Represents the configuration of a filter.
/// </summary>
[DataContract]
public class CarnaFilterConfiguration : CarnaConfiguration
{
    /// <summary>
    /// Gets of sets a pattern to accept a fixture.
    /// </summary>
    [DataMember(Name = "pattern")]
    public string? Pattern { get; set; }

    /// <summary>
    /// Ensures the configuration of a filter.
    /// </summary>
    /// <returns>The instance of the <see cref="CarnaFilterConfiguration"/>.</returns>
    public CarnaFilterConfiguration Ensure()
    {
        if (!string.IsNullOrEmpty(Type)) return this;

        Options ??= new Dictionary<string, string?>();
        if (Options.ContainsKey("pattern")) return this;

        Options["pattern"] = Pattern;
        return this;
    }
}