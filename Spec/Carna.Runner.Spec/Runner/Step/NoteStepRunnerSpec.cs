// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step
{
    [Specification("NoteStepRunner Spec")]
    class NoteStepRunnerSpec : FixtureSteppable
    {
        FixtureStepResultCollection StepResults { get; }

        NoteStep Step { get; set; }
        FixtureStepResult Result { get; set; }

        public NoteStepRunnerSpec()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(NoteStep step) => new NoteStepRunner(step);

        [Example("When NoteStep is run")]
        void Ex01()
        {
            Given("NoteStep", () => Step = FixtureSteps.CreateNoteStep());
            When("the given NoteStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then("the status of the result should be None", () => Result.Status == FixtureStepStatus.None);
            Then("the exception of the result should be null", () => Result.Exception == null);
            Then("the step of the result should be the given NoteStep", () => Result.Step == Step);
        }
    }
}
