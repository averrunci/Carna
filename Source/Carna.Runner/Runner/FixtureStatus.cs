// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    /// <summary>
    /// Represents a fixture status.
    /// </summary>
    public enum FixtureStatus
    {
        /// <summary>
        /// The fixture is ready.
        /// </summary>
        Ready,

        /// <summary>
        /// The fixture is pending.
        /// </summary>
        Pending,

        /// <summary>
        /// The fixture is running.
        /// </summary>
        Running,

        /// <summary>
        /// The fixture is passed.
        /// </summary>
        Passed,

        /// <summary>
        /// The fixture is failed.
        /// </summary>
        Failed
    }
}
