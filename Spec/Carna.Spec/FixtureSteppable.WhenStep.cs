﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;
using NSubstitute;

namespace Carna;

[Context("When step")]
class FixtureSteppable_WhenStep : FixtureSteppable
{
    IFixtureStepper FixtureStepper { get; }
    FixtureSteppableTss Fixture { get; }

    static string Description => "description";

    public FixtureSteppable_WhenStep()
    {
        FixtureStepper = Substitute.For<IFixtureStepper>();
        Fixture = new FixtureSteppableTss(FixtureStepper);
    }

    [Example("When a description is specified")]
    void Ex01()
    {
        Fixture.RunWhen(Description);

        Expect(
            "the underlying stepper should take a When step that has the specified description.",
            () => FixtureStepper.Received().Take(Arg.Is<WhenStep>(step =>
                StepAssertions.WhenStep.Of(step) == StepAssertions.WhenStep.Of(Description)
            ))
        );
    }

    [Example("When a description and an action are specified")]
    void Ex02()
    {
        var action = () => { };

        Fixture.RunWhen(Description, action);

        Expect(
            "the underlying stepper should take a When step that has the specified description and action.", 
            () => FixtureStepper.Received().Take(Arg.Is<WhenStep>(step =>
                StepAssertions.WhenStep.Of(step) == StepAssertions.WhenStep.Of(Description, action)
            ))
        );
    }

    [Example("When a description and an asynchronous action are specified")]
    void Ex03()
    {
        var asyncAction = async () => { await Task.Delay(100); };

        Fixture.RunWhen(Description, asyncAction);

        Expect(
            "the underlying stepper should take a When step that has the specified description and asynchronous action.",
            () => FixtureStepper.Received().Take(Arg.Is<WhenStep>(step =>
                StepAssertions.WhenStep.Of(step) == StepAssertions.WhenStep.Of(Description, asyncAction)
            ))
        );
    }
}