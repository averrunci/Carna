// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Assertions
{
    /// <summary>
    /// Represents a property that is asserted by an assertion object.
    /// This property does not assert any assertion properties.
    /// </summary>
    /// <typeparam name="TValue">The type of the property value to assert.</typeparam>
    public class ActualValueProperty<TValue> : AssertionProperty<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActualValueProperty{TValue}"/> class
        /// with the specified property value to assert.
        /// </summary>
        /// <param name="value">The value of the property to assert.</param>
        public ActualValueProperty(TValue value) : base(value)
        {
        }

        /// <summary>
        /// Asserts the specified property value.
        /// </summary>
        /// <param name="other">The value of the property to assert.</param>
        /// <returns>
        /// <c>true</c> if the specified property value is asserted; otherwise <c>false</c>.
        /// </returns>
        protected override bool Assert(TValue other) => throw new InvalidOperationException();

        /// <summary>
        /// Asserts the specified <see cref="IAssertionProperty"/>.
        /// </summary>
        /// <param name="other">The <see cref="IAssertionProperty"/> to assert.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="IAssertionProperty"/> is asserted; otherwise <c>false</c>.
        /// </returns>
        protected override bool Assert(IAssertionProperty other) => throw new InvalidOperationException();
    }
}
