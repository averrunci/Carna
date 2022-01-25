// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

[Context("GivenStep running constrains")]
class GivenStepRunnerSpec_Constrains : FixtureSteppable
{
    FixtureStepResultCollection StepResults { get; }

    GivenStep Step { get; set; } = default!;
    FixtureStepResult Result { get; set; } = default!;
    FixtureStepResultAssertion ExpectedResult { get; set; } = default!;

    public GivenStepRunnerSpec_Constrains()
    {
        StepResults = new FixtureStepResultCollection();
    }

    private IFixtureStepRunner RunnerOf(GivenStep step) => new GivenStepRunner(step);

    [Example("When WhenStep has already is run, an exception is thrown")]
    void Ex01()
    {
        Given("GivenStep that has an arrangement that does not throw any exceptions", () => Step = FixtureSteps.CreateGivenStep(() => { }));
        Given("a result of WhenStep", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Passed().Build()));
        When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then("InvalidFixtureStepException should be thrown", exc => exc.GetType() == typeof(InvalidFixtureStepException));
    }

    [Example("When ThenStep has already is run, an exception is thrown")]
    void Ex02()
    {
        Given("GivenStep that has an arrangement that does not throw any exceptions", () => Step = FixtureSteps.CreateGivenStep(() => { }));
        Given("a result of ThenStep", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Passed().Build()));
        When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then("InvalidFixtureStepException should be thrown", exc => exc.GetType() == typeof(InvalidFixtureStepException));
    }

    [Example("When GivenStep that has already run has an exception, GivenStep is not run")]
    void Ex03()
    {
        Given("GivenStep that has an arrangement that does not throw any exceptions", () =>
        {
            Step = FixtureSteps.CreateGivenStep(() => { });
            ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.Ready, Step);
        });
        Given("a result of GivenStep that has an exception", () => StepResults.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
        When("the given GivenStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
        Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
    }
}