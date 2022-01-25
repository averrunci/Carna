// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Runner.Step;
using NSubstitute;

namespace Carna.Runner;

[Context("AroundFixtureAttribute")]
class FixtureSpec_RunFixture_AroundFixtureAttribute : FixtureSteppable
{
    void Assert(int expectedCount)
    {
        Expect("to execute before running a fixture", () => TestAroundFixtureAttribute.OnFixtureRunningCount.Value == expectedCount);
        Expect("to execute after running a fixture", () => TestAroundFixtureAttribute.OnFixtureRunCount.Value == expectedCount);
    }

    public FixtureSpec_RunFixture_AroundFixtureAttribute()
    {
        TestAroundFixtureAttribute.OnFixtureRunningCount.Value = 0;
        TestAroundFixtureAttribute.OnFixtureRunCount.Value = 0;
    }

    [Example("When a fixture is specified by one AroundAttribute")]
    void Ex01()
    {
        var fixture = TestFixtures.CreateFixture<TestFixtures.MethodFixtureSpecifiedByOneAroundFixtureAttribute>("Ex01");
        fixture.Run(null, Substitute.For<IFixtureStepRunnerFactory>());
        Assert(1);
    }

    [Example("When a fixture is specified by some AroundAttribute")]
    void Ex02()
    {
        var fixture = TestFixtures.CreateFixture<TestFixtures.MethodFixtureSpecifiedBySomeAroundFixtureAttributes>("Ex01");
        fixture.Run(null, Substitute.For<IFixtureStepRunnerFactory>());
        Assert(3);
    }
}