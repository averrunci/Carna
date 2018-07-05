// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs WhenStep")]
    class WhenStepRunnerSpec_StepRunning : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public WhenStepRunnerSpec_StepRunning()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When WhenStep that has an action that does not throw any exceptions is run")]
        void Ex01()
        {
            Given("WhenStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateWhenStep(() => { }));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }

        [Example("When WhenStep that has an action that throws an exception is run")]
        void Ex02()
        {
            Given("WhenStep that has an action that throws an exception", () => Step = FixtureSteps.CreateWhenStep(() => throw new Exception()));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }

        [Example("When WhenStep that does not have an action is run")]
        void Ex03()
        {
            Given("WhenStep that does not have an action", () => Step = FixtureSteps.CreateWhenStep());
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Pending", () => Result.Status == FixtureStepStatus.Pending);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }
    }
}
