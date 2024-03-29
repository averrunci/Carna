﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Context("Applies the assembly option")]
class AssemblyOptionSpec_ApplyOption : FixtureSteppable, IDisposable
{
    AssemblyOption Option { get; } = new();
    CarnaRunnerCommandLineOptions Options { get; } = new();
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    string? AssemblyFilePath { get; set; }

    public void Dispose()
    {
        if (File.Exists(AssemblyFilePath)) File.Delete(AssemblyFilePath);
    }

    [Example("When an assembly file path exists")]
    void Ex01()
    {
        Given("a context that has an assembly file path that exists", () =>
        {
            AssemblyFilePath = Path.GetTempFileName();
            Context = CarnaRunnerCommandLineOptionContext.Of(AssemblyFilePath);
        });
        When("the option is applied", () => Option.Apply(Options, Context));
        Then("the assembly file path should be added", () =>
            Options.Assemblies.Count == 1 && Options.Assemblies[0] == AssemblyFilePath
        );
    }

    [Example("When an assembly file path does not exist")]
    void Ex02()
    {
        Given("a context that has an assembly file path that does not exist", () =>
        {
            AssemblyFilePath = "AssemblyFile.dll";
            Context = CarnaRunnerCommandLineOptionContext.Of(AssemblyFilePath);
        });
        When("the option is applied", () => Option.Apply(Options, Context));
        Then("the InvalidCommandLineOptionException should be thrown", exc => exc.GetType() == typeof(InvalidCommandLineOptionException));
    }
}