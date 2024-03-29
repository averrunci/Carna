﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Xml;
using System.Xml.Linq;
using Carna.Runner.Step;

namespace Carna.Runner.Reporters;

/// <summary>
/// Provides the function to report a result of a fixture running
/// using an XML format.
/// </summary>
public class XmlFixtureReporter : FixtureReporter
{
    /// <summary>
    /// Gets an output path.
    /// </summary>
    protected string? OutputPath => GetOptionValueOrDefault("outputPath", () => "Results.xml");

    /// <summary>
    /// Gets or sets an XML Document to which results are reported.
    /// </summary>
    protected XDocument? Document { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlFixtureReporter"/> class
    /// with the specified options.
    /// </summary>
    /// <param name="options">The options to be applied.</param>
    public XmlFixtureReporter(IDictionary<string, string?>? options) : base(options)
    {
    }

    /// <summary>
    /// Handles the event when to start reporting results.
    /// </summary>
    /// <param name="results">The fixture running results.</param>
    protected override void OnReporting(IEnumerable<FixtureResult> results)
    {
        var fixtureResults = results.ToList();

        Document = new XDocument(
            new XElement("fixtures",
                new XAttribute("total", fixtureResults.TotalCount()),
                new XAttribute("passed", fixtureResults.PassedCount()),
                new XAttribute("pending", fixtureResults.PendingCount()),
                new XAttribute("failed", fixtureResults.FailedCount()),
                new XAttribute("startTime", fixtureResults.StartTime()),
                new XAttribute("endTime", fixtureResults.EndTime()),
                new XAttribute("duration", (fixtureResults.EndTime() - fixtureResults.StartTime()).TotalSeconds)
            )
        );
    }

    /// <summary>
    /// Reports the specified fixture running result.
    /// </summary>
    /// <param name="result">The fixture running result.</param>
    /// <param name="level">The level of the fixture running result.</param>
    protected override void Report(FixtureResult result, int level)
    {
        var documentRoot = Document?.Root;
        if (documentRoot is null) return;

        ReportFixture(result, documentRoot);
    }

    /// <summary>
    /// Reports the specified fixture running result to the specified XML element.
    /// </summary>
    /// <param name="result">The fixture running result.</param>
    /// <param name="element">The XML element to which the result is reported.</param>
    protected virtual void ReportFixture(FixtureResult result, XElement element)
    {
        var fixtureElement = new XElement("fixture",
            new XAttribute("type", result.FixtureDescriptor.FixtureAttributeType),
            new XAttribute("name", result.FixtureDescriptor.Name),
            new XAttribute("fullName", result.FixtureDescriptor.FullName),
            new XAttribute("description", result.FixtureDescriptor.Description),
            new XAttribute("tag", result.FixtureDescriptor.Tag ?? string.Empty),
            new XAttribute("benefit", result.FixtureDescriptor.Benefit ?? string.Empty),
            new XAttribute("role", result.FixtureDescriptor.Role ?? string.Empty),
            new XAttribute("feature", result.FixtureDescriptor.Feature ?? string.Empty),
            new XAttribute("background", result.FixtureDescriptor.Background ?? string.Empty),
            new XAttribute("status", result.Status),
            new XAttribute("startTime", result.StartTime.GetValueOrDefault()),
            new XAttribute("endTime", result.EndTime.GetValueOrDefault()),
            new XAttribute("duration", result.Duration.GetValueOrDefault().TotalSeconds),
            new XAttribute("formattedDescription", FixtureFormatter.FormatFixture(result.FixtureDescriptor))
        );
        if (result.Exception is not null) fixtureElement.Add(new XElement("exception", result.Exception));
        result.StepResults.ForEach(stepResult => ReportFixtureStep(stepResult, fixtureElement));
        result.Results.ForEach(subResult => ReportFixture(subResult, fixtureElement));

        element.Add(fixtureElement);
    }

    /// <summary>
    /// Reports the specified fixture step running result to the specified XML element.
    /// </summary>
    /// <param name="result">The fixture step running result.</param>
    /// <param name="element">The XML element to which the result is reported.</param>
    protected virtual void ReportFixtureStep(FixtureStepResult result, XElement element)
    {
        var stepElement = new XElement("step",
            new XAttribute("type", result.Step.GetType()),
            new XAttribute("description", result.Step.Description),
            new XAttribute("status", result.Status),
            new XAttribute("startTime", result.StartTime.GetValueOrDefault()),
            new XAttribute("endTime", result.EndTime.GetValueOrDefault()),
            new XAttribute("duration", result.Duration.GetValueOrDefault().TotalSeconds),
            new XAttribute("formattedDescription", FixtureFormatter.FormatFixtureStep(result.Step))
        );
        if (result.Exception is not null) stepElement.Add(new XElement("exception", result.Exception));

        element.Add(stepElement);
    }

    /// <summary>
    /// Handles the event when to complete reporting results.
    /// </summary>
    protected override void OnReported()
    {
        if (OutputPath is null || Document is null) return;

        var directoryPath = Path.GetDirectoryName(OutputPath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        using var stream = new FileStream(OutputPath, FileMode.Create, FileAccess.Write);
        using var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true });
        Document.WriteTo(writer);
    }
}