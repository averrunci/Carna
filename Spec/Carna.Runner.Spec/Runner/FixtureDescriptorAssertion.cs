// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Assertions;

namespace Carna.Runner
{
    internal class FixtureDescriptorAssertion : AssertionObject
    {
        [AssertionProperty]
        protected string Description { get; }
        
        [AssertionProperty]
        protected string Name { get; }

        [AssertionProperty]
        protected string FullName { get; }

        [AssertionProperty]
        protected Type FixtureAttributeType { get; }

        public FixtureDescriptorAssertion(string description, string name, string fullName, Type fixtureAttributeType)
        {
            Description = description;
            Name = name;
            FullName = fullName;
            FixtureAttributeType = fixtureAttributeType;
        }

        public static FixtureDescriptorAssertion Of(string description, string name, string fullName, Type fixtureAttributeType) => new FixtureDescriptorAssertion(description, name, fullName, fixtureAttributeType);
        public static FixtureDescriptorAssertion Of(FixtureDescriptor descriptor) => new FixtureDescriptorAssertion(descriptor.Description, descriptor.Name, descriptor.FullName, descriptor.FixtureAttributeType);
    }

    internal class FixtureDescriptorWithBackgroundAssertion : FixtureDescriptorAssertion
    {
        [AssertionProperty]
        string Background { get; }

        public FixtureDescriptorWithBackgroundAssertion(string description, string name, string fullName, Type fixtureAttributeType, string background) : base(description, name, fullName, fixtureAttributeType)
        {
            Background = background;
        }

        public static FixtureDescriptorWithBackgroundAssertion Of(string description, string name, string fullName, Type fixtureAttributeType, string background) => new FixtureDescriptorWithBackgroundAssertion(description, name, fullName, fixtureAttributeType, background);
        public static FixtureDescriptorWithBackgroundAssertion Of(FixtureDescriptor descriptor) => new FixtureDescriptorWithBackgroundAssertion(descriptor.Description, descriptor.Name, descriptor.FullName, descriptor.FixtureAttributeType, descriptor.Background);
    }
}
