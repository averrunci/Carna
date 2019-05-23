// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Carna.Assertions;
using Carna.Step;

namespace Carna.Runner.Step
{
    [Specification("FixtureStepper Spec")]
    class FixtureStepperSpec : FixtureSteppable
    {
        FixtureStepper Stepper { get; }

        public FixtureStepperSpec()
        {
            Stepper = new FixtureStepper(new SimpleFixtureStepRunnerFactory());
        }

        [Example("Takes a step")]
        void Ex01()
        {
            SimpleStep step = null;
            Given("SimpleStep", () => step = new SimpleStep());
            When("Stepper takes the given step", () => Stepper.Take(step));
            Then("the count of the result should be 1", () => Stepper.Results.Count() == 1);
            Then("the step of the result should be the given step", () => Stepper.Results.ElementAt(0).Step == step);
        }

        [Example("Takes some steps")]
        void Ex02()
        {
            SimpleStep[] steps = null;
            Given("three SimpleSteps", () => steps = new[] { new SimpleStep(), new SimpleStep(), new SimpleStep() });
            When("Stepper takes the given three steps", () => { foreach (var step in steps) { Stepper.Take(step); } });
            Then("the count of the result should be 3", () => Stepper.Results.Count() == 3);
            Then("the step of the first result should be the first given step", () => Stepper.Results.ElementAt(0).Step == steps[0]);
            Then("the step of the second result should be the second given step", () => Stepper.Results.ElementAt(1).Step == steps[1]);
            Then("the step of the third result should be the third given step", () => Stepper.Results.ElementAt(2).Step == steps[2]);
        }

        [Example("Event is raised before/after a step is run")]
        void Ex03()
        {
            FixtureStepResult runningResult = null;
            FixtureStepResult runResult = null;
            Stepper.FixtureStepRunning += (s, e) => runningResult = e.Result;
            Stepper.FixtureStepRun += (s, e) => runResult = e.Result;

            SimpleStep step = null;
            FixtureStepResultAssertion expectedResult = null;
            Given("SimpleStep", () =>
            {
                step = new SimpleStep();
                expectedResult = FixtureStepResultAssertion.ForRunning(step);
            });
            When("Stepper takes the given step", () => Stepper.Take(step));
            Then("FixtureStepRunning event should be raised", () => runningResult != null);
            Then($"the result should be as follows:{expectedResult.ToDescription()}", () => FixtureStepResultAssertion.Of(runningResult) == expectedResult);

            Then("FixtureStepRun event should be raised", () => runResult != null);
            Then("the result on FixtureStepRun event should be the result Stepper has", () => runResult == Stepper.Results.ElementAt(0));
        }

        private class FixtureStepResultAssertion : AssertionObject
        {
            [AssertionProperty]
            FixtureStep Step { get; }
            [AssertionProperty("StartTime has value")]
            bool StartTimeHasValue { get; }
            [AssertionProperty("EndTime has value")]
            bool EndTimeHasValue { get; }
            [AssertionProperty("Duration has value")]
            bool DurationHasValue { get; }
            [AssertionProperty]
            Exception Exception { get; }
            [AssertionProperty]
            FixtureStepStatus Status { get; }

            private FixtureStepResultAssertion(FixtureStep step, bool startTimeHasValue, bool endTimeHasValue, bool durationHasValue, Exception exception, FixtureStepStatus status)
            {
                Step = step;
                StartTimeHasValue = startTimeHasValue;
                EndTimeHasValue = endTimeHasValue;
                DurationHasValue = durationHasValue;
                Exception = exception;
                Status = status;
            }

            public static FixtureStepResultAssertion ForRunning(FixtureStep step) => new FixtureStepResultAssertion(step, true, false, false, null, FixtureStepStatus.Running);
            public static FixtureStepResultAssertion Of(FixtureStepResult result) => new FixtureStepResultAssertion(result.Step, result.StartTime.HasValue, result.EndTime.HasValue, result.Duration.HasValue, result.Exception, result.Status);
        }

        private class SimpleStep : FixtureStep
        {
            public SimpleStep() : base(string.Empty, null, string.Empty, string.Empty, 0)
            {
            }
        }

        private class SimpleStepRunner : FixtureStepRunner<SimpleStep>
        {
            public SimpleStepRunner(SimpleStep step) : base(step)
            {
            }

            protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results) => FixtureStepResult.Of(Step).Passed();
        }

        private class SimpleFixtureStepRunnerFactory : IFixtureStepRunnerFactory
        {
            public IFixtureStepRunner Create(FixtureStep step) => new SimpleStepRunner(step as SimpleStep);
            public void RegisterFrom(IEnumerable<Assembly> assemblies) {}
        }
    }
}
