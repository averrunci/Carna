// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.ConsoleRunner.Configuration;

/// <summary>
/// Represents all command line options.
/// </summary>
public class CarnaRunnerCommandLineOptions
{
    /// <summary>
    /// Gets or sets a value that indicates whether the help option is contained.
    /// </summary>
    public bool HasHelp { get; set; }

    /// <summary>
    /// Gets assembly files.
    /// </summary>
    public IList<string> Assemblies { get; } = new List<string>();

    /// <summary>
    /// Gets or sets a settings file path.
    /// </summary>
    public string SettingsFilePath { get; set; } = "carna-runner-settings.json";

    /// <summary>
    /// Gets or sets a filter value.
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether to wait for a user input
    /// before running the fixtures.
    /// </summary>
    public bool CanPause { get; set; }

    static CarnaRunnerCommandLineOptions()
    {
        registeredOptions = typeof(CarnaRunnerCommandLineOption).GetTypeInfo().Assembly.DefinedTypes
            .Where(t => typeof(CarnaRunnerCommandLineOption).GetTypeInfo().IsAssignableFrom(t))
            .Where(t => !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t.AsType()) as CarnaRunnerCommandLineOption)
            .OfType<CarnaRunnerCommandLineOption>()
            .ToList();
    }

    /// <summary>
    /// Gets registered options.
    /// </summary>
    public static IEnumerable<CarnaRunnerCommandLineOption> RegisteredOptions => registeredOptions.AsReadOnly();
    private static readonly List<CarnaRunnerCommandLineOption> registeredOptions;

    /// <summary>
    /// Registers the specified option.
    /// </summary>
    /// <param name="option">The option to be registered.</param>
    public static void Register(CarnaRunnerCommandLineOption option)
        => registeredOptions.Add(option);
        
    /// <summary>
    /// Unregisters the specified option.
    /// </summary>
    /// <param name="option">The option to be unregistered.</param>
    public static void Unregister(CarnaRunnerCommandLineOption option)
        => registeredOptions.Remove(option);
}