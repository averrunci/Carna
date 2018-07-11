// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;

using Carna.ConsoleRunner.Configuration;
using Carna.Runner;

namespace Carna.ConsoleRunner
{
    internal static class ConsoleFixtureEngine
    {
        public static bool Start(CarnaRunnerCommandLineOptions options)
        {
            var engine = new FixtureEngine()
                .AddOptions(options)
                .AddSummaryReporter();
            if (!options.CanCreateSeparateDomain) return engine.Start();

            var fixtureResults = RunFixturesInDedicatedAppDomain(engine, options);
            engine.Report(fixtureResults);
            return fixtureResults.All(result => result.Status == FixtureStatus.Passed);
        }

        private static IList<FixtureResult> RunFixturesInDedicatedAppDomain(FixtureEngine engine, CarnaRunnerCommandLineOptions options)
        {
            var fixtureResults = new List<FixtureResult>();

            fixtureResults.AddRange(
                engine.Parallel
                    ? engine.Assemblies.AsParallel().SelectMany(assembly => RunFixturesInDedicatedAppDomain(assembly, options))
                    : engine.Assemblies.SelectMany(assembly => RunFixturesInDedicatedAppDomain(assembly, options))
            );

            return fixtureResults;
        }

        private static IList<FixtureResult> RunFixturesInDedicatedAppDomain(Assembly assembly, CarnaRunnerCommandLineOptions options)
        {
            var appDomain = CreateAppDomain(assembly);
            try
            {
                var results = CreateAppDomainFixtureEngine(appDomain)?.Start(assembly.Location, options.HasHelp, options.SettingsFilePath, options.Filter);
                if (results == null) throw new Exception();

                return results.Select(result => result.ToFixtureResult()).ToList();
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }

        private static AppDomain CreateAppDomain(Assembly assembly)
        {
            var applicationBase = Path.GetDirectoryName(assembly.Location);
            if (applicationBase == null) throw new InvalidOperationException($"The location of {assembly} must be specified.");

            return AppDomain.CreateDomain(
                Guid.NewGuid().ToString(),
                new Evidence(AppDomain.CurrentDomain.Evidence),
                new AppDomainSetup
                {
                    ApplicationBase = applicationBase,
                    ConfigurationFile = $"{Path.Combine(applicationBase, assembly.GetName().Name)}{Path.GetExtension(assembly.Location)}.config"
                }
            );
        }

        private static AppDomainFixtureEngine CreateAppDomainFixtureEngine(AppDomain appDomain)
            => appDomain.CreateInstanceFromAndUnwrap(
                typeof(AppDomainFixtureEngine).Assembly.Location,
                typeof(AppDomainFixtureEngine).FullName ?? throw new InvalidOperationException($"The full name of the {typeof(AppDomainFixtureEngine)} must be specified")
            ) as AppDomainFixtureEngine;
    }
}
