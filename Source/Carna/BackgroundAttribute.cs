// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the background of the fixture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = true)]
    public class BackgroundAttribute : Attribute
    {
        /// <summary>
        /// Gets a description of the background.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundAttribute"/> class
        /// with the specified description of the background.
        /// </summary>
        /// <param name="description">The description of the background.</param>
        public BackgroundAttribute(string description) => Description = description;
    }
}
