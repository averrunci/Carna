// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TestAroundFixtureAttribute : AroundFixtureAttribute
{
    public static ThreadLocal<int> OnFixtureRunningCount { get; } = new();
    public static ThreadLocal<int> OnFixtureRunCount { get; } = new();

    public override void OnFixtureRunning(IFixtureContext context)
    {
        OnFixtureRunningCount.Value += 1;
    }

    public override void OnFixtureRun(IFixtureContext context)
    {
        OnFixtureRunCount.Value += 1;
    }
}