// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Fixture implemented IDisposable")]
class FixtureSpec_RunFixture_FixtureImplementedIDisposable : FixtureSpec_FixtureImplementedIDisposableContext
{
    protected override Type TargetFixtureType => typeof(TestFixtures.SimpleDisposableFixture);
    protected override string TargetMethodName => "FixtureMethod";
    protected override string TargetFixtureDescription => "Fixture Method Example";
    protected override string TargetMethodDescription => "FixtureMethod";
    protected override string TargetMethodFullName => "Carna.TestFixtures+SimpleDisposableFixture.FixtureMethod";
}