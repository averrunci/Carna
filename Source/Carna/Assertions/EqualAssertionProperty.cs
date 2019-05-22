// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions
{
    /// <summary>
    /// Represents a property to assert that a value is equal.
    /// </summary>
    /// <typeparam name="TValue">The type of the property value to assert.</typeparam>
    public class EqualAssertionProperty<TValue> : AssertionProperty<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualAssertionProperty{TValue}"/> class
        /// with the specified property value to assert.
        /// </summary>
        /// <param name="value">The value of the property to assert.</param>
        public EqualAssertionProperty(TValue value) : base(value)
        {
        }

        /// <summary>
        /// Asserts the specified property value.
        /// </summary>
        /// <param name="other">The value of the property to assert.</param>
        /// <returns>
        /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
        /// </returns>
        protected override bool Assert(TValue other) => Equals(Value, other);
    }
}
