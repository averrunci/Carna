// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to find a fixture type.
    /// </summary>
    public class FixtureTypeFinder : IFixtureTypeFinder
    {
        /// <summary>
        /// Gets a filter to be applied to determine whether the type is the fixture type.
        /// </summary>
        protected virtual Func<TypeInfo, bool> FixtureTypeFilter => typeInfo =>
        {
            var fixtureAttribute = typeInfo.GetCustomAttribute<FixtureAttribute>();
            return !typeInfo.IsNested && fixtureAttribute != null && fixtureAttribute.IsRootFixture;
        };

        /// <summary>
        /// Finds fixture types with the specified assemblies.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies in which fixture types exist.
        /// </param>
        /// <returns>
        /// The fixture types in the specified assemblies.
        /// </returns>
        protected virtual IEnumerable<TypeInfo> Find(IEnumerable<Assembly> assemblies)
            => assemblies.SelectMany(assembly => assembly.DefinedTypes.Where(FixtureTypeFilter));

        IEnumerable<TypeInfo> IFixtureTypeFinder.Find(IEnumerable<Assembly> assemblies)
            => assemblies == null ? Enumerable.Empty<TypeInfo>() : Find(assemblies);
    }
}
