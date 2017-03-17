// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;

namespace Carna.Runner.Formatters
{
    [Context("Format a fixture")]
    class FixtureFormatterSpec_FormatFixture : FixtureSteppable
    {
        private IFixtureFormatter Formatter { get; }
        private string Name { get; }
        private string Description { get; }

        private FormattedDescription FormattedDescription { get; set; }

        public FixtureFormatterSpec_FormatFixture()
        {
            Formatter = new FixtureFormatter();
            Name = "Test";
            Description = "Description";
        }

        [Example("When FixtureDescriptor of AssemblyFixtureAttribute is specified")]
        void Ex01()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new AssemblyFixtureAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'Assembly: {the description of AssemblyFixtureAttribute}", () => FormattedDescription.Lines.ElementAt(0) == $"Assembly: {Description}");
            Expect("the line indent of the formatted description should be '          '(10 spaces)", () => FormattedDescription.LineIndent == "          ");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of NamespaceFixtureAttribute is specified")]
        void Ex02()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new NamespaceFixtureAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be the description of NamespaceFixtureAttribute", () => FormattedDescription.Lines.ElementAt(0) == Description);
            Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of FeatureAttribute is specified")]
        void Ex03()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new FeatureAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'Feature: {the description of FeatureAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"Feature: {Description}");
            Expect("the line indent of the formatted description should be '         '(9 spaces)", () => FormattedDescription.LineIndent == "         ");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of StoryAttribute is specified")]
        void Ex04()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new StoryAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'Story: {the description of StoryAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"Story: {Description}");
            Expect("the line indent of the formatted description should be '       '(7 spaces)", () => FormattedDescription.LineIndent == "       ");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of ScenarioAttribute is specified")]
        void Ex05()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new ScenarioAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'Scenario: {the description of ScenarioAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"Scenario: {Description}");
            Expect("the line indent of the formatted description should be '          '(10 spaces)", () => FormattedDescription.LineIndent == "          ");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of SpecificationAttribute is specified")]
        void Ex06()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new SpecificationAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be the description of SpecificationAttribute", () => FormattedDescription.Lines.ElementAt(0) == Description);
            Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of RequirementAttribute is specified")]
        void Ex07()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new RequirementAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'Requirement: {the description of RequirementAttribute}", () => FormattedDescription.Lines.ElementAt(0) == $"Requirement: {Description}");
            Expect("the line indent of the formatted description should be '             '(13 spaces)", () => FormattedDescription.LineIndent == "             ");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of ContextAttribute is specified")]
        void Ex08()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new ContextAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be the description of ContextAttribute", () => FormattedDescription.Lines.ElementAt(0) == Description);
            Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of ExampleAttribute is specified")]
        void Ex09()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new ExampleAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be the description of ExampleAttribute", () => FormattedDescription.Lines.ElementAt(0) == Description);
            Expect("the line indent of the formatted description should be empty", () => FormattedDescription.LineIndent == string.Empty);
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When the description of the specified FixtureDescriptor is multiline")]
        void Ex10()
        {
            var description = string.Empty;
            Given("the description that has three lines", () => description = @"Description line 1
Description line 2
Description line 3");
            When("the description is formatted", () => FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new AssemblyFixtureAttribute(description))));
            Then("the line count of the formatted description should be 3", () => FormattedDescription.Lines.Count() == 3);
            Then("the first element of the formatted description line should be the first line of the given description", () => FormattedDescription.Lines.ElementAt(0) == "Assembly: Description line 1");
            Then("the second element of the formatted description line should be the second line of the given description", () => FormattedDescription.Lines.ElementAt(1) == "Description line 2");
            Then("the third element of the formatted description line should be the third line of the given description", () => FormattedDescription.Lines.ElementAt(2) == "Description line 3");
        }

        [Example("When FixtureDescriptor of FeatureAttribute that has a narrative is specified")]
        void Ex11()
        {
            FeatureAttribute featureAttribute = null;
            Given("FeatureAttribute that has benefit, role, and feature", () => featureAttribute = new FeatureAttribute(Description) { Benefit = "Benefit", Role = "Role", Feature = "Feature" });
            When("the description is formatted", () => FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, featureAttribute)));
            Then("the item count of the formatted description should be 3", () => FormattedDescription.Items.Count() == 3);

            var item = FormattedDescription.Items.ElementAt(0);
            Then("the first line indent of the first item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the first item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the first item line should be 'In order {the Benefit of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"In order {featureAttribute.Benefit}");
            Then("the line indent of the first item should be empty", () => item.LineIndent == string.Empty);
            Then("the items of the first item should be empty", () => !item.Items.Any());

            item = FormattedDescription.Items.ElementAt(1);
            Then("the first line indent of the second item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the second item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the second item line should be 'As {the Role of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"As {featureAttribute.Role}");
            Then("the line indent of the second item should be empty", () => item.LineIndent == string.Empty);
            Then("the items of the second item should be empty", () => !item.Items.Any());

            item = FormattedDescription.Items.ElementAt(2);
            Then("the first line indent of the third item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the third item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the third item line should be 'I want {the Feature of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"I want {featureAttribute.Feature}");
            Then("the line indent of the third item should be empty", () => item.LineIndent == string.Empty);
            Then("the items of the third item should be empty", () => !item.Items.Any());
        }
    }
}
