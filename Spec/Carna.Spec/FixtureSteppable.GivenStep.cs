// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;
using NSubstitute;

namespace Carna;

[Context("Given step")]
class FixtureSteppable_GivenStep : FixtureSteppable
{
    IFixtureStepper FixtureStepper { get; }
    FixtureSteppableTss Fixture { get; }

    static string Description => "description";

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
            "the underlying stepper should take a Given step that has the specified description.",
            () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                StepAssertions.GivenStep.Of(step) == StepAssertions.ExpectStep.Of(Description)
            ))
        );
    }

    [Example("When a description and an arrangement are specified")]
    void Ex02()
    {
        var arrangement = () => { };

        Fixture.RunGiven(Description, arrangement);

        Expect(
            "the underlying stepper should take a Given step that has the specified description and arrangement.",
            () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                StepAssertions.GivenStep.Of(step) == StepAssertions.GivenStep.Of(Description, arrangement)
            ))
        );
    }

    [Example("When a description and an asynchronous arrangement are specified")]
    void Ex03()
    {
        var asyncArrangement = async () => { await Task.Delay(100); };

        Fixture.RunGiven(Description, asyncArrangement);

        Expect(
            "the underlying stepper should take a Given step that has the specified description and asynchronous arrangement.",
            () => FixtureStepper.Received().Take(Arg.Is<GivenStep>(step =>
                StepAssertions.GivenStep.Of(step) == StepAssertions.GivenStep.Of(Description, asyncArrangement)
            ))
        );
    }
}