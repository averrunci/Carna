// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step
{
    [Specification("GivenStepRunner Spec")]
    class GivenStepRunnerSpec
    {
        [Context]
        GivenStepRunnerSpec_StepRunning StepRunning { get; }

        [Context]
        GivenStepRunnerSpec_Constrains Constrains { get; }

        [Context]
        GivenStepRunnerSpec_StepRunningAsync StepRunningAsync { get; }
    }
}
