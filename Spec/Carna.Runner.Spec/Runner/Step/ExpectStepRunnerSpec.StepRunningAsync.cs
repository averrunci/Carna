// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ExpectStep asynchronously")]
    class ExpectStepRunnerSpec_StepRunningAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        ExpectStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public ExpectStepRunnerSpec_StepRunningAsync()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

        [Example("When ExpectStep that has an action that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var expectStepCompleted = false;
            Given("async ExpectStep that has an action that does not throw any exceptions", () => Step = FixtureSteps.CreateExpectStep(async () =>
            {
                await Task.Delay(100);
                expectStepCompleted = true;
            }));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given ExpectStep should be awaited", () => expectStepCompleted);
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When ExpectStep that has an action that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async ExpectStep that has an action that throws an exception", () => Step = FixtureSteps.CreateExpectStep(async () =>
            {
                await Task.Delay(100);
                throw new Exception();
            }));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }
    }
}
