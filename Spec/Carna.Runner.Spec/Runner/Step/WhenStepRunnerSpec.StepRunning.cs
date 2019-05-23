// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs WhenStep")]
    class WhenStepRunnerSpec_StepRunning : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        WhenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public WhenStepRunnerSpec_StepRunning()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(WhenStep step) => new WhenStepRunner(step);

        [Example("When WhenStep that has an action that does not throw any exceptions is run")]
        void Ex01()
        {
            Given("WhenStep that has an action that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateWhenStep(() => { });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that has an action that throws an exception is run")]
        void Ex02()
        {
            Given("WhenStep that has an action that throws an exception", () =>
            {
                Step = FixtureSteps.CreateWhenStep(() => throw new Exception());
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When WhenStep that does not have an action is run")]
        void Ex03()
        {
            Given("WhenStep that does not have an action", () =>
            {
                Step = FixtureSteps.CreateWhenStep();
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
            });
            When("the given WhenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
