// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;

using Carna.Step;

namespace Carna.Runner.Formatters
{
    /// <summary>
    /// Provides the function to format a description of a fixture and fixture step.
    /// </summary>
    public class FixtureFormatter : IFixtureFormatter
    {
        /// <summary>
        /// Gets fixture names for the fixture attribute type.
        /// </summary>
        protected IDictionary<Type, string> FixtureNames { get; } = new Dictionary<Type, string>
        {
            [typeof(AssemblyFixtureAttribute)] = "Assembly",
            [typeof(NamespaceFixtureAttribute)] = string.Empty,
            [typeof(FeatureAttribute)] = "Feature",
            [typeof(StoryAttribute)] = "Story",
            [typeof(ScenarioAttribute)] = "Scenario",
            [typeof(SpecificationAttribute)] = string.Empty,
            [typeof(RequirementAttribute)] = "Requirement",
            [typeof(ContextAttribute)] = string.Empty,
            [typeof(ExampleAttribute)] = string.Empty
        };

        /// <summary>
        /// Gets step names for the fixture step type.
        /// </summary>
        protected IDictionary<Type, string> StepNames { get; } = new Dictionary<Type, string>
        {
            [typeof(GivenStep)] = "Given",
            [typeof(WhenStep)] = "When",
            [typeof(ThenStep)] = "Then",
            [typeof(ExpectStep)] = "Expect",
            [typeof(NoteStep)] = "Note"
        };

        /// <summary>
        /// Gets a conjunction expression.
        /// </summary>
        protected virtual string Conjunction { get; } = "And";

        /// <summary>
        /// Represents a narrative item.
        /// </summary>
        protected enum NarrativeItem
        {
            /// <summary>
            /// The role of a fixture.
            /// </summary>
            Role,

            /// <summary>
            /// The feature of a fixture.
            /// </summary>
            Feature,

            /// <summary>
            /// The benefit of a fixture.
            /// </summary>
            Benefit
        }

        /// <summary>
        /// Gets narrative item names for the narrative item.
        /// </summary>
        protected IDictionary<NarrativeItem, string> NarrativeItemNames { get; } = new Dictionary<NarrativeItem, string>
        {
            [NarrativeItem.Benefit] = "In order",
            [NarrativeItem.Role] = "As",
            [NarrativeItem.Feature] = "I want"
        };

        /// <summary>
        /// Gets or sets a type of the step to run previously.
        /// </summary>
        protected Type PreviousStepType { get; set; }

        /// <summary>
        /// Formats a fixture with the specified descriptor of it.
        /// </summary>
        /// <param name="descriptor">The descriptor of the fixture.</param>
        /// <returns>The formatted description.</returns>
        protected virtual FormattedDescription FormatFixture(FixtureDescriptor descriptor)
        {
            PreviousStepType = null;

            var fixtureName = FixtureNames.GetOrDefault(descriptor.FixtureAttributeType, () => ToDefaultFixtureName(descriptor));

            var lines = descriptor.Description.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (!string.IsNullOrEmpty(fixtureName)) { lines[0] = FormatFixture(fixtureName, lines[0]); }

            var descriptionItems = new List<FormattedDescription>();
            descriptionItems.AddRange(FormatNarrative(descriptor.Benefit, descriptor.Role, descriptor.Feature));
            return new FormattedDescription
            {
                Lines = lines,
                FirstLineIndent = FixtureFirstLineIndent(fixtureName),
                LineIndent = FixtureLineIndent(fixtureName),
                Items = descriptionItems.AsReadOnly()
            };
        }

        /// <summary>
        /// Gets a default fixture name of the specified descriptor of a fixture.
        /// </summary>
        /// <param name="descriptor">The descriptor of a fixture.</param>
        /// <returns>The default fixture name of the specified descriptor of a fixture.</returns>
        protected virtual string ToDefaultFixtureName(FixtureDescriptor descriptor) => string.Empty;

        /// <summary>
        /// Gets a formatted string expression of the specified fixture name and description.
        /// </summary>
        /// <param name="fixtureName">The name of a fixture.</param>
        /// <param name="description">The description of a fixture.</param>
        /// <returns>
        /// The formatted string expression of the specified fixture name and description.
        /// </returns>
        protected virtual string FormatFixture(string fixtureName, string description) => $"{fixtureName}: {description}";

        /// <summary>
        /// Gets a first line indent of the specified fixture name.
        /// </summary>
        /// <param name="fixtureName">The name of a fixture.</param>
        /// <returns>The first line indent of the specified fixture name.</returns>
        protected virtual string FixtureFirstLineIndent(string fixtureName) => string.Empty;

        /// <summary>
        /// Gets a line indent of the specified fixture name.
        /// </summary>
        /// <param name="fixtureName">The name of a fixture.</param>
        /// <returns>The line indent of the specified fixture name.</returns>
        protected virtual string FixtureLineIndent(string fixtureName)
            => string.IsNullOrEmpty(fixtureName) ? string.Empty : new string(' ', fixtureName.Length + 2);

