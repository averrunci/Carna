// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Fixture implemented IFixtureSteppable")]
class FixtureSpec_RunFixtureAsync_FixtureImplementedIFixtureSteppable : FixtureSpec_FixtureImplementedIFixtureSteppableContext
{
    protected override Type TargetFixtureType => typeof(TestFixtures.SimpleAsyncFixtureSteppable);
    protected override string TargetMethodName => "FixtureMethodAsync";
    protected override string TargetFixtureDescription => "Fixture Asynchronous Method Example";
    protected override string TargetMethodDescription => "FixtureMethodAsync";
    protected override string TargetMethodFullName => "Carna.TestFixtures+SimpleAsyncFixtureSteppable.FixtureMethodAsync";
}