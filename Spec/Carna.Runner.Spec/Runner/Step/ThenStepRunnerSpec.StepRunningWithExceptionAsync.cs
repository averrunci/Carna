// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep with Exception asynchronously")]
    class ThenStepRunnerSpec_StepRunningWithExceptionAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        ThenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_StepRunningWithExceptionAsync()
        {
            StepResults = new FixtureStepResultCollection
            {
                FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new InvalidOperationException()).Build()
            };
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var thenStepCompleted = false;
            Given("async ThenStep that has an assertion with Exception that does not throw any exceptions", () => Step = FixtureSteps.CreateThenStep(async exc =>
            {
                await Task.Delay(100);
                thenStepCompleted = true;
            }));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given ThenStep should be awaited", () => thenStepCompleted);
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async ThenStep that has an assertion with Exception that throws an exception", () => Step = FixtureSteps.CreateThenStep(async exc =>
            {
                await Task.Delay(100);
                throw new Exception();
            }));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }
    }
}
