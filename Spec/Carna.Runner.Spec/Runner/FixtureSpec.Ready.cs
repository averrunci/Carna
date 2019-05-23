// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Context("Ready")]
    class FixtureSpec_Ready : FixtureSteppable
    {
        IFixture Fixture { get; }

        FixtureDescriptorAssertion ExpectedFixtureDescriptor { get; set; }
        FixtureResultAssertion ExpectedFixtureResult { get; set; }

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

            ExpectedFixtureDescriptor = FixtureDescriptorAssertion.Of("Fixture Method Example", "FixtureMethod", "Carna.TestFixtures+SimpleFixture.FixtureMethod", typeof(ExampleAttribute));
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(false, false, false, 0, 0, FixtureStatus.Ready);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);
        }
    }
}
