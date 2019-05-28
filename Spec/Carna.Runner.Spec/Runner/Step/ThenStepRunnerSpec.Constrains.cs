// Copyright (C) 2017-2019 Fievus
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
        FixtureStepResultCollection StepResults { get; }

        ThenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

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
            Given("ThenStep that has an assertion that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
            });
            Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Ready().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run has an exception and ThenStep has an assertion without Exception that returns boolean, ThenStep is not run")]
        void Ex03()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
            });
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run has an exception and ThenStep has an assertion without Exception that is Action, ThenStep is not run")]
        void Ex04()
        {
            Given("ThenStep that has an assertion without Exception that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => { });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
            });
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception and ThenStep has an assertion without Exception that returns boolean, ThenStep is run")]
        void Ex05()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception and ThenStep has an assertion without Exception that does not throw any exceptions, ThenStep is run")]
        void Ex06()
        {
            Given("ThenStep that has an assertion without Exception that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => { });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
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

        [Example("When the latest WhenStep that has already run has Ready status, ThenStep is not run")]
        void Ex11()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
            });
            Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Ready().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run does not have Ready status but other WhenStep that has already run has Ready status, ThenStep is run")]
        void Ex12()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Ready().Build()));
            Given("a result of ThenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Ready().Build()));
            Given("a result of WhenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run has Pending status, ThenStep is not run")]
        void Ex13()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
            });
            Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Pending().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When the latest WhenStep that has already run does not have Pending status but other WhenStep that has already run has Pending status, ThenStep is run")]
        void Ex14()
        {
            Given("ThenStep that has an assertion without Exception that returns true", () =>
            {
                Step = FixtureSteps.CreateThenStep(() => true);
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
            Given("a result of WhenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Pending().Build()));
            Given("a result of ThenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Pending().Build()));
            Given("a result of WhenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
