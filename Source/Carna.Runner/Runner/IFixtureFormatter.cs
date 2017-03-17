// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to format a description of a fixture and fixture step.
    /// </summary>
    public interface IFixtureFormatter
    {
        /// <summary>
        /// Formats a fixture with the specified descriptor of it.
        /// </summary>
        /// <param name="descriptor">The descriptor of the fixture.</param>
        /// <returns>The formatted description.</returns>
        FormattedDescription FormatFixture(FixtureDescriptor descriptor);

        /// <summary>
        /// Formats a fixture step with the specified step.
        /// </summary>
        /// <param name="step">The fixture step.</param>
        /// <returns>The formatted description.</returns>
        FormattedDescription FormatFixtureStep(FixtureStep step);
    }
}
