// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Carna.Runner.Configuration
{
    /// <summary>
    /// Represents the configuration used in Carna.
    /// </summary>
    [DataContract]
    public class CarnaConfiguration
    {
        /// <summary>
        /// Gets a type used in this configuration.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets options of this configuration.
        /// </summary>
        [DataMember(Name = "options")]
        public IDictionary<string, string> Options { get; set; }
    }
}
