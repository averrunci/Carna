// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("Runs ThenStep with Typed Exception")]
class ThenStepRunnerSpec_StepRunningWithTypedException : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    ThenStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public ThenStepRunnerSpec_StepRunningWithTypedException()
    {
        StepResults = new FixtureStepResultCollection
        {
            FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new ArgumentNullException()).Build()
        };
    }

    private IFixtureStepRunner RunnerOf(ThenStep step) => new ThenStepRunner(step);

    [Example("When ThenStep that has the type of an exception that is valid")]
    void Ex01()
    {
        Given("ThenStep that has the type of an exception that is valid", () =>
        {
            Step = FixtureSteps.CreateThenStep<ArgumentNullException>();
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has the type of an exception that is invalid")]
    void Ex02()
    {
        Given("ThenStep that has tye type of an exception that is invalid", () =>
        {
            Step = FixtureSteps.CreateThenStep<InvalidOperationException>();
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion with Exception that returns true is run")]
    void Ex03()
    {
        Given("ThenStep that has an assertion with Exception that returns true", () =>
        {
            Step = FixtureSteps.CreateThenStep<ArgumentNullException>(_ => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion with Exception that returns false is run")]
    void Ex04()
    {
        Given("ThenStep that has an assertion with Exception that returns false", () =>
        {
            Step = FixtureSteps.CreateThenStep<ArgumentNullException>(_ => false);
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion with Exception that does not throw any exceptions is run")]
    void Ex05()
    {
        Given("ThenStep that has an assertion with Exception that does not throw any exception", () =>
        {
            Step = FixtureSteps.CreateThenStep<ArgumentNullException>(_ => { });
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When ThenStep that has an assertion with Exception that throws an exception is run")]
    void Ex06()
    {
        Given("ThenStep that has an assertion with Exception that throws an exception", () =>
        {
            Step = FixtureSteps.CreateThenStep(new Action<ArgumentNullException>(_ => throw new Exception()));
            ExpectedResult = FixtureStepResultAssertion.ForNotNullException(FixtureStepStatus.Failed, Step);
        });
        When("the given ThenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}