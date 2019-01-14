// Copyright (C) 2017-2019 Fievus
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
        FixtureStepResultCollection StepResults { get; }

        Exception AssertedException { get; } = new InvalidOperationException();
        ThenStep Step { get; set; }
        ThenStep NextStep { get; set; }
        FixtureStepResult Result { get; set; }

        public ThenStepRunnerSpec_StepRunningWithException()
        {
            StepResults = new FixtureStepResultCollection
            {
                FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(AssertedException).Build()
            };
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

        [Example("When ThenStep that has an assertion with Exception that returns true and the next ThenStep that asserts the Exception that is thrown at WhenStep are run")]
        void Ex05()
        {
            Given("ThenStep that has an assertion with Exception that returns true", () => Step = FixtureSteps.CreateThenStep(exc => true));
            Given("The next ThenStep that asserts the Exception that is thrown at WhenStep", () => NextStep = FixtureSteps.CreateThenStep(exc => exc == AssertedException));
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);

            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given next ThenStep", () => Result.Step == NextStep);
        }

        [Example("When ThenStep that has an assertion with Exception that returns true and the next ThenStep that asserts the Exception that is not thrown at WhenStep are run")]
        void Ex06()
        {
            Given("ThenStep that has an assertion with Exception that returns true", () => Step = FixtureSteps.CreateThenStep(exc => true));
            Given("The next ThenStep that asserts the Exception that is not thrown at WhenStep", () => NextStep = FixtureSteps.CreateThenStep(exc => exc != null && exc == new Exception()));
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);

            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given next ThenStep", () => Result.Step == NextStep);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exception and the next ThenStep that asserts the Exception that is thrown at WhenStep are run")]
        void Ex07()
        {
            Given("ThenStep that has an assertion with Exception that does not throw any exception", () => Step = FixtureSteps.CreateThenStep(exc => { }));
            Given("The next ThenStep that asserts the Exception that is thrown at WhenStep", () => NextStep = FixtureSteps.CreateThenStep(exc => { if (exc != AssertedException) throw new Exception(); }));
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);

            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given next ThenStep", () => Result.Step == NextStep);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exception and the next ThenStep that asserts the Exception that is not thrown at WhenStep are run")]
        void Ex08()
        {
            Given("ThenStep that has an assertion with Exception that does not throw any exception", () => Step = FixtureSteps.CreateThenStep(exc => { }));
            Given("The next ThenStep that asserts the Exception that is not thrown at WhenStep", () => NextStep = FixtureSteps.CreateThenStep(exc => { if (exc == AssertedException) throw new Exception(); }));
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the status of the result should be Passed", () => Result.Status == FixtureStepStatus.Passed);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given ThenStep", () => Result.Step == Step);

            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then("the status of the result should be Failed", () => Result.Status == FixtureStepStatus.Failed);
            Then("the exception of the result should not be null", () => Result.Exception != null);
            Then("the step of the result should be the given next ThenStep", () => Result.Step == NextStep);
        }
    }
}
