// Copyright (C) 2017-2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// A base attribute that is applied to a fixture used by Carna.
    /// </summary>
    public abstract class FixtureAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value that indicates whether a fixture specified by this attribute
        /// is a root fixture.
        /// </summary>
        public bool IsRootFixture { get; set; }

        /// <summary>
        /// Gets a value that indicates whether a fixture specified by this attribute
        /// is a container fixture.
        /// </summary>
        public abstract bool IsContainerFixture { get; }

        /// <summary>
        /// Gets a value that indicates whether fixtures in a fixture specified by this attribute
        /// can be run in parallel.
        /// </summary>
        public bool CanRunParallel { get; set; }

        /// <summary>
        /// Gets a description of a fixture specified by this attribute.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets a tag about a fixture specified by this attribute.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets a benefit about a fixture specified by this attribute.
        /// </summary>
        public string Benefit { get; set; }

        /// <summary>
        /// Gets a role about a fixture specified by this attribute.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets a feature about a fixture specified by this attribute.
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// Gets a value that indicates whether to run a fixture in a single thread apartment.
        /// </summary>
        public bool RequiresSta { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureAttribute"/> class.
        /// </summary>
        protected FixtureAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureAttribute"/> class
        /// with the specified description of a fixture specified by this attribute.
        /// </summary>
        /// <param name="description">
        /// The description of a fixture specified by this attribute.
        /// </param>
        protected FixtureAttribute(string description)
        {
            Description = description;
        }
    }
}
