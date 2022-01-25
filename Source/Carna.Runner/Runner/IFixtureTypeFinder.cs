// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner;

/// <summary>
/// Provides the function to find a fixture type.
/// </summary>
public interface IFixtureTypeFinder
{
    /// <summary>
    /// Finds fixture types with the specified assemblies.
    /// </summary>
    /// <param name="assemblies">
    /// The assemblies in which fixture types exist.
    /// </param>
    /// <returns>
    /// The fixture types in the specified assemblies.
    /// </returns>
    IEnumerable<TypeInfo> Find(IEnumerable<Assembly> assemblies);
}