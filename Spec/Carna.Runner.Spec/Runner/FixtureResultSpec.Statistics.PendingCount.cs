// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Pending count")]
class FixtureResultSpec_Statistics_PendingCount : FixtureSteppable
{
    IEnumerable<FixtureResult> FixtureResults { get; set; } = default!;

    FixtureDescriptor ContainerFixtureDescriptor { get; } = new("ContainerTest", new ContextAttribute());
    FixtureDescriptor FixtureDescriptor { get; } = new("Test", new ExampleAttribute());

    [Example("When Enumerable of FixtureResult does not have any sub results")]
    void Ex01()
    {
        Given(
            "5 FixtureResults (3 FixtureResults are Pending)",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).Pending(),
                FixtureResult.Of(FixtureDescriptor).Pending(),
                FixtureResult.Of(FixtureDescriptor).Passed(),
                FixtureResult.Of(FixtureDescriptor).Failed(null),
                FixtureResult.Of(FixtureDescriptor).Pending()
            }
        );
        Expect("the pending count should be 3", () => FixtureResults.PendingCount() == 3);
    }

    [Example("When Enumerable of FixtureResult has any sub results, the count includes the count of sub results")]
    void Ex02()
    {
        Given(
            "2 FixtureResults (one has 3 sub FixtureResults (1 FixtureResult is Pending), the other has 2 sub FixtureResults (1 FixtureResult is Pending))",
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
                    FixtureResult.Of(FixtureDescriptor).Pending()
                })
            }
        );
        Expect("the pending count should be 2", () => FixtureResults.PendingCount() == 2);
    }

    [Example("When Enumerable of FixtureResult has any container fixtures, the count excludes the count of container fixtures")]
    void Ex03()
    {
        Given(
            "3 FixtureResults (one is a container fixture that has 3 FixtureResults (all FixtureResults are Pending), the others are not container fixtures (1 FixtureResult is Pending))",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(ContainerFixtureDescriptor).FinishedWith(new[]
                {
                    FixtureResult.Of(FixtureDescriptor).Pending(),
                    FixtureResult.Of(FixtureDescriptor).Pending(),
                    FixtureResult.Of(FixtureDescriptor).Pending()
                }),
                FixtureResult.Of(FixtureDescriptor).Pending()
            }
        );
        Expect("the pending count should be 4", () => FixtureResults.PendingCount() == 4);
    }
}