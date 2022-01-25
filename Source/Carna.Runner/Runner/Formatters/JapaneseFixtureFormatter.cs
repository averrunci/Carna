// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Formatters;

/// <summary>
/// Provides the function to format a description of a fixture and fixture step
/// using Japanese language.
/// </summary>
public class JapaneseFixtureFormatter : FixtureFormatter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JapaneseFixtureFormatter"/> class.
    /// </summary>
    public JapaneseFixtureFormatter()
    {
        FixtureNames[typeof(AssemblyFixtureAttribute)] = "アセンブリ";
        FixtureNames[typeof(NamespaceFixtureAttribute)] = string.Empty;
        FixtureNames[typeof(FeatureAttribute)] = "機能";
        FixtureNames[typeof(StoryAttribute)] = "ストーリー";
        FixtureNames[typeof(ScenarioAttribute)] = "シナリオ";
        FixtureNames[typeof(SpecificationAttribute)] = string.Empty;
        FixtureNames[typeof(RequirementAttribute)] = "要件";
        FixtureNames[typeof(ContextAttribute)] = string.Empty;
        FixtureNames[typeof(ExampleAttribute)] = string.Empty;

        StepNames[typeof(GivenStep)] = "前提条件として、";
        StepNames[typeof(WhenStep)] = string.Empty;
        StepNames[typeof(ThenStep)] = "結果として、";
        StepNames[typeof(ExpectStep)] = "期待値として、";
        StepNames[typeof(NoteStep)] = "＃";

        NarrativeItemNames[NarrativeItem.Benefit] = "目的";
        NarrativeItemNames[NarrativeItem.Role] = "役割";
        NarrativeItemNames[NarrativeItem.Feature] = "機能";
    }

    /// <summary>
    /// Gets a name for the background.
    /// </summary>
    protected override string BackgroundName => "前提条件として、";

    /// <summary>
    /// Gets a formatted string expression of the specified fixture name and description.
    /// </summary>
    /// <param name="fixtureName">The name of a fixture.</param>
    /// <param name="description">The description of a fixture.</param>
    /// <returns>
    /// The formatted string expression of the specified fixture name and description.
    /// </returns>
    protected override string FormatFixture(string fixtureName, string description) => $"{fixtureName}：{description}";

    /// <summary>
    /// Gets a first line indent of the specified fixture name.
    /// </summary>
    /// <param name="fixtureName">The name of a fixture.</param>
    /// <returns>The first line indent of the specified fixture name.</returns>
    protected override string FixtureFirstLineIndent(string fixtureName) => string.Empty;

    /// <summary>
    /// Gets a line indent of the specified fixture name.
    /// </summary>
    /// <param name="fixtureName">The name of a fixture.</param>
    /// <returns>The line indent of the specified fixture name.</returns>
    protected override string FixtureLineIndent(string fixtureName)
        => string.IsNullOrEmpty(fixtureName) ? string.Empty : new string('　', fixtureName.Length + 1);

    /// <summary>
    /// Gets a formatted string expression of the specified narrative item name and description.
    /// </summary>
    /// <param name="narrativeItemName">The narrative item.</param>
    /// <param name="description">The description of a narrative item.</param>
    /// <returns>
    /// The formatted string expression of the specified narrative item name and description.
    /// </returns>
    protected override string FormatNarrative(string narrativeItemName, string description) => $"{narrativeItemName}：{description}";

    /// <summary>
    /// Gets a first line indent of the specified narrative item name.
    /// </summary>
    /// <param name="narrativeItemName">The name of a narrative item.</param>
    /// <returns>The first line indent of the specified narrative item name.</returns>
    protected override string NarrativeFirstLineIndent(string narrativeItemName) => string.Empty;

    /// <summary>
    /// Gets a line indent of the specified narrative item name.
    /// </summary>
    /// <param name="narrativeItemName">The name of a narrative item.</param>
    /// <returns>The line indent of the specified narrative item name.</returns>
    protected override string NarrativeLineIndent(string narrativeItemName) => new('　', narrativeItemName.Length + 1);

    /// <summary>
    /// Gets a formatted string expression of the specified background description.
    /// </summary>
    /// <param name="backgroundName">The name of the background.</param>
    /// <param name="description">The description of the background.</param>
    /// <returns>The formatted string expression of the specified background description.</returns>
    protected override string FormatBackground(string backgroundName, string description) => $"{backgroundName}{description}";

    /// <summary>
    /// Gets a line indent of the background.
    /// </summary>
    /// <param name="backgroundName">The name of the background.</param>
    /// <returns>The line indent of the background.</returns>
    protected override string BackgroundLineIndent(string backgroundName) => new('　', backgroundName.Length);

    /// <summary>
    /// Gets a formatted string expression of the specified step name, description, and
    /// a value that indicates whether a conjunction is required.
    /// </summary>
    /// <param name="stepName">The name of a fixture step.</param>
    /// <param name="description">The description of a fixture step.</param>
    /// <param name="conjunctionRequired">
    /// <c>true</c> if a conjunction is required; otherwise, <c>false</c>.
    /// </param>
    /// <returns>
    /// The formatted string expression of the specified step name, description, and
    /// a value that indicates whether a conjunction is required.
    /// </returns>
    protected override string FormatFixtureStep(string stepName, string description, bool conjunctionRequired)
        => conjunctionRequired ? description : $"{stepName}{description}";

    /// <summary>
    /// Gets a first line indent of the specified step name and a value that indicates
    /// whether a conjunction is required.
    /// </summary>
    /// <param name="stepName">The name of a fixture step.</param>
    /// <param name="conjunctionRequired">
    /// <c>true</c> if a conjunction is required; otherwise, <c>false</c>.
    /// </param>
    /// <returns>
    /// The first line indent of the specified step name and a value that indicates
    /// whether a conjunction is required.
    /// </returns>
    protected override string FixtureStepFirstLineIndent(string stepName, bool conjunctionRequired)
        => conjunctionRequired ? new string('　', stepName.Length) : string.Empty;

    /// <summary>
    /// Gets a line indent of the specified step name and a value that indicates
    /// whether a conjunction is required.
    /// </summary>
    /// <param name="stepName">The name of a fixture step.</param>
    /// <param name="conjunctionRequired">
    /// <c>true</c> if a conjunction is required; otherwise, <c>false</c>.
    /// </param>
    /// <returns>
    /// The line indent of the specified step name and a value that indicates
    /// whether a conjunction is required.
    /// </returns>
    protected override string FixtureStepLineIndent(string stepName, bool conjunctionRequired) => new('　', stepName.Length);
}