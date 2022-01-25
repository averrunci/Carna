// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification("ThenStepRunner Spec")]
class ThenStepRunnerSpec
{
    [Context]
    ThenStepRunnerSpec_StepRunningWithoutException StepRunningWithoutException => default!;

    [Context]
    ThenStepRunnerSpec_StepRunningWithException StepRunningWithException => default!;

    [Context]
    ThenStepRunnerSpec_Constrains Constrains => default!;

    [Context]
    ThenStepRunnerSpec_StepRunningWithoutExceptionAsync StepRunningWithoutExceptionAsync => default!;

    [Context]
    ThenStepRunnerSpec_StepRunningWithExceptionAsync StepRunningWithExceptionAsync => default!;

    [Context]
    ThenStepRunnerSpec_StepRunningWithTypedException StepRunningWithTypedException => default!;

    [Context]
    ThenStepRunnerSpec_StepRunningWithTypedExceptionAsync StepRunningWithTypedExceptionAsync => default!;
}