// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner
{
    [Context("Fixture implemented IDisposable")]
    class FixtureSpec_RunFixtureAsync_FixtureImplementedIDisposable : FixtureSpec_FixtureImplementedIDisposableContext
    {
        protected override Type TargetFixtureType => typeof(TestFixtures.SimpleDisposableAsyncFixture);
        protected override string TargetMethodName => "FixtureMethodAsync";
        protected override string TargetFixtureDescription => "Fixture Asynchronous Method Example";
        protected override string TargetMethodDescription => "FixtureMethodAsync";
        protected override string TargetMethodFullName => "Carna.TestFixtures+SimpleDisposableAsyncFixture.FixtureMethodAsync";
    }
}
