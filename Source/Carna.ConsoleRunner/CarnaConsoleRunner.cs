// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Carna.ConsoleRunner.Configuration;

namespace Carna.ConsoleRunner
{
    /// <summary>
    /// Represents a console runner of Carna.
    /// </summary>
    public static class CarnaConsoleRunner
    {
        /// <summary>
        /// Gets an assembly name of a runner.
        /// </summary>
        public static string Name { get; } = Path.GetFileNameWithoutExtension(typeof(CarnaConsoleRunner).GetTypeInfo().Assembly.Location);

        /// <summary>
        /// Runs with the specified arguments of the command line.
        /// </summary>
        /// <param name="args">The arguments of the command line.</param>
        /// <param name="runnerName">The assembly name of the runner.</param>
        /// <param name="commandLineParser">The command line parser.</param>
        /// <returns>The value that indicates the running result.</returns>
        public static int Run(string[] args, string runnerName = null, ICarnaRunnerCommandLineParser commandLineParser = null)
        {
            try
            {
                WriteHeader();

                var options = (commandLineParser ?? new CarnaRunnerCommandLineParser()).Parse(args);
                if (options.HasHelp)
                {
                    WriteUsage(runnerName);
                    return CarnaConsoleRunnerResult.Success.Value();
                }

                return ConsoleFixtureEngine.Start(options) ? CarnaConsoleRunnerResult.Success.Value() : CarnaConsoleRunnerResult.Failed.Value();
            }
            catch (InvalidCommandLineOptionException exc)
            {
                CarnaConsole.WriteLine(exc.Message);
                CarnaConsole.WriteLine($@"
For option syntax, type ""{(string.IsNullOrEmpty(runnerName) ? string.Empty : Name + " ")}/help""
");
                return CarnaConsoleRunnerResult.InvalidCommandLineOption.Value();
            }
            catch (Exception exc)
            {
                CarnaConsole.WriteLine(exc);
                return CarnaConsoleRunnerResult.Error.Value();
            }
        }

        private static void WriteHeader()
        {
            var assembly = typeof(CarnaConsoleRunner).GetTypeInfo().Assembly;
            CarnaConsole.WriteLineTitle(
                $@"--------------------------------------------
  {assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version}
--------------------------------------------
");
        }

        private static void WriteUsage(string runnerName)
        {
            CarnaConsole.WriteLine($@"Usage:

  {(string.IsNullOrEmpty(runnerName) ? string.Empty : Name + " ")}[options] [assembly file]

Description:

  Runs the fixtures in the specified assemblies.
  If an assembly or settings file is not specified,
  {Name} searches the current working
  directory for a settings file that has a file name
  that is 'carna-runner-settings.json' and uses that
  file.

Options:");
            CarnaRunnerCommandLineOptions.RegisteredOptions
                .Where(option => option.Description != null)
                .OrderBy(option => option.Order)
                .ForEach(option => CarnaConsole.WriteLine(option.Description));
            CarnaConsole.WriteLine();
        }
    }
}
