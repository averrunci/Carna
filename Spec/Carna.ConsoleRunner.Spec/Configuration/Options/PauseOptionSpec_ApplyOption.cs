// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Applied the pause option")]
class PauseOptionSpec_ApplyOption : FixtureSteppable
{
    PauseOption Option { get; } = new();
    CarnaRunnerCommandLineOptions Options { get; } = new();
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    [Example("When a pause option is specified")]
    void Ex01()
    {
        Given("a context that has a pause option key", () => Context = CarnaRunnerCommandLineOptionContext.Of("/p"));
        When("the option is applied", () => Option.Apply(Options, Context));
        Then("the pause option should be set", () => Options.CanPause);
    }
}