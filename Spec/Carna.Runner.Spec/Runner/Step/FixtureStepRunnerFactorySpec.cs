// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Specification("FixtureStepRunnerFactory Spec")]
class FixtureStepRunnerFactorySpec : FixtureSteppable
{
    IFixtureStepRunnerFactory StepRunnerFactory { get; }

    public FixtureStepRunnerFactorySpec()
    {
        StepRunnerFactory = new FixtureStepRunnerFactory();
    }

    [Example("Creates the default registered step runner")]
    void Ex01()
    {
        Expect("the step runner of GivenStep should be GivenStepRunner", () => StepRunnerFactory.Create(FixtureSteps.CreateGivenStep()).GetType() == typeof(GivenStepRunner));
        Expect("the step runner of WhenStep should be WhenStepRunner", () => StepRunnerFactory.Create(FixtureSteps.CreateWhenStep()).GetType() == typeof(WhenStepRunner));
        Expect("the step runner of ThenStep should be ThenStepRunner", () => StepRunnerFactory.Create(FixtureSteps.CreateThenStep()).GetType() == typeof(ThenStepRunner));
        Expect("the step runner of ExpectStep should be ExpectStepRunner", () => StepRunnerFactory.Create(FixtureSteps.CreateExpectStep()).GetType() == typeof(ExpectStepRunner));
    }

    [Example("Throws an exception when the unregistered step runner is created")]
    void Ex02()
    {
        When("the unregistered step runner is created", () => StepRunnerFactory.Create(FixtureSteps.CreateStep<StepRunnerNotRegisteredStep>()));
        Then("FixtureStepRunnerNotFoundException should be thrown", exc => exc.GetType() == typeof(FixtureStepRunnerNotFoundException));
    }

    private class StepRunnerNotRegisteredStep : FixtureStep
    {
        public StepRunnerNotRegisteredStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
        {
        }
    }
}