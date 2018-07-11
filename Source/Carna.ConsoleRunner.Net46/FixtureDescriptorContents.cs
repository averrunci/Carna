// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;
using Carna.Runner;

namespace Carna.ConsoleRunner
{
    internal class FixtureDescriptorContents : MarshalByRefObject
    {
        public string Name { get; }
        public string FullName { get; }
        public string Description { get; }
        public string Tag { get; }
        public string Benefit { get; }
        public string Role { get; }
        public string Feature { get; }
        public bool CanRunParallel { get; }
        public string FixtureAttributeTypeName { get; }
        public string Background { get; }

        public FixtureDescriptorContents(FixtureDescriptor fixtureDescriptor)
        {
            Name = fixtureDescriptor.Name;
            FullName = fixtureDescriptor.FullName;
            Description = fixtureDescriptor.Description;
            Tag = fixtureDescriptor.Tag;
            Benefit = fixtureDescriptor.Benefit;
            Role = fixtureDescriptor.Role;
            Feature = fixtureDescriptor.Feature;
            CanRunParallel = fixtureDescriptor.CanRunParallel;
            FixtureAttributeTypeName = fixtureDescriptor.FixtureAttributeType.AssemblyQualifiedName;
            Background = fixtureDescriptor.Background;
        }
    }

    internal static class FixtureDescriptorContentsExtensions
    {
        public static FixtureDescriptor ToFixtureDescriptor(this FixtureDescriptorContents @this)
        {
            var fixtureAttributeType = Type.GetType(@this.FixtureAttributeTypeName) ??
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.DefinedTypes)
                    .FirstOrDefault(type => type.AssemblyQualifiedName == @this.FixtureAttributeTypeName) ??
                throw new InvalidOperationException($"'{@this.FixtureAttributeTypeName}' is not found");
            if (!(Activator.CreateInstance(fixtureAttributeType, @this.Description) is FixtureAttribute fixtureAttribute)) throw new InvalidOperationException($"'{@this.FixtureAttributeTypeName}' can not be instantiated");

            fixtureAttribute.Tag = @this.Tag;
            fixtureAttribute.Benefit = @this.Benefit;
            fixtureAttribute.Role = @this.Role;
            fixtureAttribute.Feature = @this.Feature;
            fixtureAttribute.CanRunParallel = @this.CanRunParallel;

            return new FixtureDescriptor(@this.Name, @this.FullName, fixtureAttribute) { Background = @this.Background };
        }
    }
}
