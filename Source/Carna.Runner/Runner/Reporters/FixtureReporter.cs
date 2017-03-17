// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;

namespace Carna.Runner.Reporters
{
    /// <summary>
    /// Provides the function to report a result of a fixture running.
    /// </summary>
    public abstract class FixtureReporter : IFixtureReporter
    {
        /// <summary>
        /// Gets a formatter of a fixture.
        /// </summary>
        protected IFixtureFormatter FixtureFormatter { get; set; }

        private IDictionary<string, string> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureReporter"/> class
        /// with the specified options.
        /// </summary>
        /// <param name="options">The options to be applied.</param>
        protected FixtureReporter(IDictionary<string, string> options)
        {
            Options = options ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Reports a result of a fixture running with the specified fixture results.
        /// </summary>
        /// <param name="results">The fixture running results.</param>
        protected virtual void Report(IEnumerable<FixtureResult> results)
        {
            try
            {
                OnReporting(results);
                results.ForEach(result => Report(result, 0));
            }
            finally
            {
                OnReported();
            }
        }

        /// <summary>
        /// Reports the specified fixture running result.
        /// </summary>
        /// <param name="result">The fixture running result.</param>
        /// <param name="level">The level of the fixture running result.</param>
        protected abstract void Report(FixtureResult result, int level);

        /// <summary>
        /// Handles the event when to start reporting results.
        /// </summary>
        /// <param name="results">The fixture running results.</param>
        protected virtual void OnReporting(IEnumerable<FixtureResult> results)
        {
        }

        /// <summary>
        /// Handles the event when to complete reporting results.
        /// </summary>
        protected virtual void OnReported()
        {
        }

        /// <summary>
        /// Gets a option value of the specified key.
        /// </summary>
        /// <param name="key">The key for a option value.</param>
        /// <returns>
        /// The option value of the specified key if key is found; otherwise, <see cref="string.Empty"/>.
        /// </returns>
        protected string GetOptionValue(string key) => GetOptionValueOrDefault(key, () => string.Empty);

        /// <summary>
        /// Gets a option value of the specified.
        /// If key is not found, the specified default value is returned.
        /// </summary>
        /// <param name="key">The key for a option value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The option value of the specified key if key is found; otherwise, a value
        /// returned from <paramref name="defaultValue"/> function.
        /// </returns>
        protected string GetOptionValueOrDefault(string key, Func<string> defaultValue)
            => Options.ContainsKey(key) ? Options[key] : defaultValue();

        /// <summary>
        /// Gets a boolean option value of the specified key.
        /// </summary>
        /// <param name="key">The key for a option value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The boolean option value of the specified key if key is found; otherwise,
        /// the specified default value.
        /// </returns>
        protected bool GetBooleanOptionValue(string key, bool defaultValue = false)
            => bool.TryParse(GetOptionValue(key), out var optionValue) ? optionValue : defaultValue;

        IFixtureFormatter IFixtureReporter.FixtureFormatter
        {
            get { return FixtureFormatter; }
            set { FixtureFormatter = value; }
        }

        void IFixtureReporter.Report(IEnumerable<FixtureResult> results) => Report(results.RequireNonNull(nameof(results)));
    }
}
