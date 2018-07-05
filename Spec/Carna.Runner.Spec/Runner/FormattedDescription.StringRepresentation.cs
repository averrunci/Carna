// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Context("String representation")]
    class FormattedDescription_StringRepresentation : FixtureSteppable
    {
        FormattedDescription Description { get; } = new FormattedDescription();

        [Example("When description does not contain any lines")]
        void Ex01()
        {
            Expect("the string representation should be empty", () => Description.ToString() == string.Empty);
        }

        [Example("When description contains some lines")]
        void Ex02()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            Given("a line indent", () => Description.LineIndent = "    ");
            Expect(
                "the string representation should be the one created by the given lines",
                () => Description.ToString() == @"  First line
    Second line
    Third line"
            );
        }

        [Example("When description contains some lines and some items")]
        void Ex07()
        {
            Given("some lines", () => Description.Lines = new[] { "First line", "Second line", "Third line" });
            Given("a first line indent", () => Description.FirstLineIndent = "  ");
            Given("a line indent", () => Description.LineIndent = "    ");
            Given(
                "some items",
                () => Description.Items = new[] {
                    new FormattedDescription
                    {
                        Lines = new[] { "First line in first item" },
                        FirstLineIndent = "  ",
                        LineIndent = "    ",
                    },
                    new FormattedDescription
                    {
                        Lines = new[]
                        {
                            "First line in second item",
                            "Second line in second item"
                        },
                        FirstLineIndent = "  ",
                        LineIndent = "    ",
                    },
                    new FormattedDescription
                    {
                        Lines = new[]
                        {
                            "First line in third item",
                            "Second line in third item",
                            "Third line in third item"
                        },
                        FirstLineIndent = "  ",
                        LineIndent = "    ",
                    }
                }
            );
            Expect(
                "the string representation should be the one created by the given lines and items",
                () => Description.ToString() == @"  First line
    Second line
    Third line
    First line in first item
    First line in second item
      Second line in second item
    First line in third item
      Second line in third item
      Third line in third item"
            );
        }
    }
}
