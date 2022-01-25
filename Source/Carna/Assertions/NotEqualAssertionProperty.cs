// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions;

/// <summary>
/// Represents a property to assert that a value is not equal.
/// </summary>
/// <typeparam name="TValue">The type of the property value to assert.</typeparam>
public class NotEqualAssertionProperty<TValue> : AssertionProperty<TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotEqualAssertionProperty{TValue}"/> class
    /// with the specified property value to assert.
    /// </summary>
    /// <param name="value">The value of the property to assert.</param>
    public NotEqualAssertionProperty(TValue value) : base(value)
    {
    }

    /// <summary>
    /// Asserts the specified property value.
    /// </summary>
    /// <param name="other">The value of the property to assert.</param>
    /// <returns>
    /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
    /// </returns>
    protected override bool Assert(TValue other) => !Equals(Value, other);

    /// <summary>
    /// Formats a string representation of the current object.
    /// </summary>
    /// <param name="value">The string representation of the current object.</param>
    /// <returns>The formatted string representation of the current object.</returns>
    protected override string Format(string? value) => $"not {value}";
}