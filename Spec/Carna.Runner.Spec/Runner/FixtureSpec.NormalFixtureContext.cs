// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;

using NSubstitute;

using Carna.Runner.Step;

namespace Carna.Runner
{
    abstract class FixtureSpec_NormalFixtureContext : FixtureSteppable, IDisposable
    {
        protected abstract Type TargetFixtureType { get; } 
        protected abstract string TargetMethodName { get; }
        protected abstract string TargetFixtureDescription { get; }
        protected abstract string TargetMethodDescription { get; }
        protected abstract string TargetMethodFullName { get; }

        private IFixture Fixture { get; }
        private IFixtureFilter Filter { get; }

        private FixtureResult FixtureRunningResult { get; set; }
        private FixtureResult FixtureRunResult { get; set; }
        private FixtureStepResult FixtureStepRunningResult { get; set; }
        private FixtureStepResult FixtureStepRunResult { get; set; }

        public FixtureSpec_NormalFixtureContext()
        {
            Fixture = TestFixtures.CreateFixture(TargetFixtureType, TargetMethodName);

            Fixture.FixtureRunning += (s, e) => FixtureRunningResult = e.Result;
            Fixture.FixtureRun += (s, e) => FixtureRunResult = e.Result;
            Fixture.FixtureStepRunning += (s, e) => FixtureStepRunningResult = e.Result;
            Fixture.FixtureStepRun += (s, e) => FixtureStepRunResult = e.Result;

            Filter = Substitute.For<IFixtureFilter>();

            TestFixtures.RaiseException = false;
            TestFixtures.CalledFixtureMethods.Clear();
        }

        public void Dispose()
        {
            TestFixtures.RaiseException = false;
            TestFixtures.CalledFixtureMethods.Clear();
        }

        [Example("When a filter that is null is specified")]
        protected void Ex01()
        {
            var result = Fixture.Run(null, null);

            Expect("the fixture method should be called", () => TestFixtures.CalledFixtureMethods.Count == 1 && TestFixtures.CalledFixtureMethods.Contains(TargetFixtureType));

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should not be raised", () => FixtureStepRunningResult == null);
            Expect("FixtureStepRun event should not be raised", () => FixtureStepRunResult == null);
        }

        [Example("When a filter that returns true is specified")]
        protected void Ex02()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(true);

            var result = Fixture.Run(Filter, null);

            Expect("the fixture method should be called", () => TestFixtures.CalledFixtureMethods.Count == 1 && TestFixtures.CalledFixtureMethods.Contains(TargetFixtureType));

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should not be raised", () => FixtureStepRunningResult == null);
            Expect("FixtureStepRun event should not be raised", () => FixtureStepRunResult == null);
        }

        [Example("When a filter that returns false is specified")]
        protected void Ex03()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(false);

            var result = Fixture.Run(Filter, null);

            Expect("the fixture method should not be called", () => TestFixtures.CalledFixtureMethods.Count == 0);

            Expect("the result should be null", () => result == null);

            Expect("FixtureRunning event should not be raised", () => FixtureRunningResult == null);
            Expect("FixtureRun event should not be raised", () => FixtureRunResult == null);

            Expect("FixtureStepRunning event should not be raised", () => FixtureStepRunningResult == null);
            Expect("FixtureStepRun event should not be raised", () => FixtureStepRunResult == null);
        }

        [Example("When a test fixture throws an exception")]
        protected void Ex04()
        {
            TestFixtures.RaiseException = true;

            var result = Fixture.Run(null, null);

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the reuslt should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should not be null", () => result.Exception != null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Failed", () => result.Status == FixtureStatus.Failed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the reuslt on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the reuslt on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should not be raised", () => FixtureStepRunningResult == null);
            Expect("FixtureStepRun event should not be raised", () => FixtureStepRunResult == null);
        }
    }
}
