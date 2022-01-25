// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions;

/// <summary>
/// Specifies the property that is asserted by the <see cref="AssertionObject"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class AssertionPropertyAttribute : Attribute
{
    /// <summary>
    /// Gets a description of the property.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionPropertyAttribute"/> class.
    /// </summary>
    public AssertionPropertyAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionPropertyAttribute"/> class
    /// with the specified description of the property.
    /// </summary>
    /// <param name="description">The description of the property.</param>
    public AssertionPropertyAttribute(string description)
    {
        Description = description;
    }
}