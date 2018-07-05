// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Context("Applied the help option")]
    class HelpOptionSpec_ApplyOption : FixtureSteppable
    {
        HelpOption Option { get; } = new HelpOption();
        CarnaRunnerCommandLineOptions Options { get; } = new CarnaRunnerCommandLineOptions();
        CarnaRunnerCommandLineOptionContext Context { get; set; }

        string Pattern { get; set; }

        [Example("When a help option is specified")]
        void Ex01()
        {
            Given("a context that has a help option key", () => Context = CarnaRunnerCommandLineOptionContext.Of("/h"));
            When("the option is applied", () => Option.Apply(Options, Context));
            Then("the help option should be set", () => Options.HasHelp);
        }
    }
}
