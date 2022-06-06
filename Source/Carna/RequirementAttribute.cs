// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Specifies the fixture that indicates a requirement.
/// </summary>
/// <remarks>
/// A fixture specified by this attribute is a container fixture.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class RequirementAttribute : FixtureAttribute
{
    /// <summary>
    /// Gets a value that indicates whether a fixture specified by this attribute
    /// is a container fixture.
    /// </summary>
    public override bool IsContainerFixture => true;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequirementAttribute"/> class.
    /// </summary>
    /// <param name="fixtures">
    /// Types of fixtures that are contained by a fixture specified by this attribute.
    /// </param>
    public RequirementAttribute(params Type[] fixtures) : base(fixtures)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequirementAttribute"/> class
    /// with the specified description of a fixture specified by this attribute.
    /// </summary>
    /// <param name="description">
    /// The description of a fixture specified by this attribute.
    /// </param>
    /// <param name="fixtures">
    /// Types of fixtures that are contained by a fixture specified by this attribute.
    /// </param>
    public RequirementAttribute(string description, params Type[] fixtures) : base(description, fixtures)
    {
    }
}