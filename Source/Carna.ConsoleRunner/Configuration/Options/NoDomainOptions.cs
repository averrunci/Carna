// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
#if NET461
using System.Collections.Generic;

namespace Carna.ConsoleRunner.Configuration.Options
{
    /// <summary>
    /// Represents an option not to create a domain for each assembly.
    /// </summary>
    public class NoDomainOption : CarnaRunnerCommandLineOption
    {
        /// <summary>
        /// Gets keys of the help option.
        /// </summary>
        public override IEnumerable<string> Keys { get; } = new[] { "/nodomain" };

        /// <summary>
        /// Gets a description of the help option.
        /// </summary>
        public override string Description { get; } = @"
  /nodomain
    Specifies that no domain is created for each
    assembly. The default is to create a separate
    domain for each assembly.";

        /// <summary>
        /// Gets an order of the help option.
        /// </summary>
        public override int Order { get; } = 3;

        /// <summary>
        /// Applies the specified context of the command line option to the specified command line options.
        /// </summary>
        /// <param name="options">The command line options to apply the command line option.</param>
        /// <param name="context">The context of the command line option to be applied.</param>
        protected override void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
        {
            options.CanCreateSeparateDomain = false;
        }
    }
}
#endif
