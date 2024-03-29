﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification(
    $"{nameof(WhenStepRunner)} Spec",
    typeof(WhenStepRunnerSpec_StepRunning),
    typeof(WhenStepRunnerSpec_Constrains),
    typeof(WhenStepRunnerSpec_StepRunningAsync),
    typeof(WhenStepRunnerSpec_StepRunningWithTimeout)
)]
class WhenStepRunnerSpec;