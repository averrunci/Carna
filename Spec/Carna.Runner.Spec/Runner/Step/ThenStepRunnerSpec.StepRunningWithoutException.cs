// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("Runs ThenStep without Exception")]
class ThenStepRunnerSpec_StepRunningWithoutException : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    ThenStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public ThenStepRunnerSpec_StepRunningWithoutException()
    {
        StepResults = new FixtureStepResultCollection
        {
            FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()
        };
    }

    private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

    [Example("When ThenStep that has an assertion that returns true is run")]
    void Ex01()
    {
        Given("ThenStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateThenStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion that returns false is run")]
    void Ex02()
    {
        Given("ThenStep that has an assertion that returns false", () =>
        {
            Step = FixtureSteps.CreateThenStep(() => false);
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion that does not throw any exceptions is run")]
    void Ex03()
    {
        Given("ThenStep that has an assertion that does not throw any exception", () =>
        {
            Step = FixtureSteps.CreateThenStep(() => { });
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion that throws an exception is run")]
    void Ex04()
    {
        Given("ThenStep that has an assertion that throws an exception", () =>
        {
            Step = FixtureSteps.CreateThenStep(new Action(() => throw new Exception()));
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that does not have an assertion is run")]
    void Ex05()
    {
        Given("ThenStep that does not have an assertion", () =>
        {
            Step = FixtureSteps.CreateThenStep();
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}