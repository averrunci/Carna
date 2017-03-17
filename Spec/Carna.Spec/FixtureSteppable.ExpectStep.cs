// Copyright (C) 2017 Fievus
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
    [Context("Expect step")]
    class FixtureSteppable_ExpectStep : FixtureSteppable
    {
        private IFixtureStepper FixtureStepper { get; set; }
        private FixtureSteppableTss Fixture { get; }

        private static string Description { get; } = "description";

        public FixtureSteppable_ExpectStep()
        {
            FixtureStepper = Substitute.For<IFixtureStepper>();
            Fixture = new FixtureSteppableTss(FixtureStepper);
        }

        [Example("When a description is specified")]
        void Ex01()
        {
            Fixture.RunExpect(Description);

            Expect(
                "the underlying stepper should take an Expect step that has the sepcified description.",
                () => FixtureStepper.Received().Take( Arg.Is<ExpectStep>(step =>
                    step.Description == Description &&
                    step.Action == null && step.Assertion == null
                ))
            );
        }

        [Example("When a description and an assertion the type of which is Action are specified")]
        void Ex02()
        {
            Action assertion = () => { };

            Fixture.RunExpect(Description, assertion);

            Expect(
                "the underlying stepper should take an Exepct step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                    step.Description == Description &&
                    step.Action == assertion && step.Assertion == null && step.AsyncAction == null
                ))
            );
        }

        [Example("When a description and an assertion that returns a boolean value are specified")]
        void Ex03()
        {
            Expression<Func<bool>> assertion = () => true;

            Fixture.RunExpect(Description, assertion);

            Expect(
                "the underlying stepper should take an Expect step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                    step.Description == Description &&
                    step.Action == null && step.Assertion == assertion && step.AsyncAction == null
                ))
            );
        }

        [Example("When a description and an assertion the type of which is Func<Task> are specified")]
        void Ex04()
        {
            Func<Task> asyncAction = async () => { await Task.Delay(100); };

            Fixture.RunExpect(Description, asyncAction);

            Expect(
                "the underlying stepper should take a When step that has the specified description and assertion.",
                () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                    step.Description == Description &&
                    step.Action == null && step.Assertion == null && step.AsyncAction == asyncAction
                ))
            );
        }
    }
}
