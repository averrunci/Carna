// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using NSubstitute;

using Carna.Step;

namespace Carna
{
    [Context("When step")]
    class FixtureSteppable_ThenStep : FixtureSteppable
    {
        IFixtureStepper FixtureStepper { get; set; }
        FixtureSteppableTss Fixture { get; }

        static string Description { get; } = "description";

        public FixtureSteppable_ThenStep()
        {
            FixtureStepper = Substitute.For<IFixtureStepper>();
            Fixture = new FixtureSteppableTss(FixtureStepper);
        }

        [Example("When a description is specified")]
        void Ex01()
        {
            Fixture.RunThen(Description);

            Expect(
                "the underlying stepper should take a Then step that has the specified description.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description)
                ))
            );
        }

        [Example("When a description and an assertion the type of which is Action are specified")]
        void Ex02()
        {
            Action assertion = () => { };

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and an exception assertion the type of which is Action are specified")]
        void Ex03()
        {
            Action<Exception> assertion = exc => { };

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and an assertion that returns a boolean value are specified")]
        void Ex04()
        {
            Expression<Func<bool>> assertion = () => true;

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and an exception assertion that returns a boolean value are specified")]
        void Ex05()
        {
            Expression<Func<Exception, bool>> assertion = exc => true;

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and an assertion the type of which is Func<Task> are specified")]
        void Ex06()
        {
            Func<Task> assertion = async () => { await Task.Delay(100); };

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and an exception assertion the type of which is Func<Exception, Task> are specified")]
        void Ex07()
        {
            Func<Exception, Task> assertion = async exc => { await Task.Delay(100); };

            Fixture.RunThen(Description, assertion);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, assertion)
                ))
            );
        }

        [Example("When a description and the type of the exception is specified")]
        void Ex08()
        {
            Fixture.RunThen<ArgumentNullException>(Description);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and type of the exception.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStep.Of(step) == StepAssertions.ThenStep.Of(Description, typeof(ArgumentNullException))
                ))
            );
        }

        [Example("When a description, the type of the exception, and an exception assertion the type of which is Action are specified")]
        void Ex09()
        {
            Fixture.RunThen<ArgumentNullException>(Description, exc => { });

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStepExceptionAction.Of(step) == StepAssertions.ThenStepExceptionAction.Of(Description, typeof(ArgumentNullException))
                ))
            );
        }

        [Example("When a description, the type of the exception and an exception assertion that returns a boolean value are specified")]
        void Ex10()
        {
            Fixture.RunThen<ArgumentNullException>(Description, exc => true);

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStepExceptionAssertion.Of(step) == StepAssertions.ThenStepExceptionAssertion.Of(Description, typeof(ArgumentNullException))
                ))
            );
        }

        [Example("When a description, the type of the exception and an exception assertion the type of which is Func<Exception, Task> are specified")]
        void Ex11()
        {
            Fixture.RunThen<ArgumentNullException>(Description, async exc => { await Task.Delay(100); });

            Expect(
                "the underlying stepper should take a Then step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ThenStep>(step =>
                    StepAssertions.ThenStepAsyncExceptionAction.Of(step) == StepAssertions.ThenStepAsyncExceptionAction.Of(Description, typeof(ArgumentNullException))
                ))
            );
        }
    }
}
