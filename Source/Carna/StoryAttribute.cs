// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the fixture that indicates a story.
    /// </summary>
    /// <remarks>
    /// A fixture specified by this attribute is a container fixture.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class StoryAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets a value that indicates whether a fixture specified by this attribute
        /// is a container fixture.
        /// </summary>
        public override bool IsContainerFixture => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryAttribute"/> class.
        /// </summary>
        public StoryAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryAttribute"/> class
        /// with the specified description of a fixture specified by this attribute.
        /// </summary>
        /// <param name="description">
        /// The description of a fixture specified by this attribute.
        /// </param>
        public StoryAttribute(string description) : base(description)
        {
        }
    }
}
