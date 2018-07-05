// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;

using Carna.Step;

namespace Carna.Runner.Step
{
    [Specification("FixtureStepResultCollectionSpec")]
    class FixtureStepResultCollectionSpec : FixtureSteppable
    {
        FixtureStepResultCollection Results { get; }

        public FixtureStepResultCollectionSpec()
        {
            Results = new FixtureStepResultCollection();
        }

        [Example("Gets the value that indicates whether the collection has the result of the specified FixtureStep")]
        void Ex01()
        {
            When("the result of GivenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Build()));
            Then("the collection should not have the result of ExpectStep", () => !Results.Has(typeof(ExpectStep)));
            Then("the collection should have the result of GivenStep", () => Results.Has(typeof(GivenStep)));
            Then("the collection should have the result of WhenStep", () => Results.Has(typeof(WhenStep)));
            Then("the collection should have the result of ThenStep", () => Results.Has(typeof(ThenStep)));
            Then("the collection should have the result of GivenStep, WhenStep, and ThenStep", () => Results.Has(typeof(GivenStep), typeof(WhenStep), typeof(ThenStep)));
        }

        [Example("Gets the value that indicates whether the collection has the result of the specified FixtureStep that has an exception")]
        void Ex02()
        {
            When("the result of GivenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Build()));
            When("the result of WhenStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the result of ThenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Build()));
            When("the result of ExpectStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Failed(new Exception()).Build()));
            Then("the value that indicates whether the result of GivenStep has an exception should be false", () => !Results.HasExceptionAt<GivenStep>());
            Then("the value that indicates whether the result of WhenStep has an exception should be true", () => Results.HasExceptionAt<WhenStep>());
            Then("the value that indicates whether the result of ThenStep has an exception should be false", () => !Results.HasExceptionAt<ThenStep>());
            Then("the value that indicates whether the result of ExpectStep has an exception should be true", () => Results.HasExceptionAt<ExpectStep>());
        }

        [Example("Gets the latest results of the specified step ordered by the latest added")]
        void Ex03()
        {
            When("the result of GivenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep("Given")).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep("When 1")).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep("When 2")).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep("When 3")).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep("Then 1")).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep("Then 2")).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep("When 4")).Build()));
            When("the result of WhenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep("When 5")).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep("Then 3")).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep("Then 4")).Build()));
            When("the result of ThenStep is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep("Then 5")).Build()));

            var latestGivenSteps = Results.GetLatestStepResultsOf<GivenStep>();
            Then("the count of the latest results of GivenStep should be 1", () => latestGivenSteps.Count() == 1);
            Then("the description of the latest result of GivenStep should be the specified description", () => latestGivenSteps.ElementAt(0).Step.Description == "Given");

            var latestWhenSteps = Results.GetLatestStepResultsOf<WhenStep>();
            Then("the count of the latest results of WhenStep should be 2", () => latestWhenSteps.Count() == 2);
            Then("the description of the latest result of WhenStep(1st) should be the specified description", () => latestWhenSteps.ElementAt(0).Step.Description == "When 5");
            Then("the description of the latest result of WhenStep(2nd) should be the specified description", () => latestWhenSteps.ElementAt(1).Step.Description == "When 4");

            var latestThenSteps = Results.GetLatestStepResultsOf<ThenStep>();
            Then("the count of the latest results of ThenStep should be 3", () => latestThenSteps.Count() == 3);
            Then("the description of the latest result of ThenStep(1st) should be the specified description", () => latestThenSteps.ElementAt(0).Step.Description == "Then 5");
            Then("the description of the latest result of ThenStep(2nd) should be the specified description", () => latestThenSteps.ElementAt(1).Step.Description == "Then 4");
            Then("the description of the latest result of ThenStep(3rd) should be the specified description", () => latestThenSteps.ElementAt(2).Step.Description == "Then 3");

            var latestExpectSteps = Results.GetLatestStepResultsOf<ExpectStep>();
            Then("the count of the latest results of ExpectStep should be 0", () => !latestExpectSteps.Any());
        }

        [Example("Gets the value that indicates whether the latest result of the specified step has an exception")]
        void Ex04()
        {
            When("the result of GivenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Build()));
            When("the result of ExpectStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Failed(new Exception()).Build()));
            When("the result of WhenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of WhenStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the result of WhenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of ThenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Build()));
            When("the result of ExpectStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Build()));
            Then("the value that indicates whether the latest result of GivenStep has an exception should be false", () => !Results.HasLatestExceptionAt<GivenStep>());
            Then("the value that indicates whether the latest result of WhenStep has an exception should be true", () => Results.HasLatestExceptionAt<WhenStep>());
            Then("the value that indicates whether the latest result of ThenStep has an exception should be false", () => !Results.HasLatestExceptionAt<ThenStep>());
            Then("the value that indicates whether the latest result of ExpectStep has an exception should be false", () => !Results.HasLatestExceptionAt<ExpectStep>());
        }

        [Example("Gets the exception of the latest result of the specified step")]
        void Ex05()
        {
            var whenException = new Exception();
            var thenException = new Exception();

            When("the result of GivenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Build()));
            When("the result of ExpectStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Failed(new Exception()).Build()));
            When("the result of WhenStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(new Exception()).Build()));
            When("the result of WhenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of WhenStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(whenException).Build()));
            When("the result of WhenStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Build()));
            When("the result of ThenStep that has an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(thenException).Build()));
            When("the result of ExpectStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Build()));
            Then("the exception of the latest result of GivenStep should be null", () => Results.GetLatestExceptionAt<GivenStep>() == null);
            Then("the exception of the latest result of WhenStep should be the specified last exception", () => Results.GetLatestExceptionAt<WhenStep>() == whenException);
            Then("the exception of the latest result of ThenStep should be the specified last exception", () => Results.GetLatestExceptionAt<ThenStep>() == thenException);
            Then("the exception of the latest result of ExpectStep should be null", () => Results.GetLatestExceptionAt<ExpectStep>() == null);
        }

        [Example("Clears the exception and changes the status to Passed for the result the exception of which is equal to the specified exception")]
        void Ex06()
        {
            var targetException = new Exception();

            When("the result of GivenStep that has an exception that is not a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateGivenStep()).Failed(new Exception()).Build()));
            When("the result of ExpectStep that has a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Failed(targetException).Build()));
            When("the result of WhenStep that has a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(targetException).Build()));
            When("the result of WhenStep that has a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateWhenStep()).Failed(targetException).Build()));
            When("the result of ThenStep that has an exception that is not a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            When("the result of ThenStep that has an exception that is not a target exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateThenStep()).Failed(new Exception()).Build()));
            When("the result of ExpectStep that does not have an exception is added", () => Results.Add(FixtureStepResult.Of(FixtureSteps.CreateExpectStep()).Passed().Build()));
            When("the collection clears the exception of the result the exception of which is equal to the target exception", () => Results.ClearException(targetException));
            Then("the exception of the result of GivenStep should not be cleared", () => Results.HasExceptionAt<GivenStep>());
            Then("all status of the result of GivenStep should not be Passed", () => Results.Where(r => r.Step.GetType() == typeof(GivenStep)).Any(r => r.Status != FixtureStepStatus.Passed));
            Then("the exception of the result of WhenStep should be cleared", () => !Results.HasExceptionAt<WhenStep>());
            Then("all status of the result of WhenStep should be Passed", () => Results.Where(r => r.Step.GetType() == typeof(WhenStep)).All(r => r.Status == FixtureStepStatus.Passed));
            Then("the exception of the result of ThenStep should not be cleared", () => Results.HasExceptionAt<ThenStep>());
            Then("all status of the result of GivenStep should not be Passed", () => Results.Where(r => r.Step.GetType() == typeof(ThenStep)).Any(r => r.Status != FixtureStepStatus.Passed));
            Then("the exception of the result of ExpectStep should be cleared", () => !Results.HasExceptionAt<ExpectStep>());
            Then("all status of the result of ExpectStep should be Passed", () => Results.Where(r => r.Step.GetType() == typeof(ExpectStep)).All(r => r.Status == FixtureStepStatus.Passed));
        }
    }
}
