// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Determines whether the assembly option can be applied")]
class AssemblyOptionSpec_CanApply : FixtureSteppable
{
    AssemblyOption Option { get; } = new();
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    [Example("When CommandLineOptionContext has an empty argument")]
    void Ex01()
    {
        Given("a context that has an empty argument", () => Context = CarnaRunnerCommandLineOptionContext.Of(string.Empty));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext has a key")]
    void Ex02()
    {
        Given("a context that has a key", () => Context = CarnaRunnerCommandLineOptionContext.Of("/a"));
        Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
    }

    [Example("When CommandLineOptionContext is not null and does not have a key")]
    void Ex03()
    {
        Given("a context that does not have a key", () => Context = CarnaRunnerCommandLineOptionContext.Of("assembly"));
        Expect("the option should be able to be applied", () => Option.CanApply(Context));
    }
}