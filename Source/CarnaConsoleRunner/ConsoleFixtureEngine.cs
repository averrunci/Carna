// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.ConsoleRunner.Configuration;
using Carna.Runner;

namespace Carna.ConsoleRunner
{
    internal static class ConsoleFixtureEngine
    {
        public static bool Start(CarnaRunnerCommandLineOptions options)
            => new FixtureEngine()
                .AddOptions(options)
                .AddSummaryReporter()
                .Start();
    }
}
