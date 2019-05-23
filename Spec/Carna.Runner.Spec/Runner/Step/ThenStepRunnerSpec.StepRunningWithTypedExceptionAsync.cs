// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep with Typed Exception asynchronously")]
    class ThenStepRunnerSpec_StepRunningWithTypedExceptionAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        Exception AssertedException { get; } = new ArgumentNullException();
        ThenStep Step { get; set; }
        ThenStep NextStep { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }
        FixtureStepResultAssertion ExpectedNextResult { get; set; }

        public ThenStepRunnerSpec_StepRunningWithTypedExceptionAsync()
        {
            StepResults = new FixtureStepResultCollection
            {
                FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(AssertedException).Build()
            };
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var thenStepCompleted = false;
            Given("async ThenStep that has an assertion with Exception that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep<ArgumentNullException>(async exc =>
                    {
                        await Task.Delay(100);
                        thenStepCompleted = true;
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given ThenStep should be awaited", () => thenStepCompleted);
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When ThenStep that has an assertion with Exception that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async ThenStep that has an assertion with Exception that throws an exception", () =>
            {
                Step = FixtureSteps.CreateThenStep<ArgumentNullException>(async exc =>
                    {
                        await Task.Delay(100);
                        throw new Exception();
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions and the next ThenStep that asserts the Exception that is thrown at WhenStep are run asynchronously")]
        void Ex03()
        {
            var thenStepCompleted = false;
            Given("async ThenStep that has an assertion with Exception that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep<ArgumentNullException>(async exc =>
                    {
                        await Task.Delay(100);
                        thenStepCompleted = true;
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("async next ThenStep that asserts the Exception that is thrown at WhenStep", () =>
            {
                NextStep = FixtureSteps.CreateThenStep<ArgumentNullException>(async exc =>
                    {
                        await Task.Delay(100);
                        thenStepCompleted = true;
                    });
                ExpectedNextResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, NextStep);
            });
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the given ThenStep should be awaited", () => thenStepCompleted);
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);

            thenStepCompleted = false;
            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then("the given next ThenStep should be awaited", () => thenStepCompleted);
            Then($"the result should be as follows:{ExpectedNextResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedNextResult);
        }

        [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions and the next ThenStep that asserts the Exception that is not thrown at WhenStep are run asynchronously")]
        void Ex04()
        {
            var thenStepCompleted = false;
            Given("async ThenStep that has an assertion with Exception that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep<ArgumentNullException>(async exc =>
                    {
                        await Task.Delay(100);
                        thenStepCompleted = true;
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            Given("async next ThenStep that asserts the Exception that is not thrown at WhenStep", () =>
            {
                NextStep = FixtureSteps.CreateThenStep<InvalidOperationException>(async exc =>
                    {
                        await Task.Delay(100);
                    });
                ExpectedNextResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, NextStep);
            });
            When("the given ThenStep is run", () =>
            {
                Result = RunnerOf(Step).Run(StepResults).Build();
                StepResults.Add(Result);
            });
            Then("the given ThenStep should be awaited", () => thenStepCompleted);
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);

            When("the given next ThenStep is run", () => Result = RunnerOf(NextStep).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedNextResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedNextResult);
        }
    }
}
