// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ExpectStep")]
    class ExpectStepRunnerSpec_StepRunning : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private ExpectStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public ExpectStepRunnerSpec_StepRunning()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

        [Example("When ExpectStep that has an assertion that returns true is run")]
        void Ex01()
        {
            Given("ExpectStep that has an assertion that returns true", () => Step = FixtureSteps.CreateExpectStep(() => true));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When ExpectStep that has an assertion that returns false is run")]
        void Ex02()
        {
            Given("ExpectStep that has an assertion that returns true", () => Step = FixtureSteps.CreateExpectStep(() => false));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When ExpectStep that has an action that does not throw any exceptions is run")]
        void Ex03()
        {
            Given("ExpectStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateExpectStep(() => { }));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When ExpectStep that has an action that throws an exception is run")]
        void Ex04()
        {
            Given("ExpectStep that has an action that throws an exception", () => Step = FixtureSteps.CreateExpectStep(new Action(() => throw new Exception())));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When ExpectStep that does not have an assertion and an action is run")]
        void Ex05()
        {
            Given("ExpectStep that does not have an assertion and an action", () => Step = FixtureSteps.CreateExpectStep());
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Pending", () => Result.Status == FixtureStepStatus.Pending);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }
    }
}
