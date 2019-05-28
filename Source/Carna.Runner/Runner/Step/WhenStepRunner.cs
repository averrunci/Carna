﻿// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;
using System.Threading.Tasks;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides the function to run a When step.
    /// </summary>
    public class WhenStepRunner : FixtureStepRunner<WhenStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteStepRunner"/> class
        /// with the specified When step.
        /// </summary>
        /// <param name="step">The When step to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="step"/> is <c>null</c>.
        /// </exception>
        public WhenStepRunner(WhenStep step) : base(step.RequireNonNull(nameof(step)))
        {
        }

        /// <summary>
        /// Runs a When step with the specified results of a fixture step.
        /// </summary>
        /// <param name="results">The results of the fixture step that was completed running.</param>
        /// <returns>The result of the When step running.</returns>
        protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
        {
            if (Step.Action == null && Step.AsyncAction == null)
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            if (results.HasExceptionAt<GivenStep>() || results.HasLatestExceptionAt<WhenStep>())
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            if (results.HasStatusAt<GivenStep>(FixtureStepStatus.Ready) || results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Ready))
            {
                return FixtureStepResult.Of(Step).Ready();
            }

            if (results.HasStatusAt<GivenStep>(FixtureStepStatus.Pending) || results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Pending))
            {
                return FixtureStepResult.Of(Step).Pending();
            }

            try
            {
                RunWhenStep();

                return FixtureStepResult.Of(Step).Passed();
            }
            catch (Exception exc)
            {
                return FixtureStepResult.Of(Step).Failed(exc);
            }
        }

        private void RunWhenStep()
        {
            if (Step.Timeout.HasValue)
            {
                var task = Step.Action == null ? Step.AsyncAction?.Invoke() : Task.Run(() => Step.Action.Invoke());
                if (task == null) return;

                RunWhenStep(Step.Action == null ? Step.AsyncAction?.Invoke() : Task.Run(() => Step.Action.Invoke()), Step.Timeout.Value);
            }
            else
            {
                Step.Action?.Invoke();
                Step.AsyncAction?.Invoke()?.GetAwaiter().GetResult();
            }
        }

        private void RunWhenStep(Task task, TimeSpan timeout)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(timeout))
            {
                RunWhenStep(task, timeout, cancellationTokenSource.Token);
            }
        }

        private void RunWhenStep(Task task, TimeSpan timeout, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            using (cancellationToken.Register(() => taskCompletionSource.TrySetCanceled(cancellationToken), false))
            {
                RunWhenStep(task, taskCompletionSource.Task, timeout);
            }
        }

        private void RunWhenStep(Task task, Task timeoutTask, TimeSpan timeout)
        {
            var completedTask = Task.WhenAny(task, timeoutTask).GetAwaiter().GetResult();
            if (completedTask == task)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                throw new AssertionException(Step, new TimeoutException($"Expected action time is within {timeout.Milliseconds}ms"));
            }
        }
    }
}
