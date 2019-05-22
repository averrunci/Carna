// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions
{
    /// <summary>
    /// Provides the function to assert a property.
    /// </summary>
    public interface IAssertionProperty
    {
        /// <summary>
        /// Asserts the specified <see cref="IAssertionProperty"/>.
        /// </summary>
        /// <param name="other">The <see cref="IAssertionProperty"/> to assert.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="IAssertionProperty"/> is asserted; otherwise <c>false</c>.
        /// </returns>
        bool Assert(IAssertionProperty other);
    }
}