        /// <summary>
        /// Formats a narrative.
        /// </summary>
        /// <param name="benefit">The benefit of a fixture.</param>
        /// <param name="role">The role of a fixture.</param>
        /// <param name="feature">The feature of a fixture.</param>
        /// <returns>
        /// The formatted description formatted with the specified benafit, role, and feature.
        /// </returns>
        protected virtual IEnumerable<FormattedDescription> FormatNarrative(string benefit, string role, string feature)
        {
            var narrativeItems = new List<FormattedDescription>();

            if (!string.IsNullOrWhiteSpace(benefit))
            {
                narrativeItems.Add(CreateNarrativeDescription(NarrativeItem.Benefit, benefit));
            }
            if (!string.IsNullOrWhiteSpace(role))
            {
                narrativeItems.Add(CreateNarrativeDescription(NarrativeItem.Role, role));
            }
            if (!string.IsNullOrWhiteSpace(feature))
            {
                narrativeItems.Add(CreateNarrativeDescription(NarrativeItem.Feature, feature));
            }

            return narrativeItems.AsReadOnly();
        }

        /// <summary>
        /// Creates a narrative description.
        /// </summary>
        /// <param name="narrativeItem">The narrative item to be formatted.</param>
        /// <param name="value">The value of the narrative item.</param>
        /// <returns>
        /// The formatted description formatted with the specified value.
        /// </returns>
        protected virtual FormattedDescription CreateNarrativeDescription(NarrativeItem narrativeItem, string value)
        {
            var narrativeItemName = NarrativeItemNames.GetOrDefault(narrativeItem, () => ToDefaultNarrativeItemName(narrativeItem));

            var lines = value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (!string.IsNullOrEmpty(narrativeItemName)) { lines[0] = FormatNarrative(narrativeItemName, lines[0]); }

            return new FormattedDescription
            {
                Lines = lines,
                FirstLineIndent = NarrativeFirstLineIndent(narrativeItemName),
                LineIndent = NarrativeLineIndent(narrativeItemName)
            };
        }

        /// <summary>
        /// Gets a default narrative item name of the specified narrative item.
        /// </summary>
        /// <param name="narrativeItem">The narrative item.</param>
        /// <returns>The default narrative item name of the specified narraitive item.</returns>
        protected virtual string ToDefaultNarrativeItemName(NarrativeItem narrativeItem) => string.Empty;

        /// <summary>
        /// Gets a formatted string expression of the specified narrative item name and description.
        /// </summary>
        /// <param name="narrativeItemName">The narrative item.</param>
        /// <param name="description">The description of a narrative item.</param>
        /// <returns>
        /// The formatted string expression of the specified narrative item name and description.
        /// </returns>
        protected virtual string FormatNarrative(string narrativeItemName, string description) => $"{narrativeItemName} {description}";

        /// <summary>
        /// Gets a first line indent of the specified narrative item name.
        /// </summary>
        /// <param name="narrativeItemName">The name of a narrative item.</param>
        /// <returns>The first line indent of the specified narrative item name.</returns>
        protected virtual string NarrativeFirstLineIndent(string narrativeItemName) => string.Empty;

        /// <summary>
        /// Gets a line indent of the specified narrative item name.
        /// </summary>
        /// <param name="narrativeItemName">The name of a narrative item.</param>
        /// <returns>The line indent of the specified narrative item name.</returns>
        protected virtual string NarrativeLineIndent(string narrativeItemName) => string.Empty;

        /// <summary>
        /// Formats a fixture step with the specified step.
        /// </summary>
        /// <param name="step">The fixture step.</param>
        /// <returns>The formatted description.</returns>
        protected virtual FormattedDescription FormatFixtureStep(FixtureStep step)
        {
            var stepType = step.GetType();
            var stepName = StepNames.GetOrDefault(stepType, () => ToDefaultStepName(step));
            var conjunctionRequired = PreviousStepType == stepType;

            var lines = step.Description.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (!string.IsNullOrEmpty(stepName)) { lines[0] = FormatFixtureStep(stepName, lines[0], conjunctionRequired); }

            return new FormattedDescription
            {
                Lines = lines,
                FirstLineIndent = FixtureStepFirstLineIndent(stepName, conjunctionRequired),
                LineIndent = FixtureStepLineIndent(stepName, conjunctionRequired)
            };
        }

        /// <summary>
        /// Gets a default step name of the specified fixture step.
        /// </summary>
        /// <param name="step">The fixture step.</param>
        /// <returns>The default step name of the specified fixture step.</returns>
        protected virtual string ToDefaultStepName(FixtureStep step)
        {
            var stepName = step.GetType().Name;
            var suffixIndex = stepName.LastIndexOf("Step");
            if (suffixIndex > 0)
            {
                stepName = stepName.Substring(0, suffixIndex);
            }
            return stepName;
        }

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
        protected virtual string FormatFixtureStep(string stepName, string description, bool conjunctionRequired)
            => conjunctionRequired ? $"{Conjunction} {description}" : $"{stepName} {description}";

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
        protected virtual string FixtureStepFirstLineIndent(string stepName, bool conjunctionRequired)
            => conjunctionRequired ? new string(' ', stepName.Length - Conjunction.Length) : string.Empty;

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
        protected virtual string FixtureStepLineIndent(string stepName, bool conjunctionRequired)
            => new string(' ', stepName.Length + 1);

        FormattedDescription IFixtureFormatter.FormatFixture(FixtureDescriptor descriptor)
            => FormatFixture(descriptor.RequireNonNull(nameof(descriptor)));

        FormattedDescription IFixtureFormatter.FormatFixtureStep(FixtureStep step)
        {
            var description = FormatFixtureStep(step.RequireNonNull(nameof(step)));
            PreviousStepType = step.GetType();
            return description;

        }
    }
}
