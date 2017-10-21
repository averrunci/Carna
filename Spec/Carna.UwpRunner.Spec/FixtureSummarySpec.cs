// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;

using Carna.Runner;

namespace Carna.UwpRunner
{
    [Specification("FixtureSummary Spec")]
    class FixtureSummarySpec : FixtureSteppable
    {
        FixtureSummary Summary { get; } = new FixtureSummary();

        [Example("Changes the passed rate when the passed count is changed")]
        void Ex01()
        {
            Given("the total count is 5", () => Summary.TotalCount = 5);
            When("the passed count is set to 2", () => Summary.PassedCount = 2);
            Then("the passed rate should be 40", () => Summary.PassedRate == 40);
            When("the passed count is set to 4", () => Summary.PassedCount = 4);
            Then("the passed rate should be 80", () => Summary.PassedRate == 80);
            When("the passed count is set to 5", () => Summary.PassedCount = 5);
            Then("the passed rate should be 100", () => Summary.PassedRate == 100);
        }

        [Example("Changes the IsTimeVisible when the total count is changed")]
        void Ex02()
        {
            Expect("the IsTimeVisible should be false", () => !Summary.IsTimeVisible);
            When("the total count is set to 1", () => Summary.TotalCount = 1);
            Then("the IsTimeVisible should be true", () => Summary.IsTimeVisible);
            When("the total count is set to 0", () => Summary.TotalCount = 0);
            Then("the IsTimeVisible should be false", () => !Summary.IsTimeVisible);
            When("the total count is set to 2", () => Summary.TotalCount = 2);
            Then("the IsTimeVisible should be true", () => Summary.IsTimeVisible);
        }

        [Example("Sets the state when the fixture building/running is starting/completed")]
        void Ex03()
        {
            Given("the total count is 6", () => Summary.TotalCount = 6);
            Given("the passed count is 3", () => Summary.PassedCount = 3);
            Given("the failed count is 2", () => Summary.FailedCount = 2);
            Given("the pending count is 1", () => Summary.PendingCount = 1);
            Given("the IsFixtureBuilding is false", () => Summary.IsFixtureBuilding = false);
            Given("the IsFixtureBuilt is true", () => Summary.IsFixtureBuilt = true);
            Given("the IsFixtureRunning is true", () => Summary.IsFixtureRunning = true);
            Given("the StartDateTime is 09:00", () => Summary.StartDateTime = "09:00");
            Given("the EndDateTime is 09:01", () => Summary.EndDateTime = "09:01");
            Given("the Duration is 1.023", () => Summary.Duration = "1.023");

            When("the fixture building is starting", () => Summary.OnFixtureBuildingStarting());
            Then("the total count should be 0", () => Summary.TotalCount == 0);
            Then("the passed count should be 0", () => Summary.PassedCount == 0);
            Then("the failed count should be 0", () => Summary.FailedCount == 0);
            Then("the pending count should be 0", () => Summary.PendingCount == 0);
            Then("the IsFixtureBuilding should be true", () => Summary.IsFixtureBuilding);
            Then("the IsFixtureBuilt should be false", () => !Summary.IsFixtureBuilt);
            Then("the IsFixtureRunning should be false", () => !Summary.IsFixtureRunning);
            Then("the StartDateTime should be null", () => Summary.StartDateTime == null);
            Then("the EndDateTime should be null", () => Summary.EndDateTime == null);
            Then("the Duration should be null", () => Summary.Duration == null);

            var fixutreBuidlingCompletedDateTime = DateTime.UtcNow;
            When("the fixture building is completed with the date time at which the fixture building is completed", () =>
                Summary.OnFixtureBuildingCompleted(fixutreBuidlingCompletedDateTime)
            );
            Then("the total count should be 0", () => Summary.TotalCount == 0);
            Then("the passed count should be 0", () => Summary.PassedCount == 0);
            Then("the failed count should be 0", () => Summary.FailedCount == 0);
            Then("the pending count should be 0", () => Summary.PendingCount == 0);
            Then("the IsFixtureBuilding should be false", () => !Summary.IsFixtureBuilding);
            Then("the IsFixtureBuilt should be true", () => Summary.IsFixtureBuilt);
            Then("the IsFixtureRunning should be false", () => !Summary.IsFixtureRunning);
            Then("the StartDateTime should be the specified date time", () =>
                Summary.StartDateTime == fixutreBuidlingCompletedDateTime.ToString("u")
            );
            Then("the EndDateTime should be null", () => Summary.EndDateTime == null);
            Then("the Duration should be null", () => Summary.Duration == null);

            When("the fixture running is starting", () => Summary.OnFixtureRunningStarting());
            Then("the total count should be 0", () => Summary.TotalCount == 0);
            Then("the passed count should be 0", () => Summary.PassedCount == 0);
            Then("the failed count should be 0", () => Summary.FailedCount == 0);
            Then("the pending count should be 0", () => Summary.PendingCount == 0);
            Then("the IsFixtureBuilding should be false", () => !Summary.IsFixtureBuilding);
            Then("the IsFixtureBuilt should be true", () => Summary.IsFixtureBuilt);
            Then("the IsFixtureRunning should be true", () => Summary.IsFixtureRunning);
            Then("the StartDateTime should be the date time at which the fixture building is completed", () =>
                Summary.StartDateTime == fixutreBuidlingCompletedDateTime.ToString("u")
            );
            Then("the EndDateTime should be null", () => Summary.EndDateTime == null);
            Then("the Duration should be null", () => Summary.Duration == null);

            var fixtureRunningStartDateTime = DateTime.UtcNow;
            var fixtureRunningEndDateTime = DateTime.UtcNow.AddMilliseconds(105);
            var results = new List<FixtureResult>(new[]
            {
                FixtureResult.Of(new FixtureDescriptor("name", new ExampleAttribute())).StartAt(fixtureRunningStartDateTime).EndAt(fixtureRunningEndDateTime).Passed()
            });
            When("the fixture running is completed with results of the fixture running", () =>
                Summary.OnFixtureRunningCompleted(results)
            );
            Then("the total count should be 0", () => Summary.TotalCount == 0);
            Then("the passed count should be 0", () => Summary.PassedCount == 0);
            Then("the failed count should be 0", () => Summary.FailedCount == 0);
            Then("the pending count should be 0", () => Summary.PendingCount == 0);
            Then("the IsFixtureBuilding should be false", () => !Summary.IsFixtureBuilding);
            Then("the IsFixtureBuilt should be true", () => Summary.IsFixtureBuilt);
            Then("the IsFixtureRunning should be false", () => !Summary.IsFixtureRunning);
            Then("the StartDateTime should be the start date time of the specified results", () =>
                Summary.StartDateTime == fixtureRunningStartDateTime.ToString("u")
            );
            Then("the EndDateTime should be the end date time of the specified results", () =>
                Summary.EndDateTime == fixtureRunningEndDateTime.ToString("u")
            );
            Then("the Duration should be the duration of the specified results", () =>
                Summary.Duration == $"{(fixtureRunningEndDateTime - fixtureRunningStartDateTime).TotalSeconds:0.000} seconds"
            );
        }
    }
}
