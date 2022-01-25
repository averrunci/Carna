// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

/// <summary>
/// Represents an option of an assembly.
/// </summary>
public class AssemblyOption : CarnaRunnerCommandLineOption
{
    /// <summary>
    /// Gets keys of the assembly option.
    /// </summary>
    public override IEnumerable<string> Keys { get; } = Enumerable.Empty<string>();

    /// <summary>
    /// Gets a description of the assembly option.
    /// </summary>
    public override string Description => string.Empty;

    /// <summary>
    /// Gets an order of the assembly option.
    /// </summary>
    public override int Order => -1;

    /// <summary>
    /// Gets a value that indicates whether the specified context of the command line option can be applied
    /// as an assembly option.
    /// </summary>
    /// <param name="context">The context of the command line option to be applied.</param>
    /// <returns>
    /// <c>true</c> if the specified context of the command line option can be applied;
    /// otherwise, <c>false</c>.
    /// </returns>
    public override bool CanApply(CarnaRunnerCommandLineOptionContext context)
        => !context.HasKey && !string.IsNullOrEmpty(context.Argument);

    /// <summary>
    /// Applies the specified context of the command line option to the specified command line options.
    /// </summary>
    /// <param name="options">The command line options to apply the command line option.</param>
    /// <param name="context">The context of the command line option to be applied.</param>
    /// <exception cref="InvalidCommandLineOptionException">
    /// Assembly file defined by <paramref name="context"/> does not exist.
    /// </exception>
    protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
    {
        if (!File.Exists(context.Argument)) throw new InvalidCommandLineOptionException($@"Assembly file does not exist.
File: {context.Argument}");

        options.Assemblies.Add(context.Argument);
    }
}