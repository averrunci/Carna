// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Specification($"{nameof(FixtureFilter)} Spec")]
class FixtureFilterSpec : FixtureSteppable
{
    FixtureDescriptor FixtureDescriptor { get; set; } = default!;
    string? Pattern { get; set; }

    IFixtureFilter FilterOf(string? pattern) => new FixtureFilter(new Dictionary<string, string?> { ["pattern"] = pattern });

    [Example("When a regular expression that matches a tag is specified")]
    void Ex01()
    {
        Given(
            "FixtureDescriptor the tag of which is Test",
            () => FixtureDescriptor = new FixtureDescriptor("Fixture", new AssemblyFixtureAttribute { Tag = "Test" })
        );
        Given("a pattern of regular expression that is 'T+'", () => Pattern = "T+");
        Expect("the result of a filter should be true", () => FilterOf(Pattern).Accept(FixtureDescriptor));
    }

    [Example("When a regular expression that matches a full name is specified")]
    void Ex02()
    {
        Given(
            "FixtureDescriptor the full name of which is Carna.Runner.Fixture",
            () => FixtureDescriptor = new FixtureDescriptor("Fixture", "Carna.Runner.Fixture", new AssemblyFixtureAttribute())
        );
        Given(@"a pattern of regular expression that is 'Runner\.F+'", () => Pattern = @"Runner\.F+");
        Expect("the result of a filter should be true", () => FilterOf(Pattern).Accept(FixtureDescriptor));
    }

    [Example("When a regular expression that does not match a tag or a full name is specified")]
    void Ex03()
    {
        Given(
            "FixtureDescriptor the tag of which is Test and the full name of which is Carna.Runner.Fixture",
            () => FixtureDescriptor = new FixtureDescriptor("Fixture", "Carna.Runner.Fixture", new AssemblyFixtureAttribute { Tag = "Test" })
        );
        Given(@"a pattern of regular expression that is 'Runner\.T+'", () => Pattern = @"Runner\.T+");
        Expect("the result of a filter should be false", () => !FilterOf(Pattern).Accept(FixtureDescriptor));
    }

    [Example("When a regular expression that is null is specified")]
    void Ex04()
    {
        Given(
            "FixtureDescriptor the tag of which is Test and the full name of which is Carna.Runner.Fixture",
            () => FixtureDescriptor = new FixtureDescriptor("Fixture", "Carna.Runner.Fixture", new AssemblyFixtureAttribute { Tag = "Test" })
        );
        Given("a pattern of regular expression that is null", () => Pattern = null);
        Expect("the result of a filter should be true", () => FilterOf(Pattern).Accept(FixtureDescriptor));
    }
}