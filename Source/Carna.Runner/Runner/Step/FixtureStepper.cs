// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to take a fixture step.
    /// </summary>
    public class FixtureStepper : IFixtureStepper
    {
        /// <summary>
        /// Occurs when to start running a fixture step.
        /// </summary>
        public event FixtureStepRunEventHandler FixtureStepRunning;

        /// <summary>
        /// Occurs when to complete running a fixture step.
        /// </summary>
        public event FixtureStepRunEventHandler FixtureStepRun;

        /// <summary>
        /// Gets results of the fixture step running.
        /// </summary>
        public FixtureStepResultCollection Results { get; } = new FixtureStepResultCollection();

        /// <summary>
        /// Gets a factory of the <see cref="IFixtureStepRunner"/>.
        /// </summary>
        protected IFixtureStepRunnerFactory StepRunnerFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepper"/> class
        /// with the specified factory of the <see cref="IFixtureStepRunner"/>.
        /// </summary>
        /// <param name="stepRunnerFactory">The factory of the <see cref="IFixtureStepRunner"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stepRunnerFactory"/> is <c>null</c>.
        /// </exception>
        public FixtureStepper(IFixtureStepRunnerFactory stepRunnerFactory)
        {
            StepRunnerFactory = stepRunnerFactory.RequireNonNull(nameof(stepRunnerFactory));
        }

        /// <summary>
        /// Takes the specified fixture step.
        /// </summary>
        /// <param name="step">The fixture step to take.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public void Take(FixtureStep step)
        {
            step.RequireNonNull(nameof(step));

            var startTime = DateTime.UtcNow;
            OnFixtureStepRunning(new FixtureStepRunEventArgs(FixtureStepResult.Of(step).StartAt(startTime).Running().Build()));

            var result = StepRunnerFactory.Create(step).Run(Results).StartAt(startTime).EndAt(DateTime.UtcNow);
            Results.Add(result);

            OnFixtureStepRun(new FixtureStepRunEventArgs(result));
        }

        /// <summary>
        /// Raises the <see cref="FixtureStepRunning"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureStepRunning(FixtureStepRunEventArgs e) => FixtureStepRunning?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureStepRun"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureStepRun(FixtureStepRunEventArgs e) => FixtureStepRun?.Invoke(this, e);
    }
}
