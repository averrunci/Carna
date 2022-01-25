// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;
using NSubstitute;

namespace Carna;

[Context("Note step")]
class FixtureSteppable_NoteStep : FixtureSteppable
{
    IFixtureStepper FixtureStepper { get; }
    FixtureSteppableTss Fixture { get; }

    static string Description => "description";

    public FixtureSteppable_NoteStep()
    {
        FixtureStepper = Substitute.For<IFixtureStepper>();
        Fixture = new FixtureSteppableTss(FixtureStepper);
    }

    [Example("When a description is specified")]
    void Ex01()
    {
        Fixture.RunNote(Description);

        Expect(
            "the underlying stepper should take a Note step that has the specified description.",
            () => FixtureStepper.Received().Take(Arg.Is<NoteStep>(step =>
                step.Description == Description
            ))
        );
    }
}