// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Carna.Runner
{
    internal class FixtureRunner
    {
        private readonly IFixture fixture;
        private readonly MethodInfo fixtureMethod;
        private readonly object fixtureInstance;
        private readonly object[] sampleData;

        public FixtureRunner(IFixture fixture, MethodInfo fixtureMethod, object fixtureInstance, object[] sampleData)
        {
            this.fixture = fixture.RequireNonNull(nameof(fixtureInstance));
            this.fixtureMethod = fixtureMethod.RequireNonNull(nameof(fixtureMethod));
            this.fixtureInstance = fixtureInstance.RequireNonNull(nameof(fixtureInstance));
            this.sampleData = sampleData;
        }

        public void Run()
        {
            if (RequiresSta())
            {
                RunFixtureInSta();
            }
            else
            {
                RunFixture();
            }
        }

        private bool RequiresSta()
        {
            if (fixtureMethod.GetCustomAttribute<FixtureAttribute>()?.RequiresSta ?? false) return true;
            if (fixtureInstance.GetType().GetCustomAttribute<FixtureAttribute>(true)?.RequiresSta ?? false) return true;
            if (fixture.IsStaFixture) return true;

            return false;
        }

        private void RunFixture() => (fixtureMethod.Invoke(fixtureInstance, sampleData) as Task)?.GetAwaiter().GetResult();

        private void RunFixtureInSta() => RunFixtureInStaAsync().GetAwaiter().GetResult();

        private Task RunFixtureInStaAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    RunFixture();
                    taskCompletionSource.SetResult(null);
                }
                catch (Exception exc)
                {
                    taskCompletionSource.SetException(exc);
                }
            })
            {
                IsBackground = true
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return taskCompletionSource.Task;
        }
    }
}
