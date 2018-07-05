// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the fixture that indicates a requirement.
    /// </summary>
    /// <remarks>
    /// A fixture specified by this attribute is a container fixture.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class RequirementAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets a value that indicates whether a fixture specified by this attribute
        /// is a container fixture.
        /// </summary>
        public override bool IsContainerFixture => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementAttribute"/> class.
        /// </summary>
        public RequirementAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementAttribute"/> class
        /// with the specified description of a fixture specified by this attribute.
        /// </summary>
        /// <param name="description">
        /// The description of a fixture specified by this attribute.
        /// </param>
        public RequirementAttribute(string description) : base(description)
        {
        }
    }
}
