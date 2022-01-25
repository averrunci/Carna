// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Carna.Runner.Step;
using Carna.Step;

namespace Carna.Runner;

/// <summary>
/// Represents a fixture.
/// </summary>
public class Fixture : FixtureBase
{
    /// <summary>
    /// Gets a fixture method.
    /// </summary>
    protected MethodInfo FixtureMethod { get; }

    /// <summary>
    /// Gets sample data.
    /// </summary>
    protected object?[]? SampleData { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fixture"/> class
    /// with the specified type of an instance of a fixture and fixture
    /// method.
    /// </summary>
    /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
    /// <param name="fixtureMethod">The fixture method.</param>
    public Fixture(Type fixtureInstanceType, MethodInfo fixtureMethod) : base(fixtureInstanceType, fixtureMethod)
    {
        FixtureMethod = fixtureMethod;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fixture"/> class
    /// with the specified type of an instance of a fixture and fixture
    /// method and context of a sample.
    /// </summary>
    /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
    /// <param name="fixtureMethod">The fixture method.</param>
    /// <param name="sample">The context of a sample.</param>
    public Fixture(Type fixtureInstanceType, MethodInfo fixtureMethod, SampleContext sample) : base(fixtureInstanceType, fixtureMethod, new SampleFixtureAttribute(sample.Description))
    {
        FixtureMethod = fixtureMethod;
        SampleData = sample.Data;
    }

    /// <summary>
    /// Runs a fixture with the specified start time, filter, step runner factory,
    /// and a value that indicates whether to run a fixture in parallel.
    /// </summary>
    /// <param name="startTime">The start time at which a fixture is run.</param>
    /// <param name="filter">
    /// The filter that determines whether to run a fixture.
    /// </param>
    /// <param name="stepRunnerFactory">
    /// The factory to create a step runner.
    /// </param>
    /// <param name="parallel">
    /// <c>true</c> if a fixture is run in parallel; otherwise, <c>false</c>.
    /// </param>
    /// <returns>
    /// The result of a fixture running.
    /// </returns>
    /// <exception cref="FixtureInstanceNotInstantiateException">
    /// The instance of the fixture is not instantiate.
    /// </exception>
    protected override FixtureResult Run(DateTime startTime, IFixtureFilter? filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        => Run(stepRunnerFactory, FixtureResult.Of(FixtureDescriptor).StartAt(startTime));

    /// <summary>
    /// Retrieves around fixture attributes that specify a fixture.
    /// </summary>
    /// <returns>The around fixture attributes that specify a fixture.</returns>
    protected override IEnumerable<AroundFixtureAttribute> RetrieveAroundFixtureAttributes()
        => FixtureMethod.GetCustomAttributes<AroundFixtureAttribute>();

    private FixtureResult Run(IFixtureStepRunnerFactory stepRunnerFactory, FixtureResult.Builder result)
        => CanRunInSta(FixtureMethod) ? RunInSta(() => RunCore(stepRunnerFactory, result)) : RunCore(stepRunnerFactory, result);

    private FixtureResult RunCore(IFixtureStepRunnerFactory stepRunnerFactory, FixtureResult.Builder result)
    {
        var fixtureInstance = CreateFixtureInstance();
        if (fixtureInstance is null) throw new FixtureInstanceNotInstantiateException($"The instance of {FixtureDescriptor.Name} is not instantiate.");

        return fixtureInstance is IFixtureSteppable fixtureSteppable ? RunCore(fixtureInstance, fixtureSteppable, stepRunnerFactory, result) : RunCore(fixtureInstance, result);
    }

    private FixtureResult RunCore(object fixtureInstance, FixtureResult.Builder result)
    {
        RunCore(fixtureInstance);
        return RecordEndTime(result).Passed();
    }

    private FixtureResult RunCore(object fixtureInstance, IFixtureSteppable fixtureSteppable, IFixtureStepRunnerFactory stepRunnerFactory, FixtureResult.Builder result)
    {
        var fixtureStepper = new FixtureStepper(stepRunnerFactory);
        fixtureStepper.FixtureStepRunning += (_, e) => OnFixtureStepRunning(e);
        fixtureStepper.FixtureStepRun += (_, e) => OnFixtureStepRun(e);
        fixtureSteppable.Stepper = fixtureStepper;

        RunCore(fixtureInstance);
        return RecordEndTime(result).FinishedWith(fixtureStepper.Results);
    }

    private void RunCore(object fixtureInstance)
    {
        void PerformFixtureMethod() => (FixtureMethod.Invoke(fixtureInstance, SampleData) as Task)?.GetAwaiter().GetResult();

        if (fixtureInstance is IDisposable disposable)
        {
            using (disposable) PerformFixtureMethod();
        }
        else
        {
            PerformFixtureMethod();
        }
    }
}