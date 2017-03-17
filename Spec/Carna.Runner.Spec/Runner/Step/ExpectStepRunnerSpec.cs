// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step
{
    [Specification("ExpectStepRunner Spec")]
    class ExpectStepRunnerSpec
    {
        [Context]
        ExpectStepRunnerSpec_StepRunning StepRunning{ get; }

        [Context]
        ExpectStepRunnerSpec_Constrains Constrains { get; }

        [Context]
        ExpectStepRunnerSpec_StepRunningAsync StepRunningAsync { get; }
    }
}
