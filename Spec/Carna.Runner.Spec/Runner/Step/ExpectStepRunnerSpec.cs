// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification("ExpectStepRunner Spec")]
class ExpectStepRunnerSpec
{
    [Context]
    ExpectStepRunnerSpec_StepRunning StepRunning => default!;

    [Context]
    ExpectStepRunnerSpec_Constrains Constrains => default!;

    [Context]
    ExpectStepRunnerSpec_StepRunningAsync StepRunningAsync => default!;
}