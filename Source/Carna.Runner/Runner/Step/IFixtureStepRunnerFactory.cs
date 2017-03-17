// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Reflection;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to create a new instance that implements the <see cref="IFixtureStepRunner"/>.
    /// </summary>
    public interface IFixtureStepRunnerFactory
    {
        /// <summary>
        /// Creates a new instance that runs the specified fixture step.
        /// </summary>
        /// <param name="step">The fixture step to run.</param>
        /// <returns>The new instance that runs the specified fixture step.</returns>
        IFixtureStepRunner Create(FixtureStep step);

        /// <summary>
        /// Registers the <see cref="IFixtureStepRunner"/> that is defined in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies in which the <see cref="IFixtureStepRunner"/> is defined.
        /// </param>
        void RegisterFrom(IEnumerable<Assembly> assemblies);
    }
}
