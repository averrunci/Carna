// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification("GivenStepRunner Spec")]
class GivenStepRunnerSpec
{
    [Context]
    GivenStepRunnerSpec_StepRunning StepRunning => default!;

    [Context]
    GivenStepRunnerSpec_Constrains Constrains => default!;

    [Context]
    GivenStepRunnerSpec_StepRunningAsync StepRunningAsync => default!;
}