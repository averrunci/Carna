// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

using Carna.ConsoleRunner.Configuration;
using Carna.Runner;
using Carna.Runner.Configuration;

namespace Carna.ConsoleRunner
{
    internal class AppDomainFixtureEngine : MarshalByRefObject
    {
        private readonly IAssemblyLoader assemblyLoader = new AssemblyLoader();

        public IList<FixtureResultContents> Start(string assembly, bool hasHelp, string settingsFilePath, string filter)
        {
            var engine = new FixtureEngine { CanReportFixtureResults = false };
            engine.AddOptions(
                new CarnaRunnerCommandLineOptions
                {
                    HasHelp = hasHelp,
                    SettingsFilePath = settingsFilePath,
                    Filter = filter
                });
            engine.Assemblies.Clear();
            return engine.StartNoReport(new[] { assemblyLoader.Load(assembly) })
                .Select(result => new FixtureResultContents(result))
                .ToList();
        }
    }
}
