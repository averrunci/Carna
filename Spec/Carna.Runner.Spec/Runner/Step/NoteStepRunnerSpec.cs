// Copyright (C) 2017-2019 Fievus
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
        FixtureStepResultAssertion ExpectedResult { get; set; }

        public NoteStepRunnerSpec()
        {
            StepResults = new FixtureStepResultCollection();
        }

        private IFixtureStepRunner RunnerOf(NoteStep step) => new NoteStepRunner(step);

        [Example("When NoteStep is run")]
        void Ex01()
        {
            Given("NoteStep", () =>
            {
                Step = FixtureSteps.CreateNoteStep();
                ExpectedResult = FixtureStepResultAssertion.ForNullException(FixtureStepStatus.None, Step);
            });
            When("the given NoteStep is run", () => Result = RunnerOf(Step).Run(StepResults).Build());
            Then($"the result should be as follows:{ExpectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(Result) == ExpectedResult);
        }
    }
}
