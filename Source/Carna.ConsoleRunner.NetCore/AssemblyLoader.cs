// Copyright (C) 2017 Fievus
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
        Assembly IAssemblyLoader.Load(string assemblyFile) => Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile)));
    }
}
