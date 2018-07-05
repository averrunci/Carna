// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;

namespace Carna.Runner
{
    [Context("Ready")]
    class FixtureContainerSpec_Ready : FixtureSteppable
    {
        IFixture Container { get; }

        public FixtureContainerSpec_Ready()
        {
            Container = new FixtureContainer(typeof(TestFixtures.SimpleFixture));
        }

        [Example("When Ready method is called")]
        void Ex01()
        {
            var fixture = TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod");
            FixtureResult fixtureReadyResult = null;
            fixture.FixtureReady += (s, e) => fixtureReadyResult = e.Result;
            ((FixtureContainer)Container).Add(fixture);

            FixtureResult containerReadyResult = null;
            Container.FixtureReady += (s, e) => containerReadyResult = e.Result;

            Container.Ready();

            Expect("FixtureReady event should be raised", () => containerReadyResult != null);

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => containerReadyResult.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => containerReadyResult.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => containerReadyResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the fixture attribute type of the descriptor of the result should be ContextAttribute", () => containerReadyResult.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result should not have value", () => !containerReadyResult.StartTime.HasValue);
            Expect("the end time of the result should not have value", () => !containerReadyResult.EndTime.HasValue);
            Expect("the duration of the result should not have value", () => !containerReadyResult.Duration.HasValue);
            Expect("the exception of the result should be null", () => containerReadyResult.Exception == null);
            Expect("the steps of the result should be empty", () => !containerReadyResult.StepResults.Any());
            Expect("the results of the result should be empty", () => !containerReadyResult.Results.Any());
            Expect("the status of the result should be Ready", () => containerReadyResult.Status == FixtureStatus.Ready);

            Expect("FixtureReady event of the inner fixture should be raised", () => fixtureReadyResult != null);

            Expect("the description of the descriptor of the result of the inner fixture should be the value specified to the fixture method", () => fixtureReadyResult.FixtureDescriptor.Description == "Fixture Method Example");
            Expect("the name of the descriptor of the result of the inner fixture should be the name of the fixture method", () => fixtureReadyResult.FixtureDescriptor.Name == "FixtureMethod");
            Expect("the full name of the descriptor of the result of the inner fixture should be the full name of the fixture method", () => fixtureReadyResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture.FixtureMethod");
            Expect("the fixture attribute type of the descriptor of the result of the inner fixture should be ExampleAttribute", () => fixtureReadyResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result of the inner fixture should not have value", () => !fixtureReadyResult.StartTime.HasValue);
            Expect("the end time of the result of the inner fixture should not have value", () => !fixtureReadyResult.EndTime.HasValue);
            Expect("the duration of the result of the inner fixture should not have value", () => !fixtureReadyResult.Duration.HasValue);
            Expect("the exception of the result of the inner fixture should be null", () => fixtureReadyResult.Exception == null);
            Expect("the steps of the result of the inner fixture should be empty", () => !fixtureReadyResult.StepResults.Any());
            Expect("the results of the result of the inner fixture should be empty", () => !fixtureReadyResult.Results.Any());
            Expect("the status of the result of the inner fixture should be Ready", () => fixtureReadyResult.Status == FixtureStatus.Ready);
        }
    }
}
