// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

/// <summary>
/// Represents an option of a pause.
/// </summary>
public class PauseOption : CarnaRunnerCommandLineOption
{
    /// <summary>
    /// Gets keys of the pause option.
    /// </summary>
    public override IEnumerable<string> Keys { get; } = new[] { "/pause", "/p", "--pause", "-p" };

    /// <summary>
    /// Gets a description of the pause option.
    /// </summary>
    public override string Description => @"
  --pause or /pause
    Specifies to wait for a user input before running
    the fixtures. (Short form: -p or /p)";

    /// <summary>
    /// Gets an order of the pause option.
    /// </summary>
    public override int Order => 3;

    /// <summary>
    /// Applies the specified context of the command line option to the specified command line options.
    /// </summary>
    /// <param name="options">The command line options to apply the command line option.</param>
    /// <param name="context">The context of the command line option to be applied.</param>
    protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
    {
        options.CanPause = true;
    }
}