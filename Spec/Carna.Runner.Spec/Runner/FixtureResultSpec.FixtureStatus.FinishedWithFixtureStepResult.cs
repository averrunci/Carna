// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner.Step;
using Carna.Step;

namespace Carna.Runner;

[Context("Finished with FixtureStepResult")]
class FixtureResultSpec_FixtureStatus_FinishedWithFixtureStepResult : FixtureSteppable
{
    FixtureDescriptor FixtureDescriptor { get; } = new("Test", new ContextAttribute());
    FixtureStep Step { get; } = new ExpectStep(string.Empty, typeof(FixtureResultSpec_FixtureStatus_FinishedWithFixtureStepResult), string.Empty, string.Empty, 0);

    [Example("When specified Enumerable of FixtureStepResult is an empty")]
    void Ex01()
    {
        Expect(
            "the status should be Ready",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(Enumerable.Empty<FixtureStepResult>()).Status == FixtureStatus.Ready
        );
    }

    [Example("When all status of specified Enumerable of FixtureStepResult are Ready")]
    void Ex02()
    {
        Expect(
            "the status should be Ready",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).Ready().Build()
            }).Status == FixtureStatus.Ready
        );
    }

    [Example("When all status (except None) of specified Enumerable of FixtureStepResult are Ready")]
    void Ex03()
    {
        Expect(
            "the status should be Ready",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Ready().Build()
            }).Status == FixtureStatus.Ready
        );
    }

    [Example("When all status of specified Enumerable of FixtureStepResult are Pending")]
    void Ex04()
    {
        Expect(
            "the status should be Pending",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Pending().Build(),
                FixtureStepResult.Of(Step).Pending().Build(),
                FixtureStepResult.Of(Step).Pending().Build()
            }).Status == FixtureStatus.Pending
        );
    }

    [Example("When all status (except None) of specified Enumerable of FixtureStepResult are Pending")]
    void Ex05()
    {
        Expect(
            "the status should be Pending",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Pending().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Pending().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Pending().Build()
            }).Status == FixtureStatus.Pending
        );
    }

    [Example("When specified Enumerable of FixtureStepResult has any FixtureStepResult the status of which is Failed")]
    void Ex06()
    {
        Expect(
            "the status should be Failed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Passed().Build(),
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Pending().Build(),
                FixtureStepResult.Of(Step).Failed(null).Build()
            }).Status == FixtureStatus.Failed
        );
    }

    [Example("When all status of specified Enumerable of FixtureStepResult are Passed")]
    void Ex07()
    {
        Expect(
            "the status should be Passed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Passed().Build(),
                FixtureStepResult.Of(Step).Passed().Build(),
                FixtureStepResult.Of(Step).Passed().Build()
            }).Status == FixtureStatus.Passed
        );
    }

    [Example("When the status of specified Enumerable of FixtureStepResult is Ready, None, Pending or Passed")]
    void Ex08()
    {
        Expect(
            "the status should be Passed",
            () => FixtureResult.Of(FixtureDescriptor).FinishedWith(new[] {
                FixtureStepResult.Of(Step).Passed().Build(),
                FixtureStepResult.Of(Step).Ready().Build(),
                FixtureStepResult.Of(Step).None().Build(),
                FixtureStepResult.Of(Step).Passed().Build()
            }).Status == FixtureStatus.Passed
        );
    }
}