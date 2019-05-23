// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs ThenStep without Exception asynchronously")]
    class ThenStepRunnerSpec_StepRunningWithoutExceptionAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        ThenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public ThenStepRunnerSpec_StepRunningWithoutExceptionAsync()
        {
            StepResults = new FixtureStepResultCollection
            {
                FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()
            };
        }

        private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

        [Example("When ThenStep that has an assertion that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var thenStepCompleted = false;
            Given("async ThenStep that has an assertion that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateThenStep(async () =>
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

        [Example("When ThenStep that has an assertion that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async ThenStep that has an assertion that throws an exception", () =>
            {
                Step = FixtureSteps.CreateThenStep(async () =>
                    {
                        await Task.Delay(100);
                        throw new Exception();
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
