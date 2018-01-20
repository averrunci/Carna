// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;

using NSubstitute;

using Carna.Runner.Step;

namespace Carna.Runner
{
    [Context("Runs fixtures")]
    class FixtureContainerSpec_RunFixtures : FixtureSteppable, IDisposable
    {
        private IFixture Container { get; }
        private IFixtureFilter Filter { get; }

        private FixtureResult FixtureRunningResult;
        private FixtureResult FixtureRunResult;

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

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the background of the descriptor of the result should be the value specified to the fixture constructor", () => string.Join(Environment.NewLine, result.FixtureDescriptor.Background) == "Simple Fixture Background");
            Expect("the fixture attribute type of the descriptor of the reuslt should be ContextAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the result count of the result should be 3", () => result.Results.Count() == 3);
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ContextAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

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

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the background of the descriptor of the result should be the value specified to the fixture constructor", () => string.Join(Environment.NewLine, result.FixtureDescriptor.Background) == "Simple Fixture Background");
            Expect("the fixture attribute type of the descriptor of the reuslt should be ContextAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the result count of the result should be 3", () => result.Results.Count() == 3);
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ContextAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

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

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the background of the descriptor of the result should be the value specified to the fixture constructor", () => string.Join(Environment.NewLine, result.FixtureDescriptor.Background) == "Simple Fixture Background");
            Expect("the fixture attribute type of the descriptor of the reuslt should be ContextAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the result count of the result should be 1", () => result.Results.Count() == 1);
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ContextAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

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

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the background of the descriptor of the result should be the value specified to the fixture constructor", () => string.Join(Environment.NewLine, result.FixtureDescriptor.Background) == "Simple Fixture Background");
            Expect("the fixture attribute type of the descriptor of the reuslt should be ContextAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the result count of the result should be 3", () => result.Results.Count() == 3);
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == "Simple Fixture");
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == "SimpleFixture");
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == "Carna.TestFixtures+SimpleFixture");
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ContextAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

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
