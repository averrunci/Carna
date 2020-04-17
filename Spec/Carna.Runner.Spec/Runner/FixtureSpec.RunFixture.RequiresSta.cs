// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;

using Carna.Runner.Step;

namespace Carna.Runner
{
    [Context("Fixture requires a single thread apartment")]
    class FixtureSpec_RunFixture_RequiresSta : FixtureSteppable
    {
        FixtureResult Result { get; set; }

        [Example("a fixture requires a single thread apartment")]
        [Sample(Source = typeof(StaFixtureAttributeSampleDataSource))]
        void Ex01(IFixture fixture)
        {
            When("to run the fixture", () => Result = fixture.Run(null, new FixtureStepRunnerFactory()));
            Then("the fixture should be run in a single thread apartment", () => Result.Status == FixtureStatus.Passed);
        }

        class StaFixtureAttributeSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new
                {
                    Description = "When a class requires a single thread apartment",
                    Fixture = TestFixtures.CreateFixture<TestFixtures.FixtureThatRequiresSta>("Ex01")
                };
                yield return new
                {
                    Description = "When a method requires a single thread apartment",
                    Fixture = TestFixtures.CreateFixture<TestFixtures.MethodFixtureThatRequiresSta>("Ex01")
                };
            }
        }
    }
}