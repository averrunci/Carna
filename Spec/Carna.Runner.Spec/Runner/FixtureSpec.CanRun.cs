// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using NSubstitute;

namespace Carna.Runner
{
    [Context("Determines whether Fixture can run a fixture")]
    class FixtureSpec_CanRun : FixtureSteppable
    {
        private IFixture Fixture { get; }
        private IFixtureFilter Filter { get; }

        public FixtureSpec_CanRun()
        {
            Fixture = TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod");
            Filter = Substitute.For<IFixtureFilter>();
        }

        [Example("When a filter that is null is specified")]
        void Ex01()
        {
            Expect("the result should be true", () => Fixture.CanRun(null));
        }

        [Example("When a filter that returns true is specified")]
        void Ex02()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(true);

            Expect("the result should be true", () => Fixture.CanRun(Filter));
        }

        [Example("When a filter that returns false is specified")]
        void Ex03()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(false);

            Expect("the result should be false", () => !Fixture.CanRun(Filter));
        }
    }
}
