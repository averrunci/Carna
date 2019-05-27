// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs WhenStep with timeout")]
    class WhenStepRunnerSpec_StepRunningWithTimeout : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public WhenStepRunnerSpec_StepRunningWithTimeout()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When WhenStep that has an action is run within a time-out")]
        void Ex01()
        {
            Given("WhenStep that has an action that is completed within a time-out", () =>
            {
                Step = FixtureSteps.CreateWhenStep(100, () => { });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that has an action is run over a time-out")]
        void Ex02()
        {
            Given("WhenStep that has an action that is not completed within a time-out", () =>
            {
                Step = FixtureSteps.CreateWhenStep(TimeSpan.FromMilliseconds(100), () => Thread.Sleep(200));
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that has an action is run within a time-out asynchronously")]
        void Ex03()
        {
            var whenStepCompleted = false;
            Given("async WhenStep that has an action that is completed within a time-out", () =>
            {
                Step = FixtureSteps.CreateWhenStep(100,async () =>
                {
                    await Task.Delay(50);
                    whenStepCompleted = true;
                });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the given WhenStep should be awaited", () => whenStepCompleted);
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that has an action is run over a time-out asynchronously")]
        void Ex04()
        {
            Given("async WhenStep that has an action that is not completed within a time-out", () =>
            {
                Step = FixtureSteps.CreateWhenStep(100, async () =>
                {
                    await Task.Delay(200);
                });
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
