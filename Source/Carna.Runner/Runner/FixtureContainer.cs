// Copyright (C) 2017-2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Carna.Runner.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Represents a container of a fixture.
    /// </summary>
    public class FixtureContainer : FixtureBase
    {
        /// <summary>
        /// Gets containing fixtures.
        /// </summary>
        protected List<IFixture> Fixtures { get; } = new List<IFixture>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContainer"/> class
        /// with the specified type of an instance of a fixture.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        public FixtureContainer(Type fixtureInstanceType) : base(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContainer"/> class
        /// with the specified name and attribute that specifies a fixture.
        /// </summary>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        public FixtureContainer(string name, FixtureAttribute attribute) : base(name, attribute.RequireNonNull(nameof(attribute)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContainer"/> class
        /// with the specified name, attribute that specifies a fixture, and
        /// containing fixtures.
        /// </summary>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <param name="fixtures">The containing fixtures.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtures"/> is <c>null</c>.
        /// </exception>
        public FixtureContainer(string name, FixtureAttribute attribute, IEnumerable<IFixture> fixtures) : base(name, attribute.RequireNonNull(nameof(attribute)))
        {
            AddRange(fixtures.RequireNonNull(nameof(fixtures)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContainer"/> class
        /// with the specified type of an instance of a fixture and containing fixtures.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <param name="fixtureMethod">The fixture method.</param>
        /// <param name="fixtures">The containing fixtures.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureMethod"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtures"/> is <c>null</c>.
        /// </exception>
        public FixtureContainer(Type fixtureInstanceType, MethodInfo fixtureMethod, IEnumerable<IFixture> fixtures) : base(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)), fixtureMethod.RequireNonNull(nameof(fixtureMethod)))
        {
            AddRange(fixtures.RequireNonNull(nameof(fixtures)));
        }

        /// <summary>
        /// Adds the specified fixture.
        /// </summary>
        /// <param name="fixture">The fixture to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixture"/> is <c>null</c>.
        /// </exception>
        public void Add(IFixture fixture) => Fixtures.Add(fixture.RequireNonNull(nameof(fixture)));

        /// <summary>
        /// Adds the specified fixtures.
        /// </summary>
        /// <param name="fixtures">The fixtures to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtures"/> is <c>null</c>.
        /// </exception>
        public void AddRange(IEnumerable<IFixture> fixtures) => Fixtures.AddRange(fixtures.RequireNonNull(nameof(fixtures)));

        /// <summary>
        /// Ensures a parent.
        /// </summary>
        /// <returns>The instance of the <see cref="IFixture"/>.</returns>
        protected override IFixture EnsureParent()
        {
            Fixtures.ForEach(fixture =>
            {
                fixture.ParentFixture = fixture.FixtureDescriptor.IsContainerFixture ? this : ParentFixture;
                fixture.EnsureParent();
            });
            return base.EnsureParent();
        }

        /// <summary>
        /// Readies a fixture state.
        /// </summary>
        protected override void Ready()
        {
            base.Ready();
            Fixtures.ForEach(fixture => fixture.Ready());
        }

        /// <summary>
        /// Gets a value that indicates whether to be able to run a fixture.
        /// </summary>
        /// <param name="filter">
        /// The filter that determines whether to run a fixture.
        /// </param>
        /// <returns>
        /// <c>true</c> if a fixture can be run; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanRun(IFixtureFilter filter)
            => base.CanRun(filter) || Fixtures.Any(fixture => fixture.CanRun(filter));

        /// <summary>
        /// Runs a fixture with the specified start time, filter, step runner factory,
        /// and a value that indicates whether to run a fixture in parallel.
        /// </summary>
        /// <param name="startTime">The start time at which a fixture is run.</param>
        /// <param name="filter">
        /// The filter that determines whether to run a fixture.
        /// </param>
        /// <param name="stepRunnerFactory">
        /// The factory to create a step runner.
        /// </param>
        /// <param name="parallel">
        /// <c>true</c> if a fixture is run in parallel; otherwise, <c>false</c>.
        /// </param>
        /// <returns>
        /// The result of a fixture running if a fixture can be run; otherwise, <c>null</c>.
        /// </returns>
        protected override FixtureResult Run(DateTime startTime, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        {
            var results = Run(Fixtures, filter, stepRunnerFactory, parallel);
            return RecordEndTime(FixtureResult.Of(FixtureDescriptor).StartAt(startTime)).FinishedWith(results);
        }

        /// <summary>
        /// Retrieves around fixture attributes that specify a fixture.
        /// </summary>
        /// <returns>The around fixture attributes that specify a fixture.</returns>
        protected override IEnumerable<AroundFixtureAttribute> RetrieveAroundFixtureAttributes()
            => FixtureInstanceType?.GetCustomAttributes<AroundFixtureAttribute>() ?? Enumerable.Empty<AroundFixtureAttribute>();

        private IEnumerable<FixtureResult> Run(IEnumerable<IFixture> fixtures, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
            => CanRunInSta() ? RunInSta(() => RunCore(fixtures, filter, stepRunnerFactory, parallel)) : RunCore(fixtures, filter, stepRunnerFactory, parallel);

        private IEnumerable<FixtureResult> RunCore(IEnumerable<IFixture> fixtures, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        {
            IEnumerable<FixtureResult> RunFixtures(IFixtureFilter f) => CanRunParallel(parallel)
                ? fixtures.AsParallel()
                    .Select(fixture => fixture.Run(base.CanRun(f) ? null : f, stepRunnerFactory, parallel))
                    .Where(r => r != null)
                    .ToList()
                : fixtures.Select(fixture => fixture.Run(base.CanRun(f) ? null : f, stepRunnerFactory, parallel))
                    .Where(r => r != null)
                    .ToList();

            FixtureDescriptor.Background = RetrieveBackground();
            if (fixtures.Any(fixture => fixture.FixtureDescriptor.IsContainerFixture))
            {
                var disposable = CreateFixtureInstance() as IDisposable;
                if (disposable == null) return RunFixtures(filter);

                using (disposable) return RunFixtures(filter);
            }

            return RunFixtures(filter);
        }

        private bool CanRunParallel(bool parallel) => parallel && FixtureDescriptor.CanRunParallel;
    }
}
