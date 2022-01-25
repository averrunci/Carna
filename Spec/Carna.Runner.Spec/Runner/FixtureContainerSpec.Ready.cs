// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Ready")]
class FixtureContainerSpec_Ready : FixtureSteppable
{
    IFixture Container { get; }

    FixtureDescriptorAssertion ExpectedContainerFixtureDescription { get; set; } = default!;
    FixtureResultAssertion ExpectedContainerFixtureResult { get; set; } = default!;

    FixtureDescriptorAssertion ExpectedFixtureDescription { get; set; } = default!;
    FixtureResultAssertion ExpectedFixtureResult { get; set; } = default!;

    public FixtureContainerSpec_Ready()
    {
        Container = new FixtureContainer(typeof(TestFixtures.SimpleFixture));
    }

    [Example("When Ready method is called")]
    void Ex01()
    {
        var fixture = TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod");
        FixtureResult? fixtureReadyResult = default;
        fixture.FixtureReady += (_, e) => fixtureReadyResult = e.Result;
        ((FixtureContainer)Container).Add(fixture);

        FixtureResult? containerReadyResult = default;
        Container.FixtureReady += (_, e) => containerReadyResult = e.Result;

        Container.Ready();

        Expect("FixtureReady event should be raised", () => containerReadyResult != null);

        ExpectedContainerFixtureDescription = FixtureDescriptorAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute));
        Expect($"the descriptor of the result should be as follows:{ExpectedContainerFixtureDescription.ToDescription()}", () => containerReadyResult != null && FixtureDescriptorAssertion.Of(containerReadyResult.FixtureDescriptor) == ExpectedContainerFixtureDescription);
        ExpectedContainerFixtureResult = FixtureResultAssertion.ForNullException(false, false, false, 0, 0, FixtureStatus.Ready);
        Expect($"the result should be as follows:{ExpectedContainerFixtureResult.ToDescription()}", () => containerReadyResult != null && FixtureResultAssertion.Of(containerReadyResult) == ExpectedContainerFixtureResult);

        Expect("FixtureReady event of the inner fixture should be raised", () => fixtureReadyResult != null);

        ExpectedFixtureDescription = FixtureDescriptorAssertion.Of("Fixture Method Example", "FixtureMethod", "Carna.TestFixtures+SimpleFixture.FixtureMethod", typeof(ExampleAttribute));
        Expect($"the description of the result of the inner fixture should be as follows:{ExpectedFixtureDescription.ToDescription()}", () => fixtureReadyResult != null && FixtureDescriptorAssertion.Of(fixtureReadyResult.FixtureDescriptor) == ExpectedFixtureDescription);
        ExpectedFixtureResult = FixtureResultAssertion.ForNullException(false, false, false, 0, 0, FixtureStatus.Ready);
        Expect($"the result of the inner fixture should be as follows:{ExpectedFixtureResult.ToDescription()}", () => fixtureReadyResult != null && FixtureResultAssertion.Of(fixtureReadyResult) == ExpectedFixtureResult);
    }
}