// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Carna.Runner.Step;

namespace Carna.Runner;

[Context("Background")]
class FixtureContainerSpec_Background : FixtureSteppable
{
    [Example("Retrieves Background attribute")]
    [Sample(Source = typeof(FixtureContainerSpecBackgroundSampleDataSource))]
    void Ex01(Type containerType, FixtureDescriptorWithBackgroundAssertion expectedDescriptor)
    {
        IFixture container = new FixtureContainer(containerType);
        var result = container.Run(null, new FixtureStepRunnerFactory());

        Expect($"the descriptor of the result should be as follows:{expectedDescriptor.ToDescription()}", () => FixtureDescriptorWithBackgroundAssertion.Of(result!.FixtureDescriptor) == expectedDescriptor);
    }

    class FixtureContainerSpecBackgroundSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "",
                ContainerType = typeof(TestFixtures.SimpleFixture),
                ExpectedDescriptor = FixtureDescriptorWithBackgroundAssertion.Of(
                    "Simple Fixture",
                    "SimpleFixture",
                    "Carna.TestFixtures+SimpleFixture",
                    typeof(ContextAttribute),
                    "Simple Fixture Background"
                )
            };
            yield return new
            {
                Description = "",
                ContainerType = typeof(TestFixtures.SimpleFixtureWithBaseFixture),
                ExpectedDescriptor = FixtureDescriptorWithBackgroundAssertion.Of(
                    "Simple Fixture that extends a base fixture to which Background attribute specifies",
                    "SimpleFixtureWithBaseFixture",
                    "Carna.TestFixtures+SimpleFixtureWithBaseFixture",
                    typeof(ContextAttribute),
                    @"Simple Fixture Background
Simple Fixture with Base Fixture Background"
                )
            };
        }
    }
}