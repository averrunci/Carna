// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Runtime.Serialization;

using Carna.Runner.Configuration;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents the configuration of CarnaUwpRunner.
    /// </summary>
    [DataContract]
    public class CarnaUwpRunnerConfiguration : CarnaRunnerConfiguration
    {
        /// <summary>
        /// Gets a value that indicates whether to exit the application automatically
        /// after the running of CarnaUwpRunner is completed.
        /// </summary>
        [DataMember(Name = "autoExit")]
        public bool AutoExit { get; set; }

        /// <summary>
        /// Gets the configuration of a formatter.
        /// </summary>
        [DataMember(Name = "formatter")]
        public CarnaConfiguration Formatter { get; set; }
    }
}
