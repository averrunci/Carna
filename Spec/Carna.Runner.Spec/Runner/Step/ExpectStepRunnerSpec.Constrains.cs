﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("ExpectStep running constrains")]
    class ExpectStepRunnerSpec_Constrains : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private ExpectStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public ExpectStepRunnerSpec_Constrains()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

        [Example("When GivenStep that has already run has an exception, ExpectStep is not run")]
        void Ex01()
        {
            Given("ExpectStep that has an assertion that returns true", () => Step = FixtureSteps.CreateExpectStep(() => true));
            Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When latest WhenStep that has already run has an exception, ExpectStep is not run")]
        void Ex02()
        {
            Given("ExpectStep that has an assertion that returns true", () => Step = FixtureSteps.CreateExpectStep(() => true));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception, ExpectStep is run")]
        void Ex03()
        {
            Given("ExpectStep that has an assertion that return true", () => Step = FixtureSteps.CreateExpectStep(() => true));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }
    }
}
