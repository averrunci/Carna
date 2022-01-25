// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

/// <summary>
/// Specifies sample data of the fixture method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class SampleAttribute : Attribute
{
    /// <summary>
    /// Gets sample data.
    /// </summary>
    public object?[] Data { get; }

    /// <summary>
    /// Gets a type of a source of the sample data.
    /// </summary>
    public Type? Source { get; set; }

    /// <summary>
    /// Gets or sets a description of sample.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SampleAttribute"/>
    /// with the specified sample data.
    /// </summary>
    /// <param name="data">The sample data.</param>
    public SampleAttribute(params object?[] data)
    {
        Data = data;
    }
}