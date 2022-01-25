// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration;

[Specification("CarnaRunnerCommandLineOptionContext Spec")]
class CarnaRunnerCommandLineOptionContextSpec : FixtureSteppable
{
    CarnaRunnerCommandLineOptionContext Context { get; set; } = default!;

    [Example("When the specified argument is null")]
    void Ex01()
    {
        When("a context is created with the specified argument that is an empty", () => Context = CarnaRunnerCommandLineOptionContext.Of(string.Empty));
        Then("the argument of the context should be empty", () => Context.Argument == string.Empty);
        Then("the key of the context should be null", () => Context.Key == null);
        Then("the value of the context should be null", () => Context.Value == null);
        Then("the context should not have a key", () => !Context.HasKey);
    }

    [Example("When an the specified argument does not start with '/'")]
    void Ex02()
    {
        When("a context is created with the specified argument that does not start with '/'", () => Context = CarnaRunnerCommandLineOptionContext.Of("assembly"));
        Then("the argument of the context should be the specified argument", () => Context.Argument == "assembly");
        Then("the key of the context should be null", () => Context.Key == null);
        Then("the value of the context should be null", () => Context.Value == null);
        Then("the context should not have a key", () => !Context.HasKey);
    }

    [Example("When an the specified argument stars with '/' and is not separated by ':'")]
    void Ex03()
    {
        When("a context is created with the specified argument that starts with '/' and is not separated by ':'", () => Context = CarnaRunnerCommandLineOptionContext.Of("/help"));
        Then("the argument of the context should be the specified argument", () => Context.Argument == "/help");
        Then("the key of the context should be the specified argument", () => Context.Key == "/help");
        Then("the value of the context should be null", () => Context.Value == null);
        Then("the context should have a key", () => Context.HasKey);
    }

    [Example("When an the specified argument stars with '/' and is separated by ':'")]
    void Ex04()
    {
        When("a context is created with the specified argument that starts with '/' and is separated by ':'", () => Context = CarnaRunnerCommandLineOptionContext.Of("/f:Test"));
        Then("the argument of the context should be the specified argument", () => Context.Argument == "/f:Test");
        Then("the key of the context should be the first part separated the specified argument by ':'", () => Context.Key == "/f");
        Then("the value of the context should be the second part separated the specified argument by ':'", () => Context.Value == "Test");
        Then("the context should have a key", () => Context.HasKey);
    }
}