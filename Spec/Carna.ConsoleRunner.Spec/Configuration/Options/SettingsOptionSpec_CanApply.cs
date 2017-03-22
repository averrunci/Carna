// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Context("Determines whether the settings option can be applied")]
    class SettingsOptionSpec_CanApply : FixtureSteppable
    {
        private SettingsOption Option { get; } = new SettingsOption();
        private CarnaRunnerCommandLineOptionContext Context { get; set; }

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

        [Example("When CommandLineOptionContext has a key that is not /s and /settings")]
        void Ex03()
        {
            Given("a context that has a key that is not /s and /settings", () => Context = CarnaRunnerCommandLineOptionContext.Of("/set"));
            Expect("the option should not be able to be applied", () => !Option.CanApply(Context));
        }

        [Example("When CommandLineOptionContext has a key that is /s")]
        void Ex04()
        {
            Given("a context that has a key that is /s", () => Context = CarnaRunnerCommandLineOptionContext.Of("/s"));
            Expect("the option should be able to be applied", () => Option.CanApply(Context));
        }

        [Example("When CommandLineOptionContext has a key that is /settings")]
        void Ex05()
        {
            Given("a context that has a key that is /settings", () => Context = CarnaRunnerCommandLineOptionContext.Of("/settings"));
            Expect("the option should be able to be applied", () => Option.CanApply(Context));
        }
    }
}
