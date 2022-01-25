// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration;

/// <summary>
/// Provides the function to parse a command line.
/// </summary>
public interface ICarnaRunnerCommandLineParser
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
    CarnaRunnerCommandLineOptions Parse(string[] args);
}