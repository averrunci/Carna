// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Carna.Step;

namespace Carna.Runner.Step;

/// <summary>
/// Provides the function to create a new instance that implements the <see cref="IFixtureStepRunner"/>.
/// </summary>
/// <remarks>
/// This factory retrieves types that implement the <see cref="IFixtureStepRunner"/>
/// that has a constructor that has a parameter of the <see cref="FixtureStep"/>
/// specifying the assemblies in which types that implement the <see cref="IFixtureStepRunner"/> are defined
/// and registers them to create a new instance that implements the <see cref="IFixtureStepRunner"/>.
/// </remarks>
public class FixtureStepRunnerFactory : IFixtureStepRunnerFactory
{
    private readonly IDictionary<Type, Type> stepRunnerTypes = new Dictionary<Type, Type>();

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureStepRunnerFactory"/> class.
    /// </summary>
    public FixtureStepRunnerFactory()
    {
        RegisterStepRunnerTypeFrom(typeof(FixtureStepRunnerFactory).GetTypeInfo().Assembly);
    }

    /// <summary>
    /// Creates a new instance that runs the specified fixture step.
    /// </summary>
    /// <param name="step">The fixture step to run.</param>
    /// <returns>The new instance that runs the specified fixture step.</returns>
    /// <exception cref="FixtureStepRunnerNotFoundException">
    /// The fixture step runner instance that implements the <see cref="IFixtureStepRunner"/> is not found.
    /// </exception>
    protected virtual IFixtureStepRunner Create(FixtureStep step)
    {
        if (!stepRunnerTypes.TryGetValue(step.GetType(), out var stepRunnerType)) throw new FixtureStepRunnerNotFoundException(step.GetType());

        return Activator.CreateInstance(stepRunnerType, step) as IFixtureStepRunner ?? throw new FixtureStepRunnerNotFoundException(step.GetType());
    }

    /// <summary>
    /// Registers the <see cref="IFixtureStepRunner"/> that is defined in the specified assemblies.
    /// </summary>
    /// <param name="assemblies">
    /// The assemblies in which the <see cref="IFixtureStepRunner"/> is defined.
    /// </param>
    protected virtual void RegisterFrom(IEnumerable<Assembly> assemblies)
    {
        assemblies.ForEach(RegisterFrom);
    }

    /// <summary>
    /// Registers the <see cref="IFixtureStepRunner"/> that is defined in the specified assembly.
    /// </summary>
    /// <param name="assembly">
    /// The assembly in which the <see cref="IFixtureStepRunner"/> is defined.
    /// </param>
    protected virtual void RegisterFrom(Assembly assembly)
    {
        RegisterStepRunnerTypeFrom(assembly);
    }

    private void RegisterStepRunnerTypeFrom(Assembly assembly)
        => assembly.DefinedTypes
            .Where(type => !type.IsInterface && typeof(IFixtureStepRunner).GetTypeInfo().IsAssignableFrom(type))
            .Select(type => new { StepType = RetrieveStepType(type), StepRunnerType = type.AsType() })
            .Where(stepRunnerContext => stepRunnerContext.StepType is not null)
            .ForEach(stepRunnerContext => stepRunnerTypes[stepRunnerContext.StepType!] = stepRunnerContext.StepRunnerType);

    private Type? RetrieveStepType(TypeInfo runnerType)
        => runnerType.DeclaredConstructors
            .Where(constructor => constructor.GetParameters().Length is 1)
            .Select(constructor => constructor.GetParameters()[0].ParameterType)
            .FirstOrDefault(parameterType => typeof(FixtureStep).GetTypeInfo().IsAssignableFrom(parameterType.GetTypeInfo()));

    IFixtureStepRunner IFixtureStepRunnerFactory.Create(FixtureStep step) => Create(step);
    void IFixtureStepRunnerFactory.RegisterFrom(IEnumerable<Assembly> assemblies) => RegisterFrom(assemblies);
}