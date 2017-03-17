// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs GivenStep")]
    class GivenStepRunnerSpec_StepRunning : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private GivenStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public GivenStepRunnerSpec_StepRunning()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(GivenStep step) => new GivenStepRunner(step);

        [Example("When GivenStep that has an arrangement that does not throw any exceptions is run")]
        void Ex01()
        {
            Given("GivenStep that has an arrangement that does not throw any exceptions", () => Step = FixtureSteps.CreateGivenStep(() => { }));
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given GivenStep", () => Result.Step == Step);
        }

        [Example("When GivenStep that has an arrangement that throws an exception is run")]
        void Ex02()
        {
            Given("GivenStep that has an arrangement that throws an exception", () => Step = FixtureSteps.CreateGivenStep(() => throw new Exception()));
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given GivenStep", () => Result.Step == Step);
        }

        [Example("When GivenStep that does not have an arrangement is run")]
        void Ex03()
        {
            Given("GivenStep that does not have an arrangement", () => Step = FixtureSteps.CreateGivenStep());
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Pending", () => Result.Status == FixtureStepStatus.Pending);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given GivenStep", () => Result.Step == Step);
        }
    }
}
