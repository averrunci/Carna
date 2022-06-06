// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner;

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
                Fixture = Build(fixtureType.AsType())
            })
            .GroupBy(g => g.Assembly)
            .ToLookup(ag => ag.Key, ag => ag.ToLookup(g => g.Namespace, g => g.Fixture))
            .Select(ag =>
                new FixtureContainer(
                    ag.Key.GetName().Name ?? ag.Key.ToString(),
                    ag.Key.GetCustomAttribute<AssemblyFixtureAttribute>() ?? new AssemblyFixtureAttribute(),
                    ag.SelectMany(g =>
                        g.Select(ng => new FixtureContainer(ng.Key, new NamespaceFixtureAttribute(), ng.ToList()))
                    )
                ) as IFixture
            )
            .Select(fixture => fixture.EnsureParent());

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
            fixtureType.GetCustomAttribute<FixtureAttribute>()?.Fixtures
                .Where(TypeFilter)
                .Select(Build) ?? Enumerable.Empty<IFixture>()
        );
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
                .Select(m => Build(fixtureType, m))
        );
        return fixtureContainer;
    }

    /// <summary>
    /// Builds a fixture with the specified fixture type and fixture method type.
    /// </summary>
    /// <param name="fixtureType">The fixture type to build.</param>
    /// <param name="fixtureMethod">The fixture method type to build.</param>
    /// <returns>
    /// The fixture that is built with the specified fixture type and fixture method type.
    /// </returns>
    protected virtual IFixture Build(Type fixtureType, MethodInfo fixtureMethod)
    {
        var samples = RetrieveSamples(fixtureMethod).ToList();
        if (samples.IsEmpty()) return new Fixture(fixtureType, fixtureMethod);

        return new FixtureContainer(fixtureType, fixtureMethod, samples.Select(sample => new Fixture(fixtureType, fixtureMethod, sample)));
    }

    /// <summary>
    /// Retrieves samples that is specified to the specified fixture method.
    /// </summary>
    /// <param name="fixtureMethod">The fixture method.</param>
    /// <returns>The samples to be retrieved.</returns>
    /// <exception cref="InvalidSampleDataSourceTypeException">
    /// The source type of the sample data does not implement the <see cref="ISampleDataSource"/>.
    /// </exception>
    protected virtual IEnumerable<SampleContext> RetrieveSamples(MethodInfo fixtureMethod)
        => fixtureMethod.GetCustomAttributes<SampleAttribute>().SelectMany(sample => RetrieveSamples(fixtureMethod, sample));

    private IEnumerable<SampleContext> RetrieveSamples(MethodInfo fixtureMethod, SampleAttribute sample)
        => sample.Source is null ? RetrieveSampleFromData(fixtureMethod, sample) : RetrieveSampleFromSource(fixtureMethod, sample.Source);

    private IEnumerable<SampleContext> RetrieveSampleFromData(MethodInfo fixtureMethod, SampleAttribute sample)
        => new[] { CreateSampleContext(fixtureMethod, sample) };

    private SampleContext CreateSampleContext(MethodInfo fixtureMethod, SampleAttribute sample)
    {
        var items = fixtureMethod.GetParameters().Select(parameter => new SampleContext.Item(parameter.Name ?? parameter.ToString())).ToArray();
        for (var index = 0; index < Math.Min(sample.Data.Length, items.Length); ++index)
        {
            items[index].Value = sample.Data[index];
        }
        return new SampleContext(sample.Description, items);
    }

    private IEnumerable<SampleContext> RetrieveSampleFromSource(MethodInfo fixtureMethod, Type sourceType)
    {
        if (!typeof(ISampleDataSource).GetTypeInfo().IsAssignableFrom(sourceType.GetTypeInfo()))
        {
            throw new InvalidSampleDataSourceTypeException(sourceType, $"{sourceType} must implement {typeof(ISampleDataSource)}");
        }

        var sourceConstructor = sourceType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => !c.GetParameters().Any());
        if (sourceConstructor is null)
        {
            throw new InvalidSampleDataSourceTypeException(sourceType, $"{sourceType} must have a parameterless constructor");
        }

        if (sourceConstructor.Invoke(null) is not ISampleDataSource source)
        {
            throw new InvalidOperationException($"{sourceType} must implement {typeof(ISampleDataSource)}");
        }

        return source.GetData().OfType<object>().Select(sample => CreateSampleContext(fixtureMethod, sample));
    }

    private SampleContext CreateSampleContext(MethodInfo fixtureMethod, object sample)
        => new(
            sample.GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == "Description")?.GetValue(sample) as string,
            fixtureMethod.GetParameters().Select(parameter =>
                new SampleContext.Item(parameter.Name ?? parameter.ToString())
                {
                    Value = sample.GetType().GetRuntimeProperties()
                        .FirstOrDefault(p => string.Equals(p.Name, parameter.Name, StringComparison.CurrentCultureIgnoreCase))?
                        .GetValue(sample)
                }
            )
        );

    /// <summary>
    /// Gets a filter that is applied to a type that is a target of a fixture.
    /// </summary>
    protected virtual Func<Type, bool> TypeFilter => Filter;

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
    /// Gets a filter that is applied to a method that is a target of a fixture method.
    /// </summary>
    protected virtual Func<MethodInfo, bool> MethodFilter => Filter;

    private Func<MemberInfo, bool> Filter => m => m.GetCustomAttribute<FixtureAttribute>() != null;

    IEnumerable<IFixture> IFixtureBuilder.Build(IEnumerable<TypeInfo> fixtureTypes) => Build(fixtureTypes);
}