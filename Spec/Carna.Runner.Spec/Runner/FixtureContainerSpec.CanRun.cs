// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using NSubstitute;

namespace Carna.Runner;

[Context("Determines whether FixtureContainer can run fixtures")]
class FixtureContainerSpec_CanRun : FixtureSteppable
{
    IFixture Container { get; }
    IFixtureFilter Filter { get; }

    public FixtureContainerSpec_CanRun()
    {
        Container = new FixtureContainer(typeof(TestFixtures.SimpleFixture));
        Filter = Substitute.For<IFixtureFilter>();
    }

    [Example("When a filter that is null is specified")]
    void Ex01()
    {
        Expect("the result should be true", () => Container.CanRun(null) == true);
    }

    [Example("When a filter that returns true is specified")]
    void Ex02()
    {
        Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(true);

        Expect("the result should be true", () => Container.CanRun(Filter) == true);
    }

    [Example("When a filter that returns false is specified")]
    void Ex03()
    {
        Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(false);

        Expect("the result should be false", () => Container.CanRun(Filter) == false);
    }

    [Example("When a filter that returns true in the container fixture and returns false in all inner fixtures is specified")]
    void Ex04()
    {
        ((FixtureContainer)Container).AddRange(new[] {
            TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
            TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
            TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod")
        });

        Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(x => x.Arg<FixtureDescriptor>().Name != "FixtureMethod");

        Expect("the result should be true", () => Container.CanRun(Filter) == true);
    }

    [Example("When a filter that returns false in the container fixture and returns true in any inner fixtures is specified")]
    void Ex05()
    {
        ((FixtureContainer)Container).AddRange(new[] {
            TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
            TestFixtures.CreateFixture<TestFixtures.SimpleDisposableFixture>("FixtureMethod"),
            TestFixtures.CreateFixture<TestFixtures.SimpleFixtureSteppable>("FixtureMethod")
        });

        Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(x => x.Arg<FixtureDescriptor>().FullName == "Carna.TestFixtures+SimpleDisposableFixture.FixtureMethod");

        Expect("the result should be true", () => Container.CanRun(Filter) == true);
    }
}