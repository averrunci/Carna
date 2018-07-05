// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs WhenStep asynchronously")]
    class WhenStepRunnerSpec_StepRunningAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public WhenStepRunnerSpec_StepRunningAsync()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When WhenStep that has an action that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var whenStepCompleted = false;
            Given("async WhenStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateWhenStep(async () =>
            {
                await Task.Delay(100);
                whenStepCompleted = true;
            }));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given WhenStep should be awaited", () => whenStepCompleted);
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }

        [Example("When WhenStep that has an action that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async WhenStep that has an action that throws an exception", () => Step = FixtureSteps.CreateWhenStep(async () =>
            {
                await Task.Delay(100);
                throw new Exception();
            }));
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given WhenStep", () => Result.Step == Step);
        }
    }
}
