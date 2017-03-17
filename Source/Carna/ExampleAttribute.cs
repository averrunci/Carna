// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the fixture that indicates an example.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExampleAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets a value that indicates whether a fixture specified by this attribute
        /// is a container fixture.
        /// </summary>
        public override bool IsContainerFixture => false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleAttribute"/> class.
        /// </summary>
        public ExampleAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleAttribute"/> class
        /// with the specified descrition of a fixture specified by this attribute.
        /// </summary>
        /// <param name="description">
        /// The description of a fixture specified by this attribute.
        /// </param>
        public ExampleAttribute(string description) : base(description)
        {
        }
    }
}
