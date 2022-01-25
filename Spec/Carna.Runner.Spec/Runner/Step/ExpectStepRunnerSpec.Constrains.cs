// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("ExpectStep running constrains")]
class ExpectStepRunnerSpec_Constrains : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    ExpectStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public ExpectStepRunnerSpec_Constrains()
    {
        StepResults = new FixtureStepResultCollection();
    }

    private IFixtureStepRunner RunnerOf(ExpectStep step) => new ExpectStepRunner(step);

    [Example("When GivenStep that has already run has an exception, ExpectStep is not run")]
    void Ex01()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
        });
        Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
        Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run has an exception, ExpectStep is not run")]
    void Ex02()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
        });
        Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run does not have an exception but other WhenStep that has already run has an exception, ExpectStep is run")]
    void Ex03()
    {
        Given("ExpectStep that has an assertion that return true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        Given("a result of GivenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
        Given("a result of ThenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
        Given("a result of WhenStep that does not have an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When GivenStep that has already run has Ready status, ExpectStep is not run")]
    void Ex04()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
        });
        Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of GivenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Ready().Build()));
        Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"The result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run has Ready status, ExpectStep is not run")]
    void Ex05()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
        });
        Given("a result of GivenStep that does not have Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Ready().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run does not have Ready status but other WhenStep that has already run has Ready status, ExpectStep is run")]
    void Ex06()
    {
        Given("ExpectStep that has an assertion that return true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        Given("a result of GivenStep that does not have Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Ready().Build()));
        Given("a result of ThenStep that has Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Ready().Build()));
        Given("a result of WhenStep that does not have Ready status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When GivenStep that has already run has Pending status, ExpectStep is not run")]
    void Ex07()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
        });
        Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of GivenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Pending().Build()));
        Given("a result of GivenStep that has Passed status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"The result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run has Pending status, ExpectStep is not run")]
    void Ex08()
    {
        Given("ExpectStep that has an assertion that returns true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Pending, Step);
        });
        Given("a result of GivenStep that does not have Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Pending().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }

    [Example("When latest WhenStep that has already run does not have Pending status but other WhenStep that has already run has Pending status, ExpectStep is run")]
    void Ex09()
    {
        Given("ExpectStep that has an assertion that return true", () =>
        {
            Step = FixtureSteps.CreateExpectStep(() => true);
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Passed, Step);
        });
        Given("a result of GivenStep that does not have Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Passed().Build()));
        Given("a result of WhenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Pending().Build()));
        Given("a result of ThenStep that has Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Pending().Build()));
        Given("a result of WhenStep that does not have Pending status", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
        When("the given ExpectStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}