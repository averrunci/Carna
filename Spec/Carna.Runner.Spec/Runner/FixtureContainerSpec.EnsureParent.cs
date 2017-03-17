﻿// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;

namespace Carna.Runner
{
    [Context("Ensures a parent")]
    class FixtureContainerSpec_EnsureParent : FixtureSteppable
    {
        private IFixture ParentContainer { get; set; }
        private FixtureContainer Container { get; set; }
        private IFixture Fixture { get; set; }

        public FixtureContainerSpec_EnsureParent()
        {
            ParentContainer = new FixtureContainer("FixtureContainer", new NamespaceFixtureAttribute());
            Container = new FixtureContainer(typeof(TestFixtures.SimpleFixture));
            ((FixtureContainer)ParentContainer).Add(Container);
        }

        [Example("When a Fixture is added")]
        void Ex01()
        {
            Fixture = TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod");
            Container.Add(Fixture);

            ParentContainer.EnsureParent();

            Expect("the parent of the added fixture is set to the parent fixture container", () => Fixture.ParentFixture == ParentContainer);
        }

        [Example("When a FixtureContainer is added")]
        void Ex02()
        {
            Fixture = new FixtureContainer(typeof(TestFixtures.SimpleFixture));
            Container.Add(Fixture);

            ParentContainer.EnsureParent();

            Expect("the parent of the added fixture container is set to the fixture container", () => Fixture.ParentFixture == Container);
        }

        [Example("When some Fistures are added")]
        void Ex03()
        {
            var fixtures = new[] {
                TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
                TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
                TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod")
            };
            Container.AddRange(fixtures);

            ParentContainer.EnsureParent();

            Expect("the parent of each added fixture is set to the parent fixture container.", () => fixtures.All(f => f.ParentFixture == ParentContainer));
        }

        [Example("When some FistureContainers are added")]
        void Ex04()
        {
            var fixtureContainers = new IFixture[] {
                new FixtureContainer(typeof(TestFixtures.SimpleFixture)),
                new FixtureContainer(typeof(TestFixtures.SimpleFixture)),
                new FixtureContainer(typeof(TestFixtures.SimpleFixture))
            };
            Container.AddRange(fixtureContainers);

            ParentContainer.EnsureParent();

            Expect("the parent of each added fixture container is set to the fixture container.", () => fixtureContainers.All(f => f.ParentFixture == Container));
        }
    }
}
