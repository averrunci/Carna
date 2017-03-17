// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;

namespace Carna.Runner.Formatters
{
    [Context("Format a fixture")]
    class JapaneseFixtureFormatterSpec_FormatFixture : FixtureSteppable
    {
        private IFixtureFormatter Formatter { get; }
        private string Name { get; }
        private string Description { get; }

        private FormattedDescription FormattedDescription { get; set; }

        public JapaneseFixtureFormatterSpec_FormatFixture()
        {
            Formatter = new JapaneseFixtureFormatter();
            Name = "名称";
            Description = "詳細";
        }

        [Example("When FixtureDescriptor of AssemblyFixtureAttribute is specified")]
        void Ex01()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new AssemblyFixtureAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'アセンブリ：{the description of AssemblyFixtureAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"アセンブリ：{Description}");
            Expect("the line indent of the formatted description should be '　　　　　　'(6 spaces)", () => FormattedDescription.LineIndent == "　　　　　　");
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
            Expect("the first element of the formatted description line should be '機能：{the description of FeatureAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"機能：{Description}");
            Expect("the line indent of the formatted description should be '　　　'(3 spaces)", () => FormattedDescription.LineIndent == "　　　");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of StoryAttribute is specified")]
        void Ex04()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new StoryAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'ストーリー：{the description of StoryAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"ストーリー：{Description}");
            Expect("the line indent of the formatted description should be '　　　　　　'(6 spaces)", () => FormattedDescription.LineIndent == "　　　　　　");
            Expect("the items of the formatted description should be empty", () => !FormattedDescription.Items.Any());
        }

        [Example("When FixtureDescriptor of ScenarioAttribute is specified")]
        void Ex05()
        {
            FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new ScenarioAttribute(Description)));

            Expect("the first line indent of the formatted description should be empty", () => FormattedDescription.FirstLineIndent == string.Empty);
            Expect("the line count of the formatted description should be 1", () => FormattedDescription.Lines.Count() == 1);
            Expect("the first element of the formatted description line should be 'シナリオ：{the description of ScenarioAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"シナリオ：{Description}");
            Expect("the line indent of the formatted description should be '　　　　　'(5 spaces)", () => FormattedDescription.LineIndent == "　　　　　");
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
            Expect("the first element of the formatted description line should be '要件：{the description of RequirementAttribute}'", () => FormattedDescription.Lines.ElementAt(0) == $"要件：{Description}");
            Expect("the line indent of the formatted description should be '　　　'(3 spaces)", () => FormattedDescription.LineIndent == "　　　");
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
            Given("the description that has three lines", () => description = @"詳細　行1
詳細　行2
詳細　行3");
            When("the description is formatted", () => FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, new AssemblyFixtureAttribute(description))));
            Then("the line count of the formatted description should be 3", () => FormattedDescription.Lines.Count() == 3);
            Then("the first element of the formatted description line should be the first line of the given description", () => FormattedDescription.Lines.ElementAt(0) == "アセンブリ：詳細　行1");
            Then("the second element of the formatted description line should be the second line of the given description", () => FormattedDescription.Lines.ElementAt(1) == "詳細　行2");
            Then("the third element of the formatted description line should be the third line of the given description", () => FormattedDescription.Lines.ElementAt(2) == "詳細　行3");
        }

        [Example("When FixtureDescriptor of FeatureAttribute that has a narrative is specified")]
        void Ex11()
        {
            FeatureAttribute featureAttribute = null;
            Given("FeatureAttribute that has role, feature, and benefit", () => featureAttribute = new FeatureAttribute(Description) { Benefit = "目的", Role = "役割", Feature = "機能" });
            When("the description is formatted", () => FormattedDescription = Formatter.FormatFixture(new FixtureDescriptor(Name, featureAttribute)));
            Then("the item count of the formatted description should be 3", () => FormattedDescription.Items.Count() == 3);

            var item = FormattedDescription.Items.ElementAt(0);
            Then("the first line indent of the first item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the first item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the first item line should be '目的：{the Role of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"目的：{featureAttribute.Benefit}");
            Then("the line indent of the first item should be '　　　'(3 spaces)", () => item.LineIndent == "　　　");
            Then("the items of the first item should be empty", () => !item.Items.Any());

            item = FormattedDescription.Items.ElementAt(1);
            Then("the first line indent of the second item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the second item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the second item line should be '役割：{the Feature of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"役割：{featureAttribute.Role}");
            Then("the line indent of the second item should be '　　　'(3 spaces)", () => item.LineIndent == "　　　");
            Then("the items of the second item should be empty", () => !item.Items.Any());

            item = FormattedDescription.Items.ElementAt(2);
            Then("the first line indent of the third item should be empty", () => item.FirstLineIndent == string.Empty);
            Then("the line count of the third item should be 1", () => item.Lines.Count() == 1);
            Then("the first element of the third item line should be '機能：{the Benefit of the given FeatureAttribute}'", () => item.Lines.ElementAt(0) == $"機能：{featureAttribute.Feature}");
            Then("the line indent of the third item should be '　　　'(3 spaces)", () => item.LineIndent == "　　　");
            Then("the items of the third item should be empty", () => !item.Items.Any());
        }
    }
}
