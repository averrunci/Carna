﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Specifies the fixture that indicates a specification.
/// </summary>
/// <remarks>
/// A fixture specified by this attribute is a container fixture and a root fixture.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class SpecificationAttribute : FixtureAttribute
{
    /// <summary>
    /// Gets a value that indicates whether a fixture specified by this attribute
    /// is a container fixture.
    /// </summary>
    public override bool IsContainerFixture => true;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpecificationAttribute"/> class.
    /// </summary>
    /// <param name="fixtures">
    /// Types of fixtures that are contained by a fixture specified by this attribute.
    /// </param>
    public SpecificationAttribute(params Type[] fixtures) : base(fixtures)
    {
        IsRootFixture = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SpecificationAttribute"/> class
    /// with the specified description of a fixture specified by this attribute.
    /// </summary>
    /// <param name="description">
    /// The description of a fixture specified by this attribute.
    /// </param>
    /// <param name="fixtures">
    /// Types of fixtures that are contained by a fixture specified by this attribute.
    /// </param>
    public SpecificationAttribute(string description, params Type[] fixtures) : base(description, fixtures)
    {
        IsRootFixture = true;
    }
}