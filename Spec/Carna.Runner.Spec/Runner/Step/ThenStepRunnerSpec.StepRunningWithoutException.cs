﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep without Exception")]
    class ThenStepRunnerSpec_StepRunningWithoutException : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        ThenStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_StepRunningWithoutException()
        {
            StepResults = new FixtureStepResultCollection
            {
                FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()
            };
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has an assertion that returns true is run")]
        void Ex01()
        {
            Given("ThenStep that has an assertion that returns true", () => Step = FixtureSteps.CreateThenStep(() => true));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion that returns false is run")]
        void Ex02()
        {
            Given("ThenStep that has an assertion that returns false", () => Step = FixtureSteps.CreateThenStep(() => false));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion that does not throw any exceptions is run")]
        void Ex03()
        {
            Given("ThenStep that has an assertion that does not throw any exception", () => Step = FixtureSteps.CreateThenStep(() => { }));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion that throws an exception is run")]
        void Ex04()
        {
            Given("ThenStep that has an assertion that throws an exception", () => Step = FixtureSteps.CreateThenStep(new Action(() => { throw new Exception(); })));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that does not have an assertion is run")]
        void Ex05()
        {
            Given("ThenStep that does not have an assertion", () => Step = FixtureSteps.CreateThenStep());
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Pending", () => Result.Status == FixtureStepStatus.Pending);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }
    }
}
