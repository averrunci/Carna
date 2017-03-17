// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep with Exception")]
    class ThenStepRunnerSpec_StepRunningWithException : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private ThenStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_StepRunningWithException()
        {
            StepResults = new FixtureStepResultCollection();
            StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build());
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has an assertion with Exception that returns true is run")]
        void Ex01()
        {
            Given("ThenStep that has an assertion with Exception that returns true", () => Step = FixtureSteps.CreateThenStep(exc => true));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that returns false is run")]
        void Ex02()
        {
            Given("ThenStep that has an assertion with Exception that returns false", () => Step = FixtureSteps.CreateThenStep(exc => false));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions is run")]
        void Ex03()
        {
            Given("ThenStep that has an assertion with Exception that does not throw any exception", () => Step = FixtureSteps.CreateThenStep(exc => { }));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that throws an exception is run")]
        void Ex04()
        {
            Given("ThenStep that has an assertion with Exception that throws an exception", () => Step = FixtureSteps.CreateThenStep(new Action<Exception>(exc => throw new Exception())));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }
    }
}
