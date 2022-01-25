// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Specifies the fixture that indicates a namespace.
/// </summary>
/// <remarks>
/// A fixture specified by this attribute is a container fixture.
/// </remarks>
public class NamespaceFixtureAttribute : FixtureAttribute
{
    /// <summary>
    /// Gets a value that indicates whether a fixture specified by this attribute
    /// is a container fixture.
    /// </summary>
    public override bool IsContainerFixture => true;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamespaceFixtureAttribute"/> class.
    /// </summary>
    public NamespaceFixtureAttribute()
    {
        CanRunParallel = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamespaceFixtureAttribute"/> class
    /// with the specified description of a fixture.
    /// </summary>
    /// <param name="description">The description of a fixture.</param>
    public NamespaceFixtureAttribute(string description) : base(description)
    {
        CanRunParallel = true;
    }
}