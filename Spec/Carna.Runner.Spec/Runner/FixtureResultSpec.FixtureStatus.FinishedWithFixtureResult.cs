// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Finished with FixtureResult")]
class FixtureResultSpec_FixtureStatus_FinishedWithFixtureResult : FixtureSteppable
{
    FixtureDescriptor FixtureDescriptor { get; } = new("Test", new ContextAttribute());

    [Example("When specified Enumerable of FixtureResult is an empty")]
    void Ex01()
    {
        Expect(
            "the status should be Ready",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(Enumerable.Empty<FixtureResult>()).Status == FixtureStatus.Ready
        );
    }

    [Example("When all status of specified Enumerable of FixtureResult are Ready")]
    void Ex02()
    {
        Expect(
            "the status should be Ready",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(FixtureDescriptor).Ready()
            }).Status == FixtureStatus.Ready
        );
    }

    [Example("When all status of specified Enumerable of FixtureResult are Pending")]
    void Ex03()
    {
        Expect(
            "the status should be Pending",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureResult.Of(FixtureDescriptor).Pending(),
                FixtureResult.Of(FixtureDescriptor).Pending(),
                FixtureResult.Of(FixtureDescriptor).Pending()
            }).Status == FixtureStatus.Pending
        );
    }

    [Example("When specified Enumerable of FixtureStep has any FixtureResult the status of which is Failed")]
    void Ex04()
    {
        Expect(
            "the status should be Failed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureResult.Of(FixtureDescriptor).Passed(),
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(FixtureDescriptor).Failed(null)
            }).Status == FixtureStatus.Failed
        );
    }

    [Example("When all status of specified Enumerable of FixtureResult are Passed")]
    void Ex05()
    {
        Expect(
            "the status should be Passed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureResult.Of(FixtureDescriptor).Passed(),
                FixtureResult.Of(FixtureDescriptor).Passed(),
                FixtureResult.Of(FixtureDescriptor).Passed()
            }).Status == FixtureStatus.Passed
        );
    }

    [Example("When the status of specified Enumerable of FixtureResult is Ready or Passed")]
    void Ex06()
    {
        Expect(
            "the status should be Passed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureResult.Of(FixtureDescriptor).Passed(),
                FixtureResult.Of(FixtureDescriptor).Ready(),
                FixtureResult.Of(FixtureDescriptor).Passed()
            }).Status == FixtureStatus.Passed
        );
    }
}