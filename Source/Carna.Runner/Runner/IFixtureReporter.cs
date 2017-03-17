// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;

namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to report a result of a fixture running.
    /// </summary>
    public interface IFixtureReporter
    {
        /// <summary>
        /// Gets or sets a formatter of a fixture.
        /// </summary>
        IFixtureFormatter FixtureFormatter { get; set; }

        /// <summary>
        /// Reports a result of a fixture running with the specified fixture results.
        /// </summary>
        /// <param name="results">The fixture running results.</param>
        void Report(IEnumerable<FixtureResult> results);
    }
}
