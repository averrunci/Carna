// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step
{
    [Specification("ThenStepRunner Spec")]
    class ThenStepRunnerSpec
    {
        [Context]
        ThenStepRunnerSpec_StepRunningWithoutException StepRunningWithoutException { get; }

        [Context]
        ThenStepRunnerSpec_StepRunningWithException StepRunningWithException { get; }

        [Context]
        ThenStepRunnerSpec_Constrains Constrains { get; }

        [Context]
        ThenStepRunnerSpec_StepRunningWithoutExceptionAsync StepRunningWithoutExceptionAsync { get; }

        [Context]
        ThenStepRunnerSpec_StepRunningWithExceptionAsync StepRunningWithExceptionAsync { get; }
    }
}
