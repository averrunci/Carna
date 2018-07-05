// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

using Carna.Runner;
using Carna.Runner.Configuration;

namespace Carna.UwpRunner
{
    static class FixtureEngineExtensions
    {
        public static FixtureEngine LoadConfiguration(this FixtureEngine @this, CarnaUwpRunnerHost host)
        {
            using (var stream = new FileStream("carna-runner-settings.json", FileMode.Open, FileAccess.Read))
            {
                stream.Position = (stream.ReadByte() == 0xef) ? 3 : 0;

                var serializer = new DataContractJsonSerializer(
                    typeof(CarnaUwpRunnerConfiguration),
                    new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true }
                );
                if (!(serializer.ReadObject(stream) is CarnaUwpRunnerConfiguration configuration)) return @this;

                configuration.Ensure(new AssemblyLoader());
                @this.Configure(configuration);

                host.AutoExit = configuration.AutoExit;
                if (configuration.Formatter != null)
                {
                    host.Formatter = configuration.Formatter.Create<IFixtureFormatter>(@this.Assemblies);
                }
            }

            return @this;
        }

        class AssemblyLoader : IAssemblyLoader
        {
            Assembly IAssemblyLoader.Load(string assemblyFile) => Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile)));
        }
    }
}
