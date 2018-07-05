// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Context("Determines whether the filter option can be applied")]
    class FilterOptionSpec_CanApply : FixtureSteppable
    {
        FilterOption Option { get; } = new FilterOption();
        CarnaRunnerCommandLineOptionContext Context { get; set; }

        [Example("When CommandLineOptionContext is null")]
        void Ex01()
        {
            Given("a context that is null", () => Context = null);
            Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
        }

        [Example("When CommandLineOptionContext does not have a key")]
        void Ex02()
        {
            Given("a context that does not have a key", () => Context = CarnaRunnerCommandLineOptionContext.Of("filter"));
            Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
        }

        [Example("When CommandLineOptionContext has a key that is not /f and /filter")]
        void Ex03()
        {
            Given("a context that has a key that is not /f and /filter", () => Context = CarnaRunnerCommandLineOptionContext.Of("/fil"));
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
    }
}
