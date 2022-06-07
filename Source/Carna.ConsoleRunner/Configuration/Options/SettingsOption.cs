// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

/// <summary>
/// Represents an option of a settings.
/// </summary>
public class SettingsOption : CarnaRunnerCommandLineOption
{
    /// <summary>
    /// Gets keys of the settings option.
    /// </summary>
    public override IEnumerable<string> Keys { get; } = new[] { "/settings", "/s", "--settings", "-s" };

    /// <summary>
    /// Gets a description of the settings option.
    /// </summary>
    public override string Description => @"
  --settings:<path> or /settings:<path>
    Specifies a carna runner settings file path.
    (Short form: -s or /s)";

    /// <summary>
    /// Gets an order of the settings option.
    /// </summary>
    public override int Order => 1;

    /// <summary>
    /// Applies the specified context of the command line option to the specified command line options.
    /// </summary>
    /// <param name="options">The command line options to apply the command line option.</param>
    /// <param name="context">The context of the command line option to be applied.</param>
    /// <exception cref="InvalidCommandLineOptionException">
    /// Settings file defined by <paramref name="context"/> does not exist.
    /// </exception>
    protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
    {
        if (!File.Exists(context.Value)) throw new InvalidCommandLineOptionException($@"Settings file does not exist.
File: {context.Value}");

        options.SettingsFilePath = context.Value;
    }
}