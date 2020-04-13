// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.ConsoleRunner
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                CarnaConsole.WriteLine(e.ExceptionObject as Exception);
                Environment.Exit(CarnaConsoleRunnerResult.Error.Value());
            };
            return CarnaConsoleRunner.Run(args, CarnaConsoleRunner.Name);
        }
    }
}