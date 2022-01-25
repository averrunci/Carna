// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("Runs ExpectStep asynchronously")]
class ExpectStepRunnerSpec_StepRunningAsync : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    ExpectStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public ExpectStepRunnerSpec_StepRunningAsync()
    {
        StepResults = new FixtureStepResultCollection();
    }

    private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

    [Example("When ExpectStep that has an action that does not throw any exceptions is run asynchronously")]
    void Ex01()
    {
        var expectStepCompleted = false;
        Given("async ExpectStep that has an action that does not throw any exceptions", () =>
        {
            Step = FixtureSteps.CreateExpectStep(async () =>
            {
                await Task.Delay(100);
                expectStepCompleted = true;
            });
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then("the given ExpectStep should be awaited", () => expectStepCompleted);
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ExpectStep that has an action that throws an exception is run asynchronously")]
    void Ex02()
    {
        Given("async ExpectStep that has an action that throws an exception", () =>
        {
            Step = FixtureSteps.CreateExpectStep(async () =>
            {
                await Task.Delay(100);
                throw new Exception();
            });
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}