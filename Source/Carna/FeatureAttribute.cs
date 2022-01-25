// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Specifies the fixture that indicates a feature.
/// </summary>
/// <remarks>
/// A fixture specified by this attribute is a container fixture and a root fixture.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class FeatureAttribute : FixtureAttribute
{
    /// <summary>
    /// Gets a value that indicates whether a fixture specified by this attribute
    /// is a container fixture.
    /// </summary>
    public override bool IsContainerFixture => true;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureAttribute"/> class.
    /// </summary>
    public FeatureAttribute()
    {
        IsRootFixture = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureAttribute"/> class
    /// with the specified description of a fixture specified by this attribute.
    /// </summary>
    /// <param name="description">
    /// The description of a fixture specified by this attribute.
    /// </param>
    public FeatureAttribute(string description) : base(description)
    {
        IsRootFixture = true;
    }
}