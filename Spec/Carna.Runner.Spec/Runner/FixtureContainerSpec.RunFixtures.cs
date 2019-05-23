// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

using NSubstitute;

using Carna.Runner.Step;

namespace Carna.Runner
{
    [Context("Runs fixtures")]
    class FixtureContainerSpec_RunFixtures : FixtureSteppable, IDisposable
    {
        IFixture Container { get; }
        IFixtureFilter Filter { get; }

        FixtureResult FixtureRunningResult { get; set; }
        FixtureResult FixtureRunResult { get; set; }

        FixtureDescriptorWithBackgroundAssertion ExpectedFixtureDescriptor { get; set; }
        FixtureResultAssertion ExpectedFixtureResult { get; set; }

        FixtureDescriptorAssertion ExpectedFixtureRunningDescriptor { get; set; }
        FixtureResultAssertion ExpectedFixtureRunningResult { get; set; }

        [Background("Fixture container that has SimpleFixture, SimpleDisposableFixture, SimpleFixtureSteppable")]
        public FixtureContainerSpec_RunFixtures()
        {
            Container = new FixtureContainer(typeof(TestFixtures.SimpleFixture));

            Container.FixtureRunning += (s, e) => FixtureRunningResult = e.Result;
            Container.FixtureRun += (s, e) => FixtureRunResult = e.Result;

            ((FixtureContainer)Container).AddRange(new[] {
                TestFixtures.CreateFixture<TestFixtures.SimpleFixture>("FixtureMethod"),
                TestFixtures.CreateFixture<TestFixtures.SimpleDisposableFixture>("FixtureMethod"),
                TestFixtures.CreateFixture<TestFixtures.SimpleFixtureSteppable>("FixtureMethod")
            });

            Filter = Substitute.For<IFixtureFilter>();

            TestFixtures.CalledFixtureMethods.Clear();
        }

        public void Dispose()
        {
            TestFixtures.CalledFixtureMethods.Clear();
        }

        [Example("When a filter that is null is specified")]
        void Ex01()
        {
            var result = Container.Run(null, new FixtureStepRunnerFactory());

            Expect(
                "all inner fixture methods should be called",
                () => TestFixtures.CalledFixtureMethods.Count == 3 &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleDisposableFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixtureSteppable))
            );

            ExpectedFixtureDescriptor = FixtureDescriptorWithBackgroundAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute), "Simple Fixture Background");
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorWithBackgroundAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 3, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute));
            Expect($"the descriptor of the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);
        }

        [Example("When a filter that returns true is specified")]
        void Ex02()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(true);

            var result = Container.Run(Filter, new FixtureStepRunnerFactory());

            Expect(
                "all inner fixture methods should be called",
                () => TestFixtures.CalledFixtureMethods.Count == 3 &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleDisposableFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixtureSteppable))
            );

            ExpectedFixtureDescriptor = FixtureDescriptorWithBackgroundAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute), "Simple Fixture Background");
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorWithBackgroundAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 3, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute));
            Expect($"the descriptor of the result on FixtureRunning should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);
        }

        [Example("When a filter that returns false is specified")]
        void Ex03()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(false);
            
            var result = Container.Run(Filter, new FixtureStepRunnerFactory());

            Expect("all inner fixture methods should not be called", () => TestFixtures.CalledFixtureMethods.Count == 0);

            Expect("the result should be null", () => result == null);

            Expect("FixtureRunning event should not be raised", () => FixtureRunningResult == null);

            Expect("FixtureRun event should not be raised", () => FixtureRunResult == null);
        }

        [Example("When a filter that returns false in the container fixture and returns true in any inner fixtures is specified")]
        void Ex04()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(x => x.Arg<FixtureDescriptor>().FullName == "Carna.TestFixtures+SimpleDisposableFixture.FixtureMethod");

            var result = Container.Run(Filter, new FixtureStepRunnerFactory());

            Expect(
                "the inner fixture method that matches the specified filter should be called",
                () => TestFixtures.CalledFixtureMethods.Count == 1 && TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleDisposableFixture))
            );

            ExpectedFixtureDescriptor = FixtureDescriptorWithBackgroundAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute), "Simple Fixture Background");
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorWithBackgroundAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 1, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute));
            Expect($"the descriptor of the result on FixtureRunning should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);
        }

        [Example("When a filter that returns true in the container fixture and returns false in any inner fixtures is specified")]
        void Ex05()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(x => x.Arg<FixtureDescriptor>().FullName == "Carna.TestFixtures+SimpleFixture");

            var result = Container.Run(Filter, new FixtureStepRunnerFactory());

            Expect(
                "all inner fixture methods should be called",
                () => TestFixtures.CalledFixtureMethods.Count == 3 &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleDisposableFixture)) &&
                    TestFixtures.CalledFixtureMethods.Contains(typeof(TestFixtures.SimpleFixtureSteppable))
            );

            ExpectedFixtureDescriptor = FixtureDescriptorWithBackgroundAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute), "Simple Fixture Background");
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorWithBackgroundAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 3, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of("Simple Fixture", "SimpleFixture", "Carna.TestFixtures+SimpleFixture", typeof(ContextAttribute));
            Expect($"the descriptor of the result on FixtureRunning should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);
        }

        void Assert(int expectedCount)
        {
            Expect("to execute before running a fixture", () => TestAroundFixtureAttribute.OnFixtureRunningCount.Value == expectedCount);
            Expect("to execute after running a fixture", () => TestAroundFixtureAttribute.OnFixtureRunCount.Value == expectedCount);
        }

        [Example("When a fixture is specified by one AroundAttribute")]
        void Ex06()
        {
            TestAroundFixtureAttribute.OnFixtureRunningCount.Value = 0;
            TestAroundFixtureAttribute.OnFixtureRunCount.Value = 0;

            var fixture = TestFixtures.CreateFixtureWithContainer<TestFixtures.FixtureSpecifiedByOneAroundFixtureAttribute>("Ex01");
            fixture.Run(null, null);
            Assert(1);
        }

        [Example("When a fixture is specified by some AroundAttributes")]
        void Ex07()
        {
            TestAroundFixtureAttribute.OnFixtureRunningCount.Value = 0;
            TestAroundFixtureAttribute.OnFixtureRunCount.Value = 0;

            var fixture = TestFixtures.CreateFixtureWithContainer<TestFixtures.FixtureSpecifiedBySomeAroundFixtureAttributes>("Ex01");
            fixture.Run(null, null);
            Assert(3);
        }
    }
}
