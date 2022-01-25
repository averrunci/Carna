// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner;

/// <summary>
/// Provides the function to build fixtures.
/// </summary>
public interface IFixtureBuilder
{
    /// <summary>
    /// Builds fixtures with the specified fixture types.
    /// </summary>
    /// <param name="fixtureTypes">The fixture types to build.</param>
    /// <returns>
    /// The fixtures that is built with the specified fixture types.
    /// </returns>
    IEnumerable<IFixture> Build(IEnumerable<TypeInfo> fixtureTypes);
}