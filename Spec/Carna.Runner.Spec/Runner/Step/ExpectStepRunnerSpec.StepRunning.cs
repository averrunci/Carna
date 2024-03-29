﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("Runs ExpectStep")]
class ExpectStepRunnerSpec_StepRunning : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    ExpectStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public ExpectStepRunnerSpec_StepRunning()
    {
        StepResults = new FixtureStepResultCollection();
    }

    private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

    [Example("When ExpectStep that has an assertion that returns true is run")]
    void Ex01()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ExpectStep that has an assertion that returns false is run")]
    void Ex02()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => false);
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ExpectStep that has an action that does not throw any exceptions is run")]
    void Ex03()
    {
        Given("ExpectStep that has an action that does not throw any exceptions", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => { });
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ExpectStep that has an action that throws an exception is run")]
    void Ex04()
    {
        Given("ExpectStep that has an action that throws an exception", () =>
        {
            Step = FixtureSteps.CreateExpectStep(new Action(() => throw new Exception()));
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ExpectStep that does not have an assertion and an action is run")]
    void Ex05()
    {
        Given("ExpectStep that does not have an assertion and an action", () =>
        {
            Step = FixtureSteps.CreateExpectStep();
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
        });
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}