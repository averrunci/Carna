// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Failed count")]
class FixtureResultSpec_Statistics_FailedCount : FixtureSteppable
{
    IEnumerable<FixtureResult> FixtureResults { get; set; } = default!;

    FixtureDescriptor ContainerFixtureDescriptor { get; } = new("ContainerTest", new ContextAttribute());
    FixtureDescriptor FixtureDescriptor { get; } = new("Test", new ExampleAttribute());

    [Example("When Enumerable of FixtureResult does not have any sub results")]
    void Ex01()
    {
        Given(
            "5 FixtureResults (3 FixtureResults are Failed)",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).Failed(null),
                FixtureResult.Of(FixtureDescriptor).Pending(),
                FixtureResult.Of(FixtureDescriptor).Failed(null),
                FixtureResult.Of(FixtureDescriptor).Failed(null),
                FixtureResult.Of(FixtureDescriptor).Passed()
            }
        );
        Expect("the failed count should be 3", () => FixtureResults.FailedCount() == 3);
    }

    [Example("When Enumerable of FixtureResult has any sub results, the count includes the count of sub results")]
    void Ex02()
    {
        Given(
            "2 FixtureResults (one has 3 sub FixtureResults (1 FixtureResult is Failed), the other has 2 sub FixtureResults (1 FixtureResult is Failed))",
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
        Expect("the failed count should be 2", () => FixtureResults.FailedCount() == 2);
    }

    [Example("When Enumerable of FixtureResult has any container fixtures, the count excludes the count of container fixtures")]
    void Ex03()
    {
        Given(
            "3 FixtureResults (one is a container fixture that has 3 FixtureResults (1 FixtureResult is Passed), the others are not container fixtures (1 FixtureResult is Failed))",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(ContainerFixtureDescriptor).FinishedWith(new[]
                {
                    FixtureResult.Of(FixtureDescriptor).Passed(),
                    FixtureResult.Of(FixtureDescriptor).Failed(null),
                    FixtureResult.Of(FixtureDescriptor).Passed()
                }),
                FixtureResult.Of(FixtureDescriptor).Failed(null)
            }
        );
        Expect("the failed count should be 2", () => FixtureResults.FailedCount() == 2);
    }
}