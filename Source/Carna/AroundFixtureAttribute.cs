// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna
{
    /// <summary>
    /// Specifies the fixture that is intercepted before or after running it.
    /// </summary>
    public abstract class AroundFixtureAttribute : Attribute
    {
        /// <summary>
        /// Occurs before running the fixture.
        /// </summary>
        /// <param name="context">The context of the fixture.</param>
        public virtual void OnFixtureRunning(IFixtureContext context)
        {
        }

        /// <summary>
        /// Occurs after running the fixture.
        /// </summary>
        /// <param name="context">The context of the fixture</param>
        public virtual void OnFixtureRun(IFixtureContext context)
        {
        }
    }
}
