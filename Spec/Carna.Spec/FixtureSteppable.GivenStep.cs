// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;

using NSubstitute;

using Carna.Step;

namespace Carna
{
    [Context("Given step")]
    class FixtureSteppable_GivenStep : FixtureSteppable
    {
        private IFixtureStepper FixtureStepper { get; set; }
        private FixtureSteppableTss Fixture { get; }

        private static string Description { get; } = "description";

        public FixtureSteppable_GivenStep()
        {
            FixtureStepper = Substitute.For<IFixtureStepper>();
            Fixture = new FixtureSteppableTss(FixtureStepper);
        }

        [Example("When a description is specified")]
        void Ex01()
        {
            Fixture.RunGiven(Description);

            Expect(
                "the underlying stepper should take a Given step that has the sepcified description.",
                () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                    step.Description == Description &&
                    step.Arrangement == null && step.AsyncArrangement == null
                ))
            );
        }

        [Example("When a description and an arrangement are specified")]
        void Ex02()
        {
            Action arrangement = () => { };

            Fixture.RunGiven(Description, arrangement);

            Expect(
                "the underlying stepper should take a Given step that has the specified description and arrangement.",
                () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                    step.Description == Description &&
                    step.Arrangement == arrangement && step.AsyncArrangement == null
                ))
            );
        }

        [Example("When a description and an asynchronous arrangement are specified")]
        void Ex03()
        {
            Func<Task> asyncArrangement = async () => { await Task.Delay(100); };

            Fixture.RunGiven(Description, asyncArrangement);

            Expect(
                "the underlying stepper should take a Given step that has the specified description and asynchronous arrangement.",
                () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                    step.Description == Description &&
                    step.Arrangement == null && step.AsyncArrangement == asyncArrangement
                ))
            );
        }
    }
}
