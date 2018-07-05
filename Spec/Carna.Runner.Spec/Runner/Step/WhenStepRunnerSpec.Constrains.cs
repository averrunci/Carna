// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("WhenStep running constrains")]
    class WhenStepRunnerSpec_Constrains : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public WhenStepRunnerSpec_Constrains()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When GivenStep that has already run has an exception, WhenStep is not run")]
        void Ex01()
        {
            Given("WhenStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateWhenStep(() => { }));
            Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }

        [Example("When latest WhenStep that has already run has an exception, WhenStep is not run")]
        void Ex02()
        {
            Given("WhenStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateWhenStep(() => { }));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }

        [Example("When latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception, WhenStep is run")]
        void Ex03()
        {
            Given("WhenStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateWhenStep(() => { }));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }
    }
}
