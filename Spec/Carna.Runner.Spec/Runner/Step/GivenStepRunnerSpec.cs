// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification(
    $"{nameof(GivenStepRunner)} Spec",
    typeof(GivenStepRunnerSpec_StepRunning),
    typeof(GivenStepRunnerSpec_Constrains),
    typeof(GivenStepRunnerSpec_StepRunningAsync)
)]
class GivenStepRunnerSpec;