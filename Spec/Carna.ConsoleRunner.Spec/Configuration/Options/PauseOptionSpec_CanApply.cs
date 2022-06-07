// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Determines whether the pause option can be applied")]
class PauseOptionSpec_CanApply : FixtureSteppable
{
    PauseOption Option { get; } = new();
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    [Example("When CommandLineOptionContext has an empty argument")]
    void Ex01()
    {
        Given("a context that has an empty argument", () => Context = CarnaRunnerCommandLineOptionContext.Of(string.Empty));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext does not have a key")]
    void Ex02()
    {
        Given("a context that does not have a key", () => Context = CarnaRunnerCommandLineOptionContext.Of("filter"));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is not /p and /pause")]
    void Ex03()
    {
        Given("a context that has a key that is not /p and /pause", () => Context = CarnaRunnerCommandLineOptionContext.Of("/pau"));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /p")]
    void Ex04()
    {
        Given("a context that has a key that is /p", () => Context = CarnaRunnerCommandLineOptionContext.Of("/p"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /help")]
    void Ex05()
    {
        Given("a context that has a key that is /pause", () => Context = CarnaRunnerCommandLineOptionContext.Of("/pause"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }
}