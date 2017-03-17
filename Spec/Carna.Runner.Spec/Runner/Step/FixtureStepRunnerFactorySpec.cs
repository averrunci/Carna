// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Specification("FixtureStepRunnerFactory Spec")]
    class FixtureStepRunnerFactorySpec : FixtureSteppable
    {
        private IFixtureStepRunnerFactory SteprunnerFactory { get; }

        public FixtureStepRunnerFactorySpec()
        {
            SteprunnerFactory = new FixtureStepRunnerFactory();
        }

        [Example("Creates the default registered step runner")]
        void Ex01()
        {
            Expect("the step runner of GivenStep should be GivenStepRunner", () => SteprunnerFactory.Create(FixtureSteps.CreateGivenStep()).GetType() == typeof(GivenStepRunner));
            Expect("the step runner of WhenStep should be WhenStepRunner", () => SteprunnerFactory.Create(FixtureSteps.CreateWhenStep()).GetType() == typeof(WhenStepRunner));
            Expect("the step runner of ThenStep should be ThenStepRunner", () => SteprunnerFactory.Create(FixtureSteps.CreateThenStep()).GetType() == typeof(ThenStepRunner));
            Expect("the step runner of ExpectStep should be ExpectStepRunner", () => SteprunnerFactory.Create(FixtureSteps.CreateExpectStep()).GetType() == typeof(ExpectStepRunner));
        }

        [Example("Throws an exceptin when the unregistered step runner is created")]
        void Ex02()
        {
            When("the unregistered step runner is created", () => SteprunnerFactory.Create(FixtureSteps.CreateStep<StepRunnerNotRegisteredStep>()));
            Then("FixtureStepRunnerNotFoundException should be thrown", exc => exc.GetType() == typeof(FixtureStepRunnerNotFoundException));
        }

        private class StepRunnerNotRegisteredStep : FixtureStep
        {
            public StepRunnerNotRegisteredStep(string description, Type callerType, string callerMemberName, string callerFilePath, int callerLineNumber) : base(description, callerType, callerMemberName, callerFilePath, callerLineNumber)
            {
            }
        }
    }
}
