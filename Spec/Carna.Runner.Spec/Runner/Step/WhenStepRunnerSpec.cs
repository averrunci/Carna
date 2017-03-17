// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step
{
    [Specification("WhenStepRunner Spec")]
    class WhenStepRunnerSpec
    {
        [Context]
        WhenStepRunnerSpec_StepRunning StepRunning { get; }

        [Context]
        WhenStepRunnerSpec_Constrains Constrains { get; }

        [Context]
        WhenStepRunnerSpec_StepRunningAsync StepRunningAsync { get; }
    }
}
