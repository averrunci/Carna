// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner
{
    [Context("Normal fixture")]
    class FixtureSpec_RunFixtureAsync_NormalFixture : FixtureSpec_NormalFixtureContext
    {
        protected override Type TargetFixtureType => typeof(TestFixtures.SimpleAsyncFixture);
        protected override string TargetMethodName => "FixtureMethodAsync";
        protected override string TargetFixtureDescription => "Fixture Asynchronous Method Example";
        protected override string TargetMethodDescription => "FixtureMethodAsync";
        protected override string TargetMethodFullName => "Carna.TestFixtures+SimpleAsyncFixture.FixtureMethodAsync";
    }
}
