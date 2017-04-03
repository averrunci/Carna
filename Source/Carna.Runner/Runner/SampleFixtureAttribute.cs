// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    /// <summary>
    /// Specifies the fixture that indicates a sample.
    /// </summary>
    public class SampleFixtureAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets a value that indicates whether a fixture specified by this attribute
        /// is a container fixture.
        /// </summary>
        public override bool IsContainerFixture => false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleFixtureAttribute"/> class
        /// with the specified description of a fixture.
        /// </summary>
        /// <param name="description">The description of a fixture.</param>
        public SampleFixtureAttribute(string description) : base(description)
        {
        }
    }
}
