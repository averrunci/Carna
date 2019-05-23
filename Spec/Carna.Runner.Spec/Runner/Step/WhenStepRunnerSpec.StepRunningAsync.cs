// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs WhenStep asynchronously")]
    class WhenStepRunnerSpec_StepRunningAsync : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public WhenStepRunnerSpec_StepRunningAsync()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When WhenStep that has an action that does not throw any exceptions is run asynchronously")]
        void Ex01()
        {
            var whenStepCompleted = false;
            Given("async WhenStep that has an action that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateWhenStep(async () =>
                    {
                        await Task.Delay(100);
                        whenStepCompleted = true;
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given WhenStep should be awaited", () => whenStepCompleted);
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that has an action that throws an exception is run asynchronously")]
        void Ex02()
        {
            Given("async WhenStep that has an action that throws an exception", () =>
            {
                Step = FixtureSteps.CreateWhenStep(async () =>
                    {
                        await Task.Delay(100);
                        throw new Exception();
                    });
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
