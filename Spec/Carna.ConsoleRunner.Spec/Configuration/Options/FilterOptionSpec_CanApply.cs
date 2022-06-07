// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Determines whether the filter option can be applied")]
class FilterOptionSpec_CanApply : FixtureSteppable
{
    FilterOption Option { get; } = new();
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

    [Example("When CommandLineOptionContext has a key that is not -f, --filter, /f, and /filter")]
    void Ex03()
    {
        Given("a context that has a key that is not -f, --filter, /f, and /filter", () => Context = CarnaRunnerCommandLineOptionContext.Of("/fil"));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /f")]
    void Ex04()
    {
        Given("a context that has a key that is /f", () => Context = CarnaRunnerCommandLineOptionContext.Of("/f"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is /filter")]
    void Ex05()
    {
        Given("a context that has a key that is /filter", () => Context = CarnaRunnerCommandLineOptionContext.Of("/filter"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is -f")]
    void Ex06()
    {
        Given("a context that has a key that is -f", () => Context = CarnaRunnerCommandLineOptionContext.Of("-f"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key that is --filter")]
    void Ex07()
    {
        Given("a context that has a key that is --filter", () => Context = CarnaRunnerCommandLineOptionContext.Of("--filter"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }
}