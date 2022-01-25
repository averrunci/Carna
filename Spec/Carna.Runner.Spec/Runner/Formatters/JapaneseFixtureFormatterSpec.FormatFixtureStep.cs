// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner.Step;

namespace Carna.Runner.Formatters;

[Context("Format a fixture step")]
class JapaneseFixtureFormatterSpec_FormatFixtureStep : FixtureSteppable
{
    IFixtureFormatter Formatter { get; }
    string Description { get; }

    private FormattedDescription FormattedDescription { get; set; } = default!;

    public JapaneseFixtureFormatterSpec_FormatFixtureStep()
    {
        Formatter = new JapaneseFixtureFormatter();
        Description = "詳細";
    }

    [Example("When GivenStep is specified")]
    void Ex01()
    {
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateGivenStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be '前提条件として、{the description of GivenStep}'", () => FormattedDescription.Lines.ElementAt(0) == $"前提条件として、{Description}");
        Expect("the line indent of the formatted description should be '　　　　　　　　'(8 spaces)", () => FormattedDescription.LineIndent == "　　　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When WhenStep is specified")]
    void Ex02()
    {
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateWhenStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of WhenStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When ThenStep is specified")]
    void Ex03()
    {
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateThenStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be '結果として、{the description of ThenStep}'", () => FormattedDescription.Lines.ElementAt(0) == $"結果として、{Description}");
        Expect("the line indent of the formatted description should be '　　　　　　'(6 spaces)", () => FormattedDescription.LineIndent == "　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When ExpectStep is specified")]
    void Ex04()
    {
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateExpectStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be '期待値として、{the description of ExpectStep}'", () => FormattedDescription.Lines.ElementAt(0) == $"期待値として、{Description}");
        Expect("the line indent of the formatted description should be '　　　　　　　'(7 spaces)", () => FormattedDescription.LineIndent == "　　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When NoteStep is specified")]
    void Ex05()
    {
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateNoteStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be '＃{the description of NoteStep}'", () => FormattedDescription.Lines.ElementAt(0) == $"＃{Description}");
        Expect("the line indent of the formatted description should be '　'(1 spaces)", () => FormattedDescription.LineIndent == "　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When GivenStep is specified after GivenStep is specified")]
    void Ex06()
    {
        Formatter.FormatFixtureStep(FixtureSteps.CreateGivenStep(Description));
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateGivenStep(Description));

        Expect("the first line indent of the formatted description should be '　　　　　　　　'(8 spaces)", () => FormattedDescription.FirstLineIndent == "　　　　　　　　");
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of GivenStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be '　　　　　　　　'(8spaces)", () => FormattedDescription.LineIndent == "　　　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When WhenStep is specified after WhenStep is specified")]
    void Ex07()
    {
        Formatter.FormatFixtureStep(FixtureSteps.CreateWhenStep(Description));
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateWhenStep(Description));

        Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of WhenStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When ThenStep is specified after ThenStep is specified")]
    void Ex08()
    {
        Formatter.FormatFixtureStep(FixtureSteps.CreateThenStep(Description));
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateThenStep(Description));

        Expect("the first line indent of the formatted description should be '　　　　　　'(6 spaces)", () => FormattedDescription.FirstLineIndent == "　　　　　　");
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of ThenStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be '　　　　　　'(6 spaces)", () => FormattedDescription.LineIndent == "　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When ExpectStep is specified after ExpectStep is specified")]
    void Ex09()
    {
        Formatter.FormatFixtureStep(FixtureSteps.CreateExpectStep(Description));
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateExpectStep(Description));

        Expect("the first line indent of the formatted description should be '　　　　　　　'(7 spaces)", () => FormattedDescription.FirstLineIndent == "　　　　　　　");
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of ExpectStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be '　　　　　　　'(7 spaces)", () => FormattedDescription.LineIndent == "　　　　　　　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When NoteStep is specified after NoteStep is specified")]
    void Ex10()
    {
        Formatter.FormatFixtureStep(FixtureSteps.CreateNoteStep(Description));
        FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateNoteStep(Description));

        Expect("the first line indent of the formatted description should be '　'(1 space)", () => FormattedDescription.FirstLineIndent == "　");
        Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
        Expect("the first element of the formatted description line should be the description of NoteStep", () => FormattedDescription.Lines.ElementAt(0) == Description);
        Expect("the line indent of the formatted description should be '　'(1 space)", () => FormattedDescription.LineIndent == "　");
        Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
    }

    [Example("When the description of the specified step is multi-line")]
    void Ex11()
    {
        var description = string.Empty;
        Given("the description that has three lines", () => description = @"詳細　行1
詳細　行2
詳細　行3");
        When("Step is formatted", () => FormattedDescription = Formatter.FormatFixtureStep(FixtureSteps.CreateNoteStep(description)));
        Then("the line count of the formatted description should be 3", () => FormattedDescription.Lines.Count() == 3);
        Then("the first element of the formatted description line should be the first line of the given description", () => FormattedDescription.Lines.ElementAt(0) == "＃詳細　行1");
        Then("the second element of the formatted description line should be the second line of the given description", () => FormattedDescription.Lines.ElementAt(1) == "詳細　行2");
        Then("the third element of the formatted description line should be the third line of the given description", () => FormattedDescription.Lines.ElementAt(2) == "詳細　行3");
    }
}