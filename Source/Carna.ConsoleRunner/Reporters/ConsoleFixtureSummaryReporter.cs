// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;
using Carna.Runner.Formatters;

namespace Carna.ConsoleRunner.Reporters;

/// <summary>
/// Provides the function to report the summary of the fixture running results
/// on the console.
/// </summary>
public class ConsoleFixtureSummaryReporter : IFixtureReporter
{
    /// <summary>
    /// Gets or sets a formatter of a fixture.
    /// </summary>
    protected IFixtureFormatter FixtureFormatter { get; set; } = new FixtureFormatter();

    /// <summary>
    /// Gets or sets a failure count.
    /// </summary>
    protected int FailureCount { get; set; }

    /// <summary>
    /// Reports a result of a fixture running with the specified fixture results.
    /// </summary>
    /// <param name="results">The fixture running results.</param>
    protected virtual void Report(IEnumerable<FixtureResult> results)
    {
        FailureCount = 0;

        var fixtureResults = results.ToList();
        ReportFailedInformation(fixtureResults);
        ReportSummary(fixtureResults);
    }

    /// <summary>
    /// Reports the failed information of the specified fixture running results.
    /// </summary>
    /// <param name="results">The fixture running results.</param>
    protected virtual void ReportFailedInformation(IEnumerable<FixtureResult> results)
    {
        results.ForEach(result =>
        {
            if (result.Exception is not null)
            {
                EnsureFailureTitle();
                CarnaConsole.WriteFailure($"{++FailureCount}) ");
                CarnaConsole.WriteLineFailure(result.Exception);
                CarnaConsole.WriteLine();
            }

            if (result.StepExceptions.Any()) ++FailureCount;

            result.StepExceptions.ForEachWithIndex((exception, index) =>
            {
                EnsureFailureTitle();
                CarnaConsole.WriteFailure($"{FailureCount}-{index + 1}) ");
                CarnaConsole.WriteLineFailure(exception);
                CarnaConsole.WriteLine();
            });

            ReportFailedInformation(result.Results);
        });
    }

    /// <summary>
    /// Ensures the failure title.
    /// </summary>
    protected virtual void EnsureFailureTitle()
    {
        if (FailureCount > 0) return;

        CarnaConsole.WriteLineFailure("Failures");
    }

    /// <summary>
    /// Reports the summary of the specified fixture running results.
    /// </summary>
    /// <param name="results">The fixture running results.</param>
    protected virtual void ReportSummary(IEnumerable<FixtureResult> results)
    {
        var fixtureResults = results.ToList();

        ReportRunSummary(fixtureResults);

        if (fixtureResults.IsEmpty())
        {
            CarnaConsole.WriteLine();
            return;
        }

        ReportExecutionTimeSummary(fixtureResults);
    }

    private void ReportRunSummary(IList<FixtureResult> results)
    {
        CarnaConsole.WriteLineHeader("Run Summary");
        CarnaConsole.WriteItem("  ");

        ReportTotalCount(results.TotalCount());
        CarnaConsole.WriteItem(", ");

        ReportPassedCount(results.PassedCount());
        CarnaConsole.WriteItem(", ");

        ReportPendingCount(results.PendingCount());
        CarnaConsole.WriteItem(", ");

        ReportFailedCount(results.FailedCount());
        CarnaConsole.WriteLine();
    }

    private void ReportTotalCount(int totalCount)
    {
        CarnaConsole.WriteItem("Total Count: ");
        CarnaConsole.WriteValue(totalCount);
    }
        
    private void ReportPassedCount(int passedCount)
    {
        CarnaConsole.WriteItem("Passed: ");
        CarnaConsole.WriteValue(passedCount);
    }

    private void ReportPendingCount(int pendingCount)
    {
        CarnaConsole.WriteItem("Pending: ");
        if (pendingCount == 0)
        {
            CarnaConsole.WriteValue(pendingCount);
        }
        else
        {
            CarnaConsole.WritePending(pendingCount);
        }
    }

    private void ReportFailedCount(int failedCount)
    {
        CarnaConsole.WriteItem("Failed: ");
        if (failedCount == 0)
        {
            CarnaConsole.WriteValue(failedCount);
        }
        else
        {
            CarnaConsole.WriteFailure(failedCount);
        }
    }

    private void ReportExecutionTimeSummary(IList<FixtureResult> results)
    {
        var startTime = results.StartTime();
        var endTime = results.EndTime();

        CarnaConsole.WriteItem("  Start Time: ");
        CarnaConsole.WriteLineValue($"{startTime:u}");

        CarnaConsole.WriteItem("    End Time: ");
        CarnaConsole.WriteLineValue($"{endTime:u}");

        CarnaConsole.WriteItem("    Duration: ");
        CarnaConsole.WriteLineValue($"{(endTime - startTime).TotalSeconds:0.000} seconds");
        CarnaConsole.WriteLine();
    }

    IFixtureFormatter IFixtureReporter.FixtureFormatter
    {
        get => FixtureFormatter;
        set => FixtureFormatter = value;
    }
    void IFixtureReporter.Report(IEnumerable<FixtureResult> results) => Report(results);
}