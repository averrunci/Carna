// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification("WhenStepRunner Spec")]
class WhenStepRunnerSpec
{
    [Context]
    WhenStepRunnerSpec_StepRunning StepRunning => default!;

    [Context]
    WhenStepRunnerSpec_Constrains Constrains => default!;

    [Context]
    WhenStepRunnerSpec_StepRunningAsync StepRunningAsync => default!;

    [Context]
    WhenStepRunnerSpec_StepRunningWithTimeout StepRunningWithTimeout => default!;
}