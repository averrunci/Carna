// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner
{
    /// <summary>
    /// Represents a running result using <see cref="CarnaConsoleRunner"/>.
    /// </summary>
    public enum CarnaConsoleRunnerResult
    {
        /// <summary>
        /// The unknown running result.
        /// </summary>
        Unknown,

        /// <summary>
        /// Errors are occurred during running.
        /// </summary>
        Error,

        /// <summary>
        /// The running is success.
        /// </summary>
        Success,

        /// <summary>
        /// The running is failed
        /// </summary>
        Failed,

        /// <summary>
        /// The specified command line option is invalid.
        /// </summary>
        InvalidCommandLineOption
    }

    /// <summary>
    /// Provides extension method of the <see cref="CarnaConsoleRunnerResult"/>.
    /// </summary>
    public static class ConsoleRunnerResults
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CarnaConsoleRunnerResult"/>
        /// of the specified value.
        /// </summary>
        /// <param name="value">The value that indicates the <see cref="CarnaConsoleRunnerResult"/>.</param>
        /// <returns>
        /// The new instance of the <see cref="CarnaConsoleRunnerResult"/> of the specified value.
        /// </returns>
        public static CarnaConsoleRunnerResult Of(int value)
        {
            switch (value)
            {
                case -1: return CarnaConsoleRunnerResult.Error;
                case 0: return CarnaConsoleRunnerResult.Success;
                case 1: return CarnaConsoleRunnerResult.Failed;
                case 2: return CarnaConsoleRunnerResult.InvalidCommandLineOption;
                default: return CarnaConsoleRunnerResult.Unknown;
            }
        }

        /// <summary>
        /// Gets a value that indicates the specified <see cref="CarnaConsoleRunnerResult"/>.
        /// </summary>
        /// <param name="this">The <see cref="CarnaConsoleRunnerResult"/>.</param>
        /// <returns>
        /// The value that indicates the specified <see cref="CarnaConsoleRunnerResult"/>.
        /// </returns>
        public static int Value(this CarnaConsoleRunnerResult @this)
        {
            switch (@this)
            {
                case CarnaConsoleRunnerResult.Error: return -1;
                case CarnaConsoleRunnerResult.Success: return 0;
                case CarnaConsoleRunnerResult.Failed: return 1;
                case CarnaConsoleRunnerResult.InvalidCommandLineOption: return 2;
                default: return -2;
            }
        }
    }
}
