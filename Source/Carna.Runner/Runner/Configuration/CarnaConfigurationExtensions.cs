// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner.Configuration;

/// <summary>
/// Provides extension methods of the <see cref="CarnaConfigurationExtensions"/>.
/// </summary>
public static class CarnaConfigurationExtensions
{
    /// <summary>
    /// Creates a new instance of the type that is defined in the specified configuration.
    /// </summary>
    /// <typeparam name="T">The type of an instance to create.</typeparam>
    /// <param name="configuration">
    /// The configuration that defines the type of an instance to create.
    /// </param>
    /// <param name="assemblies">The assemblies in which fixtures exist.</param>
    /// <returns>
    /// The new instance of the type that is defined in the specified configuration.
    /// </returns>
    /// <exception cref="TypeNotFoundException">
    /// The type that is defined in the <paramref name="configuration"/> is not found.
    /// </exception>
    public static T Create<T>(this CarnaConfiguration configuration, IEnumerable<Assembly> assemblies) where T : class
    {
        if (string.IsNullOrEmpty(configuration.Type)) throw new TypeNotFoundException("The configuration type is not defined");

        var type = GetType(configuration.Type, assemblies);
        if (type is null) throw new TypeNotFoundException($"{configuration.Type} is not found");

        foreach (var constructor in type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic))
        {
            var parameters = constructor.GetParameters();
            if (parameters.Length == 1 && parameters[0].ParameterType == typeof(IDictionary<string, string>))
            {
                return constructor.Invoke(new object?[] { configuration.Options }) as T ?? throw new TypeNotFoundException($"{configuration.Type} is not found");
            }
        }

        return Activator.CreateInstance(type) as T ?? throw new TypeNotFoundException($"{configuration.Type} is not found");
    }

    private static Type? GetType(string typeName, IEnumerable<Assembly> assemblies)
    {
        var type = Type.GetType(typeName);
        if (type is not null) return type;

        var typeNameParts = typeName.Split(',');
        if (typeNameParts.Length < 2) return null;

        return assemblies.SelectMany(assembly => assembly.DefinedTypes)
            .FirstOrDefault(t =>
            {
                var assemblyQualifiedNameParts = t.AssemblyQualifiedName?.Split(',') ?? Array.Empty<string>();
                if (typeNameParts.Length > assemblyQualifiedNameParts.Length) return false;
                return !typeNameParts.Where((typeNamePart, index) => typeNamePart.Trim() != assemblyQualifiedNameParts[index].Trim()).Any();
            })?.AsType();
    }
}