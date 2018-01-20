// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;

namespace Carna
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TestAroundFixtureAttribute : AroundFixtureAttribure
    {
        public static ThreadLocal<int> OnFixtureRunningCount { get; } = new ThreadLocal<int>();
        public static ThreadLocal<int> OnFixtureRunCount { get; } = new ThreadLocal<int>();

        public override void OnFixtureRunning(IFixtureContext context)
        {
            OnFixtureRunningCount.Value = OnFixtureRunningCount.Value + 1;
        }

        public override void OnFixtureRun(IFixtureContext context)
        {
            OnFixtureRunCount.Value = OnFixtureRunCount.Value + 1;
        }
    }

}
