// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions;

/// <summary>
/// Represents a property to assert that is greater than or equal.
/// </summary>
/// <typeparam name="TValue">The type of the property value to assert.</typeparam>
public class GreaterThanOrEqualAssertionProperty<TValue> : AssertionProperty<TValue> where TValue : IComparable<TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GreaterThanOrEqualAssertionProperty{TValue}"/> class
    /// with the specified property value to assert.
    /// </summary>
    /// <param name="value">The value of the property to assert.</param>
    public GreaterThanOrEqualAssertionProperty(TValue value) : base(value)
    {
    }

    /// <summary>
    /// Asserts the specified property value.
    /// </summary>
    /// <param name="other">The value of the property to assert.</param>
    /// <returns>
    /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
    /// </returns>
    protected override bool Assert(TValue other) => Value.CompareTo(other) <= 0;

    /// <summary>
    /// Formats a string representation of the current object.
    /// </summary>
    /// <param name="value">The string representation of the current object.</param>
    /// <returns>The formatted string representation of the current object.</returns>
    protected override string Format(string? value) => $"greater than or equal {value}";
}