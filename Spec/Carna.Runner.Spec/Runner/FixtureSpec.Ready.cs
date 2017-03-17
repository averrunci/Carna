// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;

namespace Carna.Runner
{
    [Context("Ready")]
    class FixtureSpec_Ready : FixtureSteppable
    {
        private IFixture Fixture { get; }

        public FixtureSpec_Ready()
        {
            Fixture = TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod");
        }

        [Example("When Ready method is called")]
        void Ex01()
        {
            FixtureResult result = null;
            Fixture.FixtureReady += (s, e) => result = e.Result;
            Fixture.Ready();

            Expect("FixtureReady event should be raised", () => result != null);

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == "Fixture Method Example");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == "FixtureMethod");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture.FixtureMethod");
            Expect("the fixture attribute type of the descriptor of the reuslt should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should not have value", () => !result.StartTime.HasValue);
            Expect("the end time of the result should not have value", () => !result.EndTime.HasValue);
            Expect("the duration of the reuslt should not have value", () => !result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Ready", () => result.Status == FixtureStatus.Ready);
        }
    }
}
