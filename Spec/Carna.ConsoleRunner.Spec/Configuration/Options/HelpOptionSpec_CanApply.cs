// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Determines whether the help option can be applied")]
class HelpOptionSpec_CanApply : FixtureSteppable
{
    HelpOption Option { get; } = new();
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

    [Example("When CommandLineOptionContext has a key that is not -?, -h, --help, /?, /h, and /help")]
    void Ex03()
    {
        Given("a context that has a key that is not -?, -h, --help, /?, /h, and /help", () => Context = CarnaRunnerCommandLineOptionContext.Of("/hel"));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /?")]
    void Ex04()
    {
        Given("a context that has a key that is /?", () => Context = CarnaRunnerCommandLineOptionContext.Of("/?"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /h")]
    void Ex05()
    {
        Given("a context that has a key that is /h", () => Context = CarnaRunnerCommandLineOptionContext.Of("/h"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /help")]
    void Ex06()
    {
        Given("a context that has a key that is /help", () => Context = CarnaRunnerCommandLineOptionContext.Of("/help"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is -?")]
    void Ex07()
    {
        Given("a context that has a key that is -?", () => Context = CarnaRunnerCommandLineOptionContext.Of("-?"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is -h")]
    void Ex08()
    {
        Given("a context that has a key that is -h", () => Context = CarnaRunnerCommandLineOptionContext.Of("-h"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is --help")]
    void Ex09()
    {
        Given("a context that has a key that is --help", () => Context = CarnaRunnerCommandLineOptionContext.Of("--help"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }
}