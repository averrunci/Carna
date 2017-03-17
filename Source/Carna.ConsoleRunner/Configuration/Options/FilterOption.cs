// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;

namespace Carna.ConsoleRunner.Configuration.Options
{
    /// <summary>
    /// Represents an option of a filter.
    /// </summary>
    public class FilterOption : CarnaRunnerCommandLineOption
    {
        /// <summary>
        /// Gets keys of the filter option.
        /// </summary>
        public override IEnumerable<string> Keys { get; } = new[] { "/filter", "/f" };

        /// <summary>
        /// Gets a description of the filter option.
        /// </summary>
        public override string Description { get; } = @"
  /filter:<pattern>
    Specifies a pattern of a full name of a fixture
    method that is executed with the regular expression.
    (Short form: /f)";

        /// <summary>
        /// Gets an order of the filter option.
        /// </summary>
        public override int Order { get; } = 2;

        /// <summary>
        /// Applies the specified context of the command line option to the specified command line options.
        /// </summary>
        /// <param name="options">The command line options to apply the command line option.</param>
        /// <param name="context">The context of the command line option to be applied.</param>
        protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
        {
            options.Filter = context.Value;
        }
    }
}
