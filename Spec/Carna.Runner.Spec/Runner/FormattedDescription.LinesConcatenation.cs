// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Context("Lines concatenation")]
    class FormattedDescription_LinesConcatenation : FixtureSteppable
    {
        private FormattedDescription Description { get; } = new FormattedDescription();
        private string JoinedDescription { get; set; }

        [Example("When description does not contain any lines")]
        void Ex01()
        {
            Given("no line", () => Description.Lines = new string[0]);
            When("the lines are joined", () => JoinedDescription = Description.JoinLines());
            Then("the joined description should be empty", () => JoinedDescription == string.Empty);
        }

        [Example("When description contains one line")]
        void Ex02()
        {
            Given("a line", () => Description.Lines = new[] { "First line" });
            When("the lines are joined", () => JoinedDescription = Description.JoinLines());
            Then(
                "the joined description should be the given line",
                () => JoinedDescription == "First line"
            );
        }

        [Example("When description contains one line and a first line indent")]
        void Ex03()
        {
            Given("a line", () => Description.Lines = new[] { "First line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            When("the lines are joined", () => JoinedDescription = Description.JoinLines());
            Then(
                "the joined description should be the given line with the given first line indent",
                () => JoinedDescription == "  First line"
            );
        }

        [Example("When description contains one line with specifying an indent")]
        void Ex04()
        {
            Given("a line", () => Description.Lines = new[] { "First line" });
            When("the lines are joined with an indent", () => JoinedDescription = Description.JoinLines("  "));
            Then(
                "the joined description should be the given line",
                () => JoinedDescription == "  First line"
            );
        }

        [Example("When description contains one line and a first line indent with specifying an indent")]
        void Ex05()
        {
            Given("a line", () => Description.Lines = new[] { "First line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            When("the lines are joined with an indent", () => JoinedDescription = Description.JoinLines("  "));
            Then(
                "the joined description should be the given line",
                () => JoinedDescription == "    First line"
            );
        }

        [Example("When description contains some lines")]
        void Ex06()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            When("the lines are joined", () => JoinedDescription = Description.JoinLines());
            Then(
                "the joined description should be the given lines",
                () => JoinedDescription == @"First line
Second line
Third line"
            );
        }

        [Example("When description contains somelines and a line indent")]
        void Ex07()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            Given("a line indent", () => Description.LineIndent = "    ");
            When("the lines are joined", () => JoinedDescription = Description.JoinLines());
            Then(
                "the string representation should be the given lines with the given line indent",
                () => JoinedDescription == @"  First line
    Second line
    Third line"
            );
        }

        [Example("When description contains some lines with specifying an indent")]
        void Ex08()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            When("the lines are joined", () => JoinedDescription = Description.JoinLines("  "));
            Then(
                "the joined description should be the given lines",
                () => JoinedDescription == @"  First line
  Second line
  Third line"
            );
        }

        [Example("When description contains somelines and a line indent with specifying an indent")]
        void Ex09()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            Given("a line indent", () => Description.LineIndent = "    ");
            When("the lines are joined", () => JoinedDescription = Description.JoinLines("  "));
            Then(
                "the string representation should be the given lines with the given line indent",
                () => JoinedDescription == @"    First line
      Second line
      Third line"
            );
        }
    }
}
