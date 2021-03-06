﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

using Carna.ConsoleRunner.Configuration;
using Carna.ConsoleRunner.Reporters;
using Carna.Runner;
using Carna.Runner.Configuration;

namespace Carna.ConsoleRunner
{
    internal static class FixtureEngineExtensions
    {
        private static IAssemblyLoader AssemblyLoader { get; } = new AssemblyLoader();

        public static FixtureEngine AddOptions(this FixtureEngine @this, CarnaRunnerCommandLineOptions options)
        {
            return @this.LoadConfiguration(options.SettingsFilePath)
                .AddAssemblies(options.Assemblies)
                .AddDefaultFilter(options.Filter);
        }

        private static FixtureEngine LoadConfiguration(this FixtureEngine @this, string filePath)
            => File.Exists(filePath) ? @this.Configure(LoadConfiguration(filePath)?.Ensure(AssemblyLoader)) : @this;

        private static CarnaRunnerConfiguration LoadConfiguration(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.Position = stream.ReadByte() == 0xef ? 3 : 0;

                var serializer = new DataContractJsonSerializer(
                    typeof(CarnaRunnerConfiguration),
                    new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true }
                );
                return serializer.ReadObject(stream) as CarnaRunnerConfiguration;
            }
        }

        private static FixtureEngine AddAssemblies(this FixtureEngine @this, IEnumerable<string> assemblies)
        {
            @this.Assemblies.AddRange(assemblies.Select(AssemblyLoader.Load));

            return @this;
        }

        private static FixtureEngine AddDefaultFilter(this FixtureEngine @this, string filter)
        {
            if (string.IsNullOrEmpty(filter)) return @this;

            @this.Filter = new FixtureFilter(new Dictionary<string, string> { ["pattern"] = filter });

            return @this;
        }

        public static FixtureEngine AddSummaryReporter(this FixtureEngine @this)
            => @this.AddReporter(new ConsoleFixtureSummaryReporter());
    }
}
