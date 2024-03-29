﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration;

[Specification($"{nameof(CarnaRunnerCommandLineParser)} Spec")]
class CarnaRunnerCommandLineParserSpec : FixtureSteppable, IDisposable
{
    ICarnaRunnerCommandLineParser Parser { get; } = new CarnaRunnerCommandLineParser();
    CarnaRunnerCommandLineOptions Options { get; set; } = default!;

    CarnaRunnerCommandLineOptions DefaultOptions { get; } = new();
    string? SettingFileBackupPath { get; }

    [Background("A default setting file does not exist")]
    public CarnaRunnerCommandLineParserSpec()
    {
        if (!File.Exists(DefaultOptions.SettingsFilePath)) return;

        SettingFileBackupPath = $"{DefaultOptions.SettingsFilePath}.bak";
        File.Move(DefaultOptions.SettingsFilePath, SettingFileBackupPath);
        File.Delete(DefaultOptions.SettingsFilePath);
    }

    public void Dispose()
    {
        if (File.Exists(DefaultOptions.SettingsFilePath))
        {
            File.Delete(DefaultOptions.SettingsFilePath);
        }
        if (SettingFileBackupPath is not null)
        {
            File.Move(SettingFileBackupPath, DefaultOptions.SettingsFilePath);
        }
    }

    [Example("When an argument is not specified and the default settings file does not exist")]
    void Ex01()
    {
        Expect("the default settings file should not exist", () => !File.Exists(DefaultOptions.SettingsFilePath));
        When("the argument that is an empty is parsed", () => Options = Parser.Parse(Array.Empty<string>()));
        Then("the InvalidCommandLineOptionException should be thrown", exc => exc.GetType() == typeof(InvalidCommandLineOptionException));
    }

    [Example("When an argument is not specified and the default settings file exists")]
    void Ex02()
    {
        Given("a default settings file", () => File.WriteAllText(DefaultOptions.SettingsFilePath, "{}"));
        Expect("the default settings file should exist", () => File.Exists(DefaultOptions.SettingsFilePath));
        When("the argument that is an empty is parsed", () => Options = Parser.Parse(Array.Empty<string>()));
        Then("the default options should be returned", () =>
            !Options.Assemblies.Any() && Options.Filter == null && !Options.HasHelp && Options.SettingsFilePath == DefaultOptions.SettingsFilePath
        );
    }

    [Example("When an argument is specified and contains an unknown option")]
    void Ex03()
    {
        When("the argument that contains an unknown option is parsed", () => Options = Parser.Parse(new[] { "/h", "/ignore:Spec", "/filter:Test" }));
        Then("the InvalidCommandLineOptionException should be thrown", exc => exc.GetType() == typeof(InvalidCommandLineOptionException));
    }

    [Example("When an argument is specified and contains all known options")]
    void Ex04()
    {
        When("the argument that contains all known options is parsed", () => Options = Parser.Parse(new[] { "/h", "/filter:Test" }));
        Then("the parsed options should be returned", () =>
            !Options.Assemblies.Any()  && Options.Filter == "Test" && Options.HasHelp && Options.SettingsFilePath == DefaultOptions.SettingsFilePath
        );
    }
}