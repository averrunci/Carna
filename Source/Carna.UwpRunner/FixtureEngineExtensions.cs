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
        public static FixtureEngine LoadConfiguration(this FixtureEngine @this)
        {
            using (var stream = new FileStream("carna-runner-settings.json", FileMode.Open, FileAccess.Read))
            {
                stream.Position = (stream.ReadByte() == 0xef) ? 3 : 0;

                var serializer = new DataContractJsonSerializer(
                    typeof(CarnaRunnerConfiguration),
                    new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true }
                );
                @this.Configure((serializer.ReadObject(stream) as CarnaRunnerConfiguration)?.Ensure(new AssemblyLoader()));
            }

            return @this;
        }

        class AssemblyLoader : IAssemblyLoader
        {
            Assembly IAssemblyLoader.Load(string assemblyFile) => Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile)));
        }
    }
}
