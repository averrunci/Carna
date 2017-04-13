// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.UwpRunner
{
    [Specification("FixtureSummary Spec")]
    class FixtureSummarySpec : FixtureSteppable
    {
        FixtureSummary Summary { get; } = new FixtureSummary();

        [Example("Changes the passed rate when the passed count is changed")]
        void Ex01()
        {
            Given("the total count is 5", () => Summary.TotalCount.Value = 5);
            When("the passed count is set to 2", () => Summary.PassedCount.Value = 2);
            Then("the passed rate should be 40", () => Summary.PassedRate.Value == 40);
            When("the passed count is set to 4", () => Summary.PassedCount.Value = 4);
            Then("the passed rate should be 80", () => Summary.PassedRate.Value == 80);
            When("the passed count is set to 5", () => Summary.PassedCount.Value = 5);
            Then("the passed rate should be 100", () => Summary.PassedRate.Value == 100);
        }

        [Example("Changes the IsTimeVisible when the total count is changed")]
        void Ex02()
        {
            Expect("the IsTimeVisible should be false", () => !Summary.IsTimeVisible.Value);
            When("the total count is set to 1", () => Summary.TotalCount.Value = 1);
            Then("the IsTimeVisible should be true", () => Summary.IsTimeVisible.Value);
            When("the total count is set to 0", () => Summary.TotalCount.Value = 0);
            Then("the IsTimeVisible should be false", () => !Summary.IsTimeVisible.Value);
            When("the total count is set to 2", () => Summary.TotalCount.Value = 2);
            Then("the IsTimeVisible should be true", () => Summary.IsTimeVisible.Value);
        }
    }
}
