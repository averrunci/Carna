// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Context("Applies the filter option")]
    class FilterOptionSpec_ApplyOption : FixtureSteppable
    {
        FilterOption Option { get; } = new FilterOption();
        CarnaRunnerCommandLineOptions Options { get; } = new CarnaRunnerCommandLineOptions();
        CarnaRunnerCommandLineOptionContext Context { get; set; }

        string Pattern { get; set; }

        [Example("When a filter pattern is specified")]
        void Ex01()
        {
            Given("a context that has a filter pattern", () =>
            {
                Pattern = "Test";
                Context = CarnaRunnerCommandLineOptionContext.Of($"/f:{Pattern}");
            });
            When("the option is applied", () => Option.Apply(Options, Context));
            Then("the filter pattern should be set", () => Options.Filter == Pattern);
        }

        [Example("When a filter pattern is not specified")]
        void Ex02()
        {
            Given("a context that does not have a filter pattern", () =>
            {
                Context = CarnaRunnerCommandLineOptionContext.Of("/f");
            });
            When("the option is applied", () => Option.Apply(Options, Context));
            Then("the filter pattern should not be set", () => Options.Filter == null);
        }
    }
}
