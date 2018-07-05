// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs GivenStep asynchronously")]
    class GivenStepRunnerSpec_StepRunningAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        GivenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public GivenStepRunnerSpec_StepRunningAsync()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(GivenStep step) => new GivenStepRunner(step);

        [Example("When GivenStep that has an arrangement that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var givenStepCompleted = false;
            Given("async GivenStep that has an arrangement that does not throw any exceptions", () => Step = FixtureSteps.CreateGivenStep(async () =>
            {
                await Task.Delay(100);
                givenStepCompleted = true;
            }));
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given GivenStep should be awaited", () => givenStepCompleted);
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given GivenStep", () => Result.Step == Step);
        }

        [Example("When GivenStep that has an arrangement that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async GivenStep that has an arrangement that throws an exception", () => Step = FixtureSteps.CreateGivenStep(async () =>
            {
                await Task.Delay(100);
                throw new Exception();
            }));
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given GivenStep", () => Result.Step == Step);
        }
    }
}
