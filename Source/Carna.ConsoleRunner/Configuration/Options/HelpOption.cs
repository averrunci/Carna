﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

/// <summary>
/// Represents an option of a help.
/// </summary>
public class HelpOption : CarnaRunnerCommandLineOption
{
    /// <summary>
    /// Gets keys of the help option.
    /// </summary>
    public override IEnumerable<string> Keys { get; } = new[] { "/help", "/h", "/?", "--help", "-h", "-?" };

    /// <summary>
    /// Gets a description of the help option.
    /// </summary>
    public override string Description => @"
  --help or /help
    Displays this usage message.
    (Short form: -h, -?, /h or /?)";

    /// <summary>
    /// Gets an order of the help option.
    /// </summary>
    public override int Order => int.MaxValue;

    /// <summary>
    /// Applies the specified context of the command line option to the specified command line options.
    /// </summary>
    /// <param name="options">The command line options to apply the command line option.</param>
    /// <param name="context">The context of the command line option to be applied.</param>
    protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
    {
        options.HasHelp = true;
    }
}