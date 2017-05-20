﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep with Typed Exception")]
    class ThenStepRunnerSpec_StepRunningWithTypedException : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private ThenStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_StepRunningWithTypedException()
        {
            StepResults = new FixtureStepResultCollection();
            StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new ArgumentNullException()).Build());
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has the type of an exception that is valid")]
        void Ex01()
        {
            Given("ThenStep that has the type of an exceptoin that is valid", () => Step = FixtureSteps.CreateThenStep<ArgumentNullException>());
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has the type of an exception that is invalid")]
        void Ex02()
        {
            Given("ThenStep that has tye type of an exception that is invalid", () => Step = FixtureSteps.CreateThenStep<InvalidOperationException>());
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that returns true is run")]
        void Ex03()
        {
            Given("ThenStep that has an assertion with Exception that returns true", () => Step = FixtureSteps.CreateThenStep<ArgumentNullException>(exc => true));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that returns false is run")]
        void Ex04()
        {
            Given("ThenStep that has an assertion with Exception that returns false", () => Step = FixtureSteps.CreateThenStep<ArgumentNullException>(exc => false));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions is run")]
        void Ex05()
        {
            Given("ThenStep that has an assertion with Exception that does not throw any exception", () => Step = FixtureSteps.CreateThenStep<ArgumentNullException>(exc => { }));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When ThenStep that has an assertion with Exception that throws an exception is run")]
        void Ex06()
        {
            Given("ThenStep that has an assertion with Exception that throws an exception", () => Step = FixtureSteps.CreateThenStep(new Action<ArgumentNullException>(exc => throw new Exception())));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }
    }
}