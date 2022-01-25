// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("End time")]
class FixtureResultSpec_Statistics_EndTime : FixtureSteppable
{
    IEnumerable<FixtureResult> FixtureResults { get; set; } = default!;

    FixtureDescriptor ContainerFixtureDescriptor { get; } = new("ContainerTest", new ContextAttribute());
    FixtureDescriptor FixtureDescriptor { get; } = new("Test", new ExampleAttribute());

    [Example("When Enumerable of FixtureResult does not have any sub results")]
    void Ex01()
    {
        Given(
            "5 FixtureResults ()",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 3)).Pending(),
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 2)).Pending(),
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 1)).Passed(),
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 5)).Failed(null),
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 4)).Pending()
            }
        );
        Expect("the start time should be the maximum end time in 5 FixtureResults", () => FixtureResults.EndTime() == new DateTime(2017, 1, 5));
    }

    [Example("When Enumerable of FixtureResult has any sub fixtures, the end time of sub fixtures is ignored")]
    void Ex02()
    {
        Given(
            "3 FixtureResults (one is a container fixture that has 3 FixtureResults, the others are not container fixtures)",
            () => FixtureResults = new[]
            {
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 3)).Ready(),
                FixtureResult.Of(ContainerFixtureDescriptor).EndAt(new DateTime(2017, 1, 1)).FinishedWith(new[]
                {
                    FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 2, 1)).Pending(),
                    FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 2, 2)).Pending(),
                    FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 2, 3)).Pending()
                }),
                FixtureResult.Of(FixtureDescriptor).EndAt(new DateTime(2017, 1, 2)).Pending()
            }
        );
        Expect("the start time should be the maximum end time in 3 FixtureResults", () => FixtureResults.EndTime() == new DateTime(2017, 1, 3));
    }

    [Example("When Enumerable of FixtureResult is empty")]
    void Ex03()
    {
        Given("0 FixtureResult", () => FixtureResults = Enumerable.Empty<FixtureResult>());
        Expect("the end time should be DateTime.MinValue", () => FixtureResults.EndTime() == DateTime.MinValue);
    }
}