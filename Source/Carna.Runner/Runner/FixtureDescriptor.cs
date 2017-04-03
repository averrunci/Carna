// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner
{
    /// <summary>
    /// Provides information about a fixture.
    /// </summary>
    public class FixtureDescriptor
    {
        /// <summary>
        /// Gets a name of a fixture.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a full name of a fixture.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Gets a description of a fixture.
        /// </summary>
        public string Description => Attribute.Description ?? Name;

        /// <summary>
        /// Gets a tag value of a fixture.
        /// </summary>
        public string Tag => Attribute.Tag;

        /// <summary>
        /// Gets a benefit of a fixture.
        /// </summary>
        public string Benefit => Attribute.Benefit;

        /// <summary>
        /// Gets a role of a fixture.
        /// </summary>
        public string Role => Attribute.Role;

        /// <summary>
        /// Gets a feature of a fixture.
        /// </summary>
        public string Feature => Attribute.Feature;

        /// <summary>
        /// Gets a value that indicates whether a fixture is a container fixture.
        /// </summary>
        public bool IsContainerFixture => Attribute.IsContainerFixture;

        /// <summary>
        /// Gets a value that indicates whether to run a fixture in parallel.
        /// </summary>
        public bool CanRunParallel => Attribute.CanRunParallel;

        /// <summary>
        /// Gets a type of an attribute that specifies a fixture.
        /// </summary>
        public Type FixtureAttributeType => Attribute.GetType();

        /// <summary>
        /// Gets an attribute that specifies a fixture.
        /// </summary>
        protected FixtureAttribute Attribute { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureDescriptor"/> class
        /// with the specified name and attribute that specifies a fixture.
        /// </summary>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        public FixtureDescriptor(string name, FixtureAttribute attribute) : this(name, name, attribute.RequireNonNull(nameof(attribute)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureDescriptor"/> class
        /// with the specified name, full name, and attribute that specifies a fixture.
        /// </summary>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="fullName">The full name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        public FixtureDescriptor(string name, string fullName, FixtureAttribute attribute)
        {
            Attribute = attribute.RequireNonNull(nameof(attribute));
            Name = name;
            FullName = fullName;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>The description of a fixture.</returns>
        public override string ToString() => Description;
    }
}
