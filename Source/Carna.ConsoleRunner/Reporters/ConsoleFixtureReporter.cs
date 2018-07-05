// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;

using Carna.Runner;
using Carna.Runner.Reporters;
using Carna.Runner.Step;

namespace Carna.ConsoleRunner.Reporters
{
    /// <summary>
    /// Provides the function to report a result of a fixture running
    /// on the console.
    /// </summary>
    public class ConsoleFixtureReporter : FixtureReporter
    {
        /// <summary>
        /// Gets a value that indicates whether the full content is reported.
        /// </summary>
        protected bool IsFullReport => GetBooleanOptionValue("full");

        /// <summary>
        /// Gets a value that indicates whether the result of the fixture step is reported.
        /// </summary>
        protected bool StepVisible => GetBooleanOptionValue("stepVisible");

        /// <summary>
        /// Gets a value that indicates whether the status is reported.
        /// </summary>
        protected bool StatusVisible => GetBooleanOptionValue("statusVisible", true);

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleFixtureReporter"/>
        /// with the specified options
        /// </summary>
        /// <param name="options">The options to be applied.</param>
        public ConsoleFixtureReporter(IDictionary<string, string> options) : base(options)
        {
        }

        /// <summary>
        /// Reports the specified fixture running result.
        /// </summary>
        /// <param name="result">The fixture running result.</param>
        /// <param name="level">The level of the fixture running result.</param>
        protected override void Report(FixtureResult result, int level)
        {
            var nextLevel = ReportFixtureTitle(result, level);
            ReportFixtureStep(result, nextLevel);
            result.Results.ForEach(subResult => Report(subResult, nextLevel));

            if (result.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute))
            {
                CarnaConsole.WriteLine();
            }
        }

