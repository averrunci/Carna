// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner.Configuration;

/// <summary>
/// Provides the function to load an assembly.
/// </summary>
public interface IAssemblyLoader
{
    /// <summary>
    /// Loads an assembly with the specified file name or path.
    /// </summary>
    /// <param name="assemblyFile">The name or path of the assembly file.</param>
    /// <returns>The assembly loaded from the specified file name or path.</returns>
    Assembly Load(string assemblyFile);
}