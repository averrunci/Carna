// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("ThenStep running constrains")]
    class ThenStepRunnerSpec_Constrains : FixtureSteppable
    {
        private FixtureStepResultCollection StepResults { get; }

        private ThenStep Step { get; set; }
        private FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_Constrains()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When WhenStep is not run, an exception is thrown")]
        void Ex01()
        {
            Given("ThenStep that has an assertion that returns true", () => Step = FixtureSteps.CreateThenStep(() => true));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("InvalidFixtureStepException should be thrown", exc => exc.GetType() == typeof(InvalidFixtureStepException));
        }

        [Example("When GivenStep that has already run has an exception, ThenStep is not run")]
        void Ex02()
        {
            Given("ThenStep that has an assertion that returns true", () => Step = FixtureSteps.CreateThenStep(() => true));
            Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When the latest WhenStep that has already run has an exception and ThenStep has an assertion without Exception that returns boolean, ThenStep is not run")]
        void Ex03()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () => Step = FixtureSteps.CreateThenStep(() => true));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When the latest WhenStep that has already run has an exception and ThenStep has an assertion without Exception that is Action, ThenStep is not run")]
        void Ex04()
        {
            Given("ThenStep that has an assertion without Exception that does not throw any exceptions", () => Step = FixtureSteps.CreateThenStep(() => { }));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Ready", () => Result.Status == FixtureStepStatus.Ready);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ExpectStep", () => Result.Step == Step);
        }

        [Example("When the latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception and ThenStep has an assertion without Exception that returns boolean, ThenStep is run")]
        void Ex05()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () => Step = FixtureSteps.CreateThenStep(() => true));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When the latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception and ThenStep has an assertion without Exception that does not throw any exceptions, ThenStep is run")]
        void Ex06()
        {
            Given("ThenStep that has an assertion without Exception that does not throw any exceptions", () => Step = FixtureSteps.CreateThenStep(() => { }));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);
        }

        [Example("When the latest WhenStep has an exception and ThenStep that has an assertion with Exception that returns boolean is Passed, the latest WhenStep is changed to Passed")]
        void Ex07()
        {
            Given("ThenStep that has an assertion with Exception that returns boolean", () => Step = FixtureSteps.CreateThenStep(exc => true));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result of the latest WhenStep should be Passed", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Status == FixtureStepStatus.Passed);
            Then("the exception of the result of the latest WhenStep should be null", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Exception == null);
        }

        [Example("When the latest WhenStep has an exception and ThenStep that has an assertion with Exception that is Action is Passed, the latest WhenStep is changed to Passed")]
        void Ex08()
        {
            Given("ThenStep that has an assertion with Exception that returns boolean", () => Step = FixtureSteps.CreateThenStep(exc => { }));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result of the latest WhenStep should be Passed", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Status == FixtureStepStatus.Passed);
            Then("the exception of the result of the latest WhenStep should be null", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Exception == null);
        }

        [Example("When the latest WhenStep has an exception and ThenStep that has an assertion with Exception that returns boolean is Failed, the latest WhenStep is changed to Passed")]
        void Ex09()
        {
            Given("ThenStep that has an assertion with Exception that returns boolean", () => Step = FixtureSteps.CreateThenStep(exc => false));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result of the latest WhenStep should be Passed", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Status == FixtureStepStatus.Passed);
            Then("the exception of the result of the latest WhenStep should be null", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Exception == null);
        }

        [Example("When the latest WhenStep has an exception and ThenStep that has an assertion with Exception that is Action is Failed, the latest WhenStep is changed to Passed")]
        void Ex10()
        {
            Given("ThenStep that has an assertion with Exception that returns boolean", () => Step = FixtureSteps.CreateThenStep(new Action<Exception>(exc => throw new Exception())));
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result of the latest WhenStep should be Passed", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Status == FixtureStepStatus.Passed);
            Then("the exception of the result of the latest WhenStep should be null", () => StepResults.GetLatestStepResultsOf<WhenStep>().First().Exception == null);
        }
    }
}
