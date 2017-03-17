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
    /// Provides the function to build fixtures.
    /// </summary>
    public class FixtureBuilder : IFixtureBuilder
    {
        /// <summary>
        /// Builds fixtures with the specified fixture types.
        /// </summary>
        /// <param name="fixtureTypes">The fixture types to build.</param>
        /// <returns>
        /// The fixtures that is built with the specified fixture types.
        /// </returns>
        protected virtual IEnumerable<IFixture> Build(IEnumerable<TypeInfo> fixtureTypes)
            => fixtureTypes.Select(fixtureType => new
            {
                Assembly = fixtureType.Assembly,
                Namespace = fixtureType.Namespace ?? string.Empty,
                Fixture = Build(fixtureType.AsType()).EnsureParent()
            })
            .GroupBy(g => g.Assembly)
            .ToLookup(ag => ag.Key, ag => ag.ToLookup(g => g.Namespace, g => g.Fixture))
            .Select(ag =>
                new FixtureContainer(
                    ag.Key.GetName().Name,
                    ag.Key.GetCustomAttribute<AssemblyFixtureAttribute>() ?? new AssemblyFixtureAttribute(),
                    ag.SelectMany(g =>
                        g.Select(ng => new FixtureContainer(ng.Key, new NamespaceFixtureAttribute(), ng.ToList()))
                    )
                )
            );

        /// <summary>
        /// Builds a fixture with the specified fixture type.
        /// </summary>
        /// <param name="fixtureType">The fixture type to build.</param>
        /// <returns>
        /// The fixture that is built with the specified fixture type.
        /// </returns>
        protected virtual IFixture Build(Type fixtureType)
        {
            var fixtureContainer = new FixtureContainer(fixtureType);
            fixtureContainer.AddRange(
                fixtureType.GetRuntimeFields()
                    .Where(FiledFilter)
                    .Select(f => Build(f.FieldType))
            );
            fixtureContainer.AddRange(
                fixtureType.GetRuntimeProperties()
                    .Where(PropertyFilter)
                    .Select(p => Build(p.PropertyType))
            );
            fixtureContainer.AddRange(
                fixtureType.GetTypeInfo().DeclaredNestedTypes
                    .Where(NestedTypeFilter)
                    .Select(t => Build(t.AsType()))
            );
            fixtureContainer.AddRange(
                fixtureType.GetRuntimeMethods()
                    .Where(MethodFilter)
                    .Select(m => new Fixture(fixtureType, m))
            );
            return fixtureContainer;
        }

        /// <summary>
        /// Gets a filter that is applied to a field that is a target of a fixture.
        /// </summary>
        protected virtual Func<FieldInfo, bool> FiledFilter => Filter;

        /// <summary>
        /// Gets a filter that is applied to a property that is a target of a fixture.
        /// </summary>
        protected virtual Func<PropertyInfo, bool> PropertyFilter => Filter;

        /// <summary>
        /// Gets a filter that is applied to a nested type that is a target of a fixture.
        /// </summary>
        protected virtual Func<TypeInfo, bool> NestedTypeFilter => Filter;

        /// <summary>
        /// Gets a filter that is applied to a method that is a target of a fixtrue method.
        /// </summary>
        protected virtual Func<MethodInfo, bool> MethodFilter => Filter;

        private Func<MemberInfo, bool> Filter => m => m.GetCustomAttribute<FixtureAttribute>() != null;

        IEnumerable<IFixture> IFixtureBuilder.Build(IEnumerable<TypeInfo> fixtureTypes)
            => fixtureTypes == null ? Enumerable.Empty<IFixture>() : Build(fixtureTypes);
    }
}
