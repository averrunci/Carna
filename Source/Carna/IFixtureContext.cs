// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Provides a context of a fixture.
/// </summary>
public interface IFixtureContext
{
    /// <summary>
    /// Gets a name of a fixture.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets a full name of a fixture.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// Gets an attribute that specifies a fixture.
    /// </summary>
    FixtureAttribute Attribute { get; }
}