        /// <summary>
        /// Reports the title of the fixture with the specified fixture running result
        /// and level of the fixture running result.
        /// </summary>
        /// <param name="result">The fixture running result.</param>
        /// <param name="level">The level of the fixture running result.</param>
        /// <returns>The next level of the specified fixture running result.</returns>
        protected virtual int ReportFixtureTitle(FixtureResult result, int level)
        {
            var formattedDescription = FixtureFormatter.FormatFixture(result.FixtureDescriptor);
            if (result.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute) ||
                result.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute))
            {
                return IsFullReport ? ReportFixtureTitle(formattedDescription, level, null) : level;
            }

            return ReportFixtureTitle(formattedDescription, level, result.Status);
        }

        /// <summary>
        /// Reports the title of the fixture with the specified description, level, and status.
        /// </summary>
        /// <param name="formattedDescription">The formatted description of the fixture.</param>
        /// <param name="level">The level of the fixture running result.</param>
        /// <param name="status">The status of the fixture running.</param>
        /// <returns>The next level of the specified fixture running result.</returns>
        protected virtual int ReportFixtureTitle(FormattedDescription formattedDescription, int level, FixtureStatus? status)
        {
            var indent = Indent(level);

            ReportValue(JoinFormattedLines(formattedDescription, indent), status, !status.HasValue);
            if (status.HasValue) ReportStatus(status.Value, true);

            if (formattedDescription.Items.IsEmpty()) return level + 1;

            indent = Indent(level + 1);
            formattedDescription.Items.ForEach(item => ReportValue(JoinFormattedLines(item, indent), status, true));

            return level + 2;
        }

        /// <summary>
        /// Reports the fixture step running result that is contained by the specified fixture running result
        /// and the level of the fixture running result.
        /// </summary>
        /// <param name="result">The fixture running result.</param>
        /// <param name="level">The level of the fixture running result.</param>
        protected virtual void ReportFixtureStep(FixtureResult result, int level)
        {
            if (!StepVisible) return;

            var indent = Indent(level);
            result.StepResults.ForEach(stepResult =>
            {
                ReportValue(JoinFormattedLines(FixtureFormatter.FormatFixtureStep(stepResult.Step), indent), stepResult.Status);
                ReportStatus(stepResult.Status, true);
            });
        }

        /// <summary>
        /// Joins formatted lines of the specified description with <see cref="Environment.NewLine"/>
        /// and the specified indent.
        /// </summary>
        /// <param name="description">The formatted description of the fixture.</param>
        /// <param name="indent">The indent of a line.</param>
        /// <returns>
        ///  The string representation of the lines of a description that is joined
        /// with <see cref="Environment.NewLine"/> and the specified indent.
        /// </returns>
        protected virtual string JoinFormattedLines(FormattedDescription description, string indent) => description.JoinLines(indent);

        /// <summary>
        /// Reports the specified value with the specified status of the fixture running.
        /// </summary>
        /// <param name="value">The value to be reported.</param>
        /// <param name="status">The status of the fixture running.</param>
        /// <param name="lineBreak">
        /// <c>true</c> if a line break is required; otherwise, <c>false</c>.
        /// </param>
        protected virtual void ReportValue(string value, FixtureStatus? status = null, bool lineBreak = false)
        {
            switch (status)
            {
                case FixtureStatus.Ready: CarnaConsole.WriteReady(value); break;
                case FixtureStatus.Pending: CarnaConsole.WritePending(value); break;
                case FixtureStatus.Failed: CarnaConsole.WriteFailure(value); break;
                case FixtureStatus.Passed: CarnaConsole.WriteValue(value); break;
                default: CarnaConsole.WriteHeader(value); break;
            }

            if (lineBreak) CarnaConsole.WriteLine();
        }

        /// <summary>
        /// Reports the specified status of the fixture running.
        /// </summary>
        /// <param name="status">The status of the fixture running.</param>
        /// <param name="lineBreak">
        /// <c>true</c> if a line break is required; otherwise, <c>false</c>.
        /// </param>
        protected virtual void ReportStatus(FixtureStatus status, bool lineBreak = false)
        {
            switch (status)
            {
                case FixtureStatus.Ready: ReportStatus(status, CarnaConsole.WriteReady); break;
                case FixtureStatus.Pending: ReportStatus(status, CarnaConsole.WritePending); break;
                case FixtureStatus.Failed: ReportStatus(status, CarnaConsole.WriteFailure); break;
                case FixtureStatus.Passed: ReportStatus(status, CarnaConsole.WriteSuccess); break;
            }

            if (lineBreak) CarnaConsole.WriteLine();
        }

        private void ReportStatus(FixtureStatus status, Action<object> action)
        {
            if (!StatusVisible) return;

            ReportValue(" - ", status);
            action(status);
        }

        /// <summary>
        /// Reports the specified value with the specified status of the fixture step running.
        /// </summary>
        /// <param name="value">The value to be reported.</param>
        /// <param name="status">The status of the fixture step running.</param>
        /// <param name="lineBreak">
        /// <c>true</c> if a line break is required; otherwise, <c>false</c>.
        /// </param>
        protected virtual void ReportValue(string value, FixtureStepStatus status, bool lineBreak = false)
        {
            switch (status)
            {
                case FixtureStepStatus.Ready: CarnaConsole.WriteReady(value); break;
                case FixtureStepStatus.Pending: CarnaConsole.WritePending(value); break;
                case FixtureStepStatus.Failed: CarnaConsole.WriteFailure(value); break;
                case FixtureStepStatus.None: CarnaConsole.WriteNote(value); break;
                default: CarnaConsole.WriteValue(value); break;
            }

            if (lineBreak) CarnaConsole.WriteLine();
        }

        /// <summary>
        /// Reports the specified status of the fixture step running.
        /// </summary>
        /// <param name="status">The status of the fixture step running.</param>
        /// <param name="lineBreak">
        /// <c>true</c> if a line break is required; otherwise, <c>false</c>.
        /// </param>
        protected virtual void ReportStatus(FixtureStepStatus status, bool lineBreak = false)
        {
            switch (status)
            {
                case FixtureStepStatus.Ready: ReportStatus(status, CarnaConsole.WriteReady); break;
                case FixtureStepStatus.Pending: ReportStatus(status, CarnaConsole.WritePending); break;
                case FixtureStepStatus.Failed: ReportStatus(status, CarnaConsole.WriteFailure); break;
                case FixtureStepStatus.Passed: ReportStatus(status, CarnaConsole.WriteSuccess); break;
            }

            if (lineBreak) CarnaConsole.WriteLine();
        }

        private void ReportStatus(FixtureStepStatus status, Action<object> action)
        {
            if (!StatusVisible) return;

            ReportValue(" - ", status);
            action(status);
        }

        /// <summary>
        /// Gets an indent of the specified level of the fixture running result.
        /// </summary>
        /// <param name="level">The level of the fixture running result.</param>
        /// <returns>The indent of the specified level of the fixture running result.</returns>
        protected virtual string Indent(int level) => new string(' ', level * 2);
    }
}
