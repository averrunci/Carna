// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the parameter to fixtures that are contained by a fixture
    /// specified by this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Gets a name of a parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class.
        /// </summary>
        public ParameterAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class
        /// with the specified name of a parameter.
        /// </summary>
        /// <param name="name">The name of a parameter.</param>
        public ParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
