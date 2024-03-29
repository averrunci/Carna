﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Applied the settings option")]
class SettingsOptionSpec_ApplyOption : FixtureSteppable, IDisposable
{
    SettingsOption Option { get; } = new();
    CarnaRunnerCommandLineOptions Options { get; } = new();
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    string? SettingsFilePath { get; set; }

    public void Dispose()
    {
        if (File.Exists(SettingsFilePath)) File.Delete(SettingsFilePath);
    }

    [Example("When a settings file path exists")]
    void Ex01()
    {
        Given("a context that has an assembly file path that exists", () =>
        {
            SettingsFilePath = Path.GetTempFileName();
            Context = CarnaRunnerCommandLineOptionContext.Of($"/s:{SettingsFilePath}");
        });
        When("the option is applied", () => Option.Apply(Options, Context));
        Then("the settings file path should be set", () => Options.SettingsFilePath == SettingsFilePath);
    }

    [Example("When a settings file path does not exist")]
    void Ex02()
    {
        Given("a context that has a settings file path that does not exist", () =>
        {
            SettingsFilePath = "settings.json";
            Context = CarnaRunnerCommandLineOptionContext.Of($"/s:{SettingsFilePath}");
        });
        When("the option is applied", () => Option.Apply(Options, Context));
        Then("the InvalidCommandLineOptionException should be thrown", exc => exc.GetType() == typeof(InvalidCommandLineOptionException));
    }
}