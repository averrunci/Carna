// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration;

/// <summary>
/// Provides the function to parse a command line.
/// </summary>
public class CarnaRunnerCommandLineParser : ICarnaRunnerCommandLineParser
{
    /// <summary>
    /// Parses the specified arguments of the command line.
    /// </summary>
    /// <param name="args">The arguments of the command line.</param>
    /// <returns>The new instance of the <see cref="CarnaRunnerCommandLineOptions"/>.</returns>
    /// <exception cref="InvalidCommandLineOptionException">
    /// The argument of the command line is not specified and
    /// a settings file does not exist.
    /// </exception>
    protected virtual CarnaRunnerCommandLineOptions Parse(string[] args)
    {
        if (args.Any()) return ParseArguments(args);

        var options = new CarnaRunnerCommandLineOptions();
        if (File.Exists(options.SettingsFilePath)) return options;

        throw new InvalidCommandLineOptionException(@"Specify an assembly or setting file.
The current working directory does not contain a settings file.");
    }

    /// <summary>
    /// Parses the specified arguments of the command line.
    /// </summary>
    /// <param name="args">The arguments of the command line.</param>
    /// <returns>The new instance of the <see cref="CarnaRunnerCommandLineOptions"/>.</returns>
    /// <exception cref="InvalidCommandLineOptionException">
    /// The <paramref name="args"/> contain an unknown option.
    /// </exception>
    protected virtual CarnaRunnerCommandLineOptions ParseArguments(string[] args)
        => args.Select(CreateCarnaRunnerCommandLineOptionContext)
            .Aggregate(new CarnaRunnerCommandLineOptions(), (options, context) =>
            {
                var option = CarnaRunnerCommandLineOptions.RegisteredOptions.FirstOrDefault(o => o.CanApply(context));
                if (option is null) throw new InvalidCommandLineOptionException($@"Unknown option
Option: {context.Argument}");

                option.Apply(options, context);
                return options;
            });

    /// <summary>
    /// Creates a new instance of the <see cref="CarnaRunnerCommandLineOptionContext"/>
    /// with the specified argument of the command line option.
    /// </summary>
    /// <param name="arg">The argument of the command line option.</param>
    /// <returns>The new instance of the <see cref="CarnaRunnerCommandLineOptionContext"/>.</returns>
    protected virtual CarnaRunnerCommandLineOptionContext CreateCarnaRunnerCommandLineOptionContext(string arg)
        => CarnaRunnerCommandLineOptionContext.Of(arg);

    CarnaRunnerCommandLineOptions ICarnaRunnerCommandLineParser.Parse(string[] args) => Parse(args);
}