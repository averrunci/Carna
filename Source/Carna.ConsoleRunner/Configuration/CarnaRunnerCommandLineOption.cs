// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Linq;

namespace Carna.ConsoleRunner.Configuration
{
    /// <summary>
    /// Represents a command line option for CarnaRunner.
    /// </summary>
    public abstract class CarnaRunnerCommandLineOption
    {
        /// <summary>
        /// Gets keys of the command line option.
        /// </summary>
        public abstract IEnumerable<string> Keys { get; }

        /// <summary>
        /// Gets a description of the command line option.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Gets an order of the command line option.
        /// </summary>
        public abstract int Order { get; }

        /// <summary>
        /// Applies the specified context of the command line option to the specified command line options.
        /// </summary>
        /// <param name="options">The command line options to apply the command line option.</param>
        /// <param name="context">The context of the command line option to be applied.</param>
        /// <exception cref = "System.ArgumentNullException" >
        /// <paramref name="options"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="context"/> is <c>null</c>.
        /// </exception>
        public void Apply(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context)
        {
            options.RequireNonNull(nameof(options));
            context.RequireNonNull(nameof(context));

            if (!CanApply(context)) { return; }

            ApplyOption(options, context);
        }

        /// <summary>
        /// Gets a value that indicates whether the specified context of the command line option can be applied.
        /// </summary>
        /// <param name="context">The context of the command line option to be applied.</param>
        /// <returns>
        /// <c>true</c> if the specified context of the command line option can be applied;
        /// otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanApply(CarnaRunnerCommandLineOptionContext context)
            => context != null && context.HasKey && Keys.Contains(context.Key.ToLower());

        /// <summary>
        /// Applies the specified context of the command line option to the specified command line options.
        /// </summary>
        /// <param name="options">The command line options to apply the command line option.</param>
        /// <param name="context">The context of the command line option to be applied.</param>
        protected abstract void ApplyOption(CarnaRunnerCommandLineOptions options, CarnaRunnerCommandLineOptionContext context);
    }
}
