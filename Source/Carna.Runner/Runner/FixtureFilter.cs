// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Carna.Runner
{
    /// <summary>
    /// Provides the function to filter a fixture.
    /// </summary>
    public class FixtureFilter : IFixtureFilter
    {
        /// <summary>
        /// Gets a pattern to accept a fixture.
        /// </summary>
        protected string Pattern => GetOptionValue("pattern");

        private IDictionary<string, string> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureFilter"/> class
        /// with the specified options.
        /// </summary>
        /// <param name="options">The options to be applied.</param>
        public FixtureFilter(IDictionary<string, string> options)
        {
            Options = options ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Determines wheter to run a fixture of the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor of the fixture.</param>
        /// <returns>
        /// <c>true</c> if a fixture is run; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool Accept(FixtureDescriptor descriptor)
        {
            if (string.IsNullOrEmpty(Pattern)) { return true; }

            var regex = new Regex(Pattern, RegexOptions.Compiled);

            return (descriptor.Tag != null && regex.IsMatch(descriptor.Tag)) ||
                regex.IsMatch(descriptor.FullName);
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

        bool IFixtureFilter.Accept(FixtureDescriptor descriptor) => descriptor == null || Accept(descriptor);
    }
}
