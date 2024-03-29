﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;

namespace Carna.Assertions;

/// <summary>
/// Represents a property to assert that an enumerable is equal.
/// </summary>
/// <typeparam name="TValue">The type of the enumerable property value to assert.</typeparam>
public class EnumerableAssertionProperty<TValue> : AssertionProperty<IEnumerable<TValue>?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerableAssertionProperty{TValue}"/> class
    /// with the specified property value to assert.
    /// </summary>
    /// <param name="value">The value of the property to assert.</param>
    public EnumerableAssertionProperty(IEnumerable<TValue>? value) : base(value)
    {
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value is null ? "<null>" : $"[{string.Join(", ", Value.Select(x => x is null ? "<null>" : x.ToString()))}]";

    /// <summary>
    /// Asserts the specified property value.
    /// </summary>
    /// <param name="other">The value of the property to assert.</param>
    /// <returns>
    /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
    /// </returns>
    protected override bool Assert(IEnumerable<TValue>? other)
    {
        if (Value is null && other is null) return true;
        if (Value is null || other is null) return false;
        return Assert(Value, other);
    }

    private bool Assert(IEnumerable first, IEnumerable second)
    {
        if (first is ICollection firstCollection && second is ICollection secondCollection)
        {
            if (firstCollection.Count != secondCollection.Count) return false;
        }

        var firstEnumerator = first.GetEnumerator();
        var secondEnumerator = second.GetEnumerator();
        while (firstEnumerator.MoveNext())
        {
            if (!secondEnumerator.MoveNext()) return false;

            if (firstEnumerator.Current is IEnumerable firstEnumerable && secondEnumerator.Current is IEnumerable secondEnumerable)
            {
                if (!Assert(firstEnumerable, secondEnumerable)) return false;
            }
            else
            {
                if (!Equals(firstEnumerator.Current, secondEnumerator.Current)) return false;
            }
        }
        return !secondEnumerator.MoveNext();
    }
}