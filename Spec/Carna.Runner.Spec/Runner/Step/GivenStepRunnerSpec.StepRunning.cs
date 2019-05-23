// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Context("Runs GivenStep")]
    class GivenStepRunnerSpec_StepRunning : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        GivenStep Step { get; set; }
        FixtureStepResult Result { get; set; }
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public GivenStepRunnerSpec_StepRunning()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(GivenStep step) => new GivenStepRunner(step);

        [Example("When GivenStep that has an arrangement that does not throw any exceptions is run")]
        void Ex01()
        {
            Given("GivenStep that has an arrangement that does not throw any exceptions", () =>
            {
                Step = FixtureSteps.CreateGivenStep(() => { });
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
            });
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When GivenStep that has an arrangement that throws an exception is run")]
        void Ex02()
        {
            Given("GivenStep that has an arrangement that throws an exception", () =>
            {
                Step = FixtureSteps.CreateGivenStep(() => throw new Exception());
                ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
            });
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }

        [Example("When GivenStep that does not have an arrangement is run")]
        void Ex03()
        {
            Given("GivenStep that does not have an arrangement", () =>
            {
                Step = FixtureSteps.CreateGivenStep();
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
            });
            When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
