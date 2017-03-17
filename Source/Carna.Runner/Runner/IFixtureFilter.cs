// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to filter a fixture.
    /// </summary>
    public interface IFixtureFilter
    {
        /// <summary>
        /// Determines wheter to run a fixture of the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor of the fixture.</param>
        /// <returns>
        /// <c>true</c> if a fixture is run; otherwise, <c>false</c>.
        /// </returns>
        bool Accept(FixtureDescriptor descriptor);
    }
}
