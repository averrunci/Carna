// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using System.Runtime.Serialization;

namespace Carna.Runner.Configuration;

/// <summary>
/// Represents the configuration of CarnaRunner.
/// </summary>
[DataContract]
public class CarnaRunnerConfiguration
{
    /// <summary>
    /// Gets or sets assembly names.
    /// </summary>
    [DataMember(Name = "assemblies")]
    public IList<string>? AssemblyFiles { get; set; }

    /// <summary>
    /// Gets or sets assemblies in which fixtures exist.
    /// </summary>
    public IList<Assembly>? Assemblies { get; set; }

    /// <summary>
    /// Gets the configuration of a filter.
    /// </summary>
    [DataMember(Name = "filter")]
    public CarnaFilterConfiguration? Filter { get; set; }

    /// <summary>
    /// Gets the configuration of a finder.
    /// </summary>
    [DataMember(Name = "finder")]
    public CarnaConfiguration? Finder { get; set; }

    /// <summary>
    /// Gets the configuration of a builder.
    /// </summary>
    [DataMember(Name = "builder")]
    public CarnaConfiguration? Builder { get; set; }

    /// <summary>
    /// Gets the configuration of a step runner factory.
    /// </summary>
    [DataMember(Name = "stepRunnerFactory")]
    public CarnaConfiguration? StepRunnerFactory { get; set; }

    /// <summary>
    /// Gets the configuration of reporters.
    /// </summary>
    [DataMember(Name = "reporters")]
    public IList<CarnaReporterConfiguration>? Reporters { get; set; }

    /// <summary>
    /// Gets a value that indicates whether to run a fixture in parallel.
    /// </summary>
    [DataMember(Name = "parallel")]
    public bool Parallel { get; set; }

    /// <summary>
    /// Ensures the configuration of CarnaRunner with the specified <see cref="IAssemblyLoader"/>.
    /// </summary>
    /// <param name="loader">The loader of an assembly.</param>
    /// <returns>The instance of the <see cref="CarnaRunnerConfiguration"/>.</returns>
    public CarnaRunnerConfiguration Ensure(IAssemblyLoader loader)
    {
        Assemblies = AssemblyFiles?.Select(loader.Load).ToList();
        Filter?.Ensure();
        return this;
    }

    [OnDeserializing]
    private void ApplyDefaultValues(StreamingContext context)
    {
        Parallel = true;
    }
}