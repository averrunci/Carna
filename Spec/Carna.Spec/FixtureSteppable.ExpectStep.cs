﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;
using Carna.Step;
using NSubstitute;

namespace Carna;

[Context("Expect step")]
class FixtureSteppable_ExpectStep : FixtureSteppable
{
    IFixtureStepper FixtureStepper { get; }
    FixtureSteppableTss Fixture { get; }

    static string Description => "description";

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
            "the underlying stepper should take an Expect step that has the specified description.",
            () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                StepAssertions.ExpectStep.Of(step) == StepAssertions.ExpectStep.Of(Description)
            ))
        );
    }

    [Example("When a description and an assertion the type of which is Action are specified")]
    void Ex02()
    {
        var assertion = () => { };

        Fixture.RunExpect(Description, assertion);

        Expect(
            "the underlying stepper should take an Expect step that has the specified description and assertion.",
            () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                StepAssertions.ExpectStep.Of(step) == StepAssertions.ExpectStep.Of(Description, assertion)
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
                StepAssertions.ExpectStep.Of(step) == StepAssertions.ExpectStep.Of(Description, assertion)
            ))
        );
    }

    [Example("When a description and an assertion the type of which is Func<Task> are specified")]
    void Ex04()
    {
        var asyncAction = async () => { await Task.Delay(100); };

        Fixture.RunExpect(Description, asyncAction);

        Expect(
            "the underlying stepper should take a When step that has the specified description and assertion.",
            () => FixtureStepper.Received().Take(Arg.Is<ExpectStep>(step =>
                StepAssertions.ExpectStep.Of(step) == StepAssertions.ExpectStep.Of(Description, asyncAction)
            ))
        );
    }
}