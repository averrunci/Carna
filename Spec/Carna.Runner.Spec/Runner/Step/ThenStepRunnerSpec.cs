// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification(
    "ThenStepRunner Spec",
    typeof(ThenStepRunnerSpec_StepRunningWithoutException),
    typeof(ThenStepRunnerSpec_StepRunningWithException),
    typeof(ThenStepRunnerSpec_Constrains),
    typeof(ThenStepRunnerSpec_StepRunningWithoutExceptionAsync),
    typeof(ThenStepRunnerSpec_StepRunningWithExceptionAsync),
    typeof(ThenStepRunnerSpec_StepRunningWithTypedException),
    typeof(ThenStepRunnerSpec_StepRunningWithTypedExceptionAsync)
)]
class ThenStepRunnerSpec
{
}