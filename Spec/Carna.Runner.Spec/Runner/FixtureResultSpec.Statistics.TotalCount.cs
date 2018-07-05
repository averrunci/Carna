// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;

namespace Carna.Runner
{
    [Context("Total count")]
    class FixtureResultSpec_Statistics_TotalCount : FixtureSteppable
    {
        IEnumerable<FixtureResult> FixtureResults { get; set; }

        FixtureDescriptor ContainerFixtureDescriptor { get; } = new FixtureDescriptor("ContainerTest", new ContextAttribute());
        FixtureDescriptor FixtureDescriptor { get; } = new FixtureDescriptor("Test", new ExampleAttribute());

        [Example("When Enumerable of FixtureResult does not have any sub results")]
        void Ex01()
        {
            Given(
                "5 FixtureResults",
                () => FixtureResults = new[]
                {
                    FixtureResult.Of(FixtureDescriptor).Ready(),
                    FixtureResult.Of(FixtureDescriptor).Pending(),
                    FixtureResult.Of(FixtureDescriptor).Passed(),
                    FixtureResult.Of(FixtureDescriptor).Failed(null),
                    FixtureResult.Of(FixtureDescriptor).Passed()
                }
            );
            Expect("the total count should be 5", () => FixtureResults.TotalCount() == 5);
        }

        [Example("When Enumerable of FixtureResult has any sub results, the count includes the count of sub results")]
        void Ex02()
        {
            Given(
                "2 FixtureResults (one has 3 sub FixtureResults, the other has 2 sub FixtureResults)",
                () => FixtureResults = new[]
                {
                    FixtureResult.Of(FixtureDescriptor).FinishedWith(new[]
                    {
                        FixtureResult.Of(FixtureDescriptor).Passed(),
                        FixtureResult.Of(FixtureDescriptor).Failed(null),
                        FixtureResult.Of(FixtureDescriptor).Pending()
                    }),
                    FixtureResult.Of(FixtureDescriptor).FinishedWith(new[]
                    {
                        FixtureResult.Of(FixtureDescriptor).Passed(),
                        FixtureResult.Of(FixtureDescriptor).Failed(null)
                    })
                }
            );
            Expect("the total count should be 5", () => FixtureResults.TotalCount() == 5);
        }

        [Example("When Enumerable of FixtureResult has any container fixtures, the count excludes the count of container fixtures")]
        void Ex03()
        {
            Given(
                "3 FixtureResults (one is a container fixture that has 3 FixtureResults, the others are not container fixtures)",
                () => FixtureResults = new[]
                {
                    FixtureResult.Of(FixtureDescriptor).Ready(),
                    FixtureResult.Of(ContainerFixtureDescriptor).FinishedWith(new[]
                    {
                        FixtureResult.Of(FixtureDescriptor).Passed(),
                        FixtureResult.Of(FixtureDescriptor).Failed(null),
                        FixtureResult.Of(FixtureDescriptor).Pending()
                    }),
                    FixtureResult.Of(FixtureDescriptor).Passed()
                }
            );
            Expect("the total count should be 5", () => FixtureResults.TotalCount() == 5);
        }
    }
}
