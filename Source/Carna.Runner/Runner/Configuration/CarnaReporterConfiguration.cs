// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Runtime.Serialization;

namespace Carna.Runner.Configuration
{
    /// <summary>
    /// Represents the configuration of a reporter.
    /// </summary>
    [DataContract]
    public class CarnaReporterConfiguration
    {
        /// <summary>
        /// Gets the configuration of a reporter.
        /// </summary>
        [DataMember(Name = "reporter")]
        public CarnaConfiguration Reporter { get; set; }

        /// <summary>
        /// Gets the configuration of a formatter.
        /// </summary>
        [DataMember(Name = "formatter")]
        public CarnaConfiguration Formatter { get; set; }
    }
}
