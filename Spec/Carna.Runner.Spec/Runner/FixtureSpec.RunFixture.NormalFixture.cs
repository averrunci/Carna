// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner
{
    [Context("Normal fixture")]
    class FixtureSpec_RunFixture_NormalFixture : FixtureSpec_NormalFixtureContext
    {
        protected override Type TargetFixtureType => typeof(TestFixtures.SimpleFixture);
        protected override string TargetMethodName => "FixtureMethod";
        protected override string TargetFixtureDescription => "Fixture Method Example";
        protected override string TargetMethodDescription => "FixtureMethod";
        protected override string TargetMethodFullName => "Carna.TestFixtures+SimpleFixture.FixtureMethod";
    }
}
