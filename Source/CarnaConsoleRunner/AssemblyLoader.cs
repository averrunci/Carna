// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.IO;
using System.Reflection;

using Carna.Runner.Configuration;

namespace Carna.ConsoleRunner
{
    internal class AssemblyLoader : IAssemblyLoader
    {
        Assembly IAssemblyLoader.Load(string assemblyFile)
            => new CarnaAssemblyLoadContext(assemblyFile).LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile)));
    }
}
