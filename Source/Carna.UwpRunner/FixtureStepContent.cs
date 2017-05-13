// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner;
using Carna.Runner.Step;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a content of a fixture step.
    /// </summary>
    public class FixtureStepContent
    {
        /// <summary>
        /// Gets a description of a fixture step.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets a status of a fixture step running.
        /// </summary>
        public FixtureStepStatus Status { get; }

        /// <summary>
        /// Gets a duration of a fixture step running.
        /// </summary>
        public string Duration { get; }

        /// <summary>
        /// Gets an exception that occurred while a fixture step was running.
        /// </summary>
        public string Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepContent"/> class
        /// with the specified result of the fixture step running and the formatter
        /// to format the description of the fixture step.
        /// </summary>
        /// <param name="result">The result of the fixture step running.</param>
        /// <param name="formatter">The formatter to format the description of the fixture step.</param>
        public FixtureStepContent(FixtureStepResult result, IFixtureFormatter formatter)
        {
            Description = formatter.FormatFixtureStep(result.Step).ToString();
            Status = result.Status;
            Duration = result.Duration.HasValue ? $"{result.Duration.Value.TotalSeconds:0.000} s" : string.Empty;
            Exception = result.Exception?.ToString();
        }
    }
}
