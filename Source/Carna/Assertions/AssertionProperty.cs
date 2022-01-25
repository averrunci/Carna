// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions;

/// <summary>
/// Represents a property that is asserted by an assertion object.
/// </summary>
/// <typeparam name="TValue">The type of the property value to assert.</typeparam>
public abstract class AssertionProperty<TValue> : IAssertionProperty
{
    /// <summary>
    /// Gets a value of a property.
    /// </summary>
    protected TValue Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionProperty{TValue}"/> class
    /// with the specified property value to assert.
    /// </summary>
    /// <param name="value">The value of the property to assert.</param>
    protected AssertionProperty(TValue value)
    {
        Value = value;
    }

    /// <summary>
    /// Asserts the specified property value.
    /// </summary>
    /// <param name="other">The value of the property to assert.</param>
    /// <returns>
    /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
    /// </returns>
    protected abstract bool Assert(TValue other);

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Format(Value is null ? "<null>" : Value.ToString());

    /// <summary>
    /// Formats a string representation of the current object.
    /// </summary>
    /// <param name="value">The string representation of the current object.</param>
    /// <returns>The formatted string representation of the current object.</returns>
    protected virtual string Format(string? value) => value ?? string.Empty;

    /// <summary>
    /// Asserts the specified <see cref="IAssertionProperty"/>.
    /// </summary>
    /// <param name="other">The <see cref="IAssertionProperty"/> to assert.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="IAssertionProperty"/> is asserted; otherwise <c>false</c>.
    /// </returns>
    protected virtual bool Assert(IAssertionProperty? other)
    {
        if (other is null) return false;
        if (GetType() != other.GetType() && other.GetType() != typeof(ActualValueProperty<TValue>)) return false;
        if (other is not AssertionProperty<TValue> assertionProperty) return false;
        return Assert(assertionProperty.Value);
    }

    bool IAssertionProperty.Assert(IAssertionProperty? other) => Assert(other);
}