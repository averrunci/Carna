// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;

using NSubstitute;

using Carna.Runner.Step;
using Carna.Step;

namespace Carna.Runner
{
    abstract class FixtureSpec_FixtureImplementedIFixtureSteppableContext : FixtureSteppable, IDisposable
    {
        protected abstract Type TargetFixtureType { get; }
        protected abstract string TargetMethodName { get; }
        protected abstract string TargetFixtureDescription { get; }
        protected abstract string TargetMethodDescription { get; }
        protected abstract string TargetMethodFullName { get; }

        IFixture Fixture { get; }
        IFixtureFilter Filter { get; }

        FixtureResult FixtureRunningResult { get; set; }
        FixtureResult FixtureRunResult { get; set; }
        FixtureStepResult FixtureStepRunningResult { get; set; }
        FixtureStepResult FixtureStepRunResult { get; set; }

        protected FixtureSpec_FixtureImplementedIFixtureSteppableContext()
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
            var result = Fixture.Run(null, new TestFixtures.SimpleFixtureStepRunnerFactory());

            Expect("the fixture method should be called", () => TestFixtures.CalledFixtureMethods.Count == 1 && TestFixtures.CalledFixtureMethods.Contains(TargetFixtureType));

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the result should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the step count of the result should be 1", () => result.StepResults.Count() == 1);

            var stepResult = result.StepResults.ElementAt(0);
            Expect("the description of the step of the result should be the specified description", () => stepResult.Step.Description == "Description");
            Expect("the exception of the step of the result should be null", () => stepResult.Exception == null);
            Expect("the status of the step of the result should be Passed", () => stepResult.Status == FixtureStepStatus.Passed);
            Expect("the type of the step of the result should be Expect", () => stepResult.Step.GetType() == typeof(ExpectStep));
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should be raised", () => FixtureStepRunningResult != null);

            Expect("the step of the result on FixtureStepRunning event should be the step of the result that is returned by Run method", () => FixtureStepRunningResult.Step == stepResult.Step);
            Expect("the start time of the result on FixtureStepRunning event should have value", () => FixtureStepRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureStepRunning event should not have value", () => !FixtureStepRunningResult.EndTime.HasValue);
            Expect("the duration of the result on FixtureStepRunning event should not have value", () => !FixtureStepRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureStepRunning event should be null", () => FixtureStepRunningResult.Exception == null);
            Expect("the status of the result on FixtureStepRunning event should be Running", () => FixtureStepRunningResult.Status == FixtureStepStatus.Running);

            Expect("FixtureStepRun event should be raised", () => FixtureStepRunResult != null);

            Expect("the result on FixtureStepRun event should be the result that is returned by Run method", () => FixtureStepRunResult == stepResult);
        }

        [Example("When a filter that returns true is specified")]
        protected void Ex02()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(true);

            var result = Fixture.Run(Filter, new TestFixtures.SimpleFixtureStepRunnerFactory());

            Expect("the fixture method should be called", () => TestFixtures.CalledFixtureMethods.Count == 1 && TestFixtures.CalledFixtureMethods.Contains(TargetFixtureType));

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the result should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should be null", () => result.Exception == null);
            Expect("the step count of the result should be 1", () => result.StepResults.Count() == 1);

            var stepResult = result.StepResults.ElementAt(0);
            Expect("the description of the step of the result should be the specified description", () => stepResult.Step.Description == "Description");
            Expect("the exception of the step of the result should be null", () => stepResult.Exception == null);
            Expect("the status of the step of the result should be Passed", () => stepResult.Status == FixtureStepStatus.Passed);
            Expect("the type of the step of the result should be Expect", () => stepResult.Step.GetType() == typeof(ExpectStep));
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Passed", () => result.Status == FixtureStatus.Passed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should be raised", () => FixtureStepRunningResult != null);

            Expect("the step of the result on FixtureStepRunning event should be the step of the result that is returned by Run method", () => FixtureStepRunningResult.Step == stepResult.Step);
            Expect("the start time of the result on FixtureStepRunning event should have value", () => FixtureStepRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureStepRunning event should not have value", () => !FixtureStepRunningResult.EndTime.HasValue);
            Expect("the duration of the result on FixtureStepRunning event should not have value", () => !FixtureStepRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureStepRunning event should be null", () => FixtureStepRunningResult.Exception == null);
            Expect("the status of the result on FixtureStepRunning event should be Running", () => FixtureStepRunningResult.Status == FixtureStepStatus.Running);

            Expect("FixtureStepRun event should be raised", () => FixtureStepRunResult != null);

            Expect("the result on FixtureStepRun event should be the result that is returned by Run method", () => FixtureStepRunResult == stepResult);
        }

        [Example("When a filter that returns false is specified")]
        protected void Ex03()
        {
            Filter.Accept(Arg.Any<FixtureDescriptor>()).Returns(false);

            var result = Fixture.Run(Filter, new TestFixtures.SimpleFixtureStepRunnerFactory());

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

            var result = Fixture.Run(null, new TestFixtures.SimpleFixtureStepRunnerFactory());

            Expect("the description of the descriptor of the result should be the value specified to the fixture method", () => result.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result should be the name of the fixture method", () => result.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result should be the full name of the fixture method", () => result.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result should be ExampleAttribute", () => result.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result should have value", () => result.StartTime.HasValue);
            Expect("the end time of the result should have value", () => result.EndTime.HasValue);
            Expect("the duration of the result should have value", () => result.Duration.HasValue);
            Expect("the exception of the result should not be null", () => result.Exception != null);
            Expect("the steps of the result should be empty", () => !result.StepResults.Any());
            Expect("the results of the result should be empty", () => !result.Results.Any());
            Expect("the status of the result should be Failed", () => result.Status == FixtureStatus.Failed);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            Expect("the description of the descriptor of the result on FixtureRunning event should be the value specified to the fixture method", () => FixtureRunningResult.FixtureDescriptor.Description == TargetFixtureDescription);
            Expect("the name of the descriptor of the result on FixtureRunning event should be the name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.Name == TargetMethodDescription);
            Expect("the full name of the descriptor of the result on FixtureRunning event should be the full name of the fixture method", () => FixtureRunningResult.FixtureDescriptor.FullName == TargetMethodFullName);
            Expect("the fixture attribute type of the descriptor of the result on FixtureRunning event should be ExampleAttribute", () => FixtureRunningResult.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
            Expect("the start time of the result on FixtureRunning event should have value", () => FixtureRunningResult.StartTime.HasValue);
            Expect("the end time of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.EndTime.HasValue);
            Expect("the duration of the result on FixtureRunning event should not have value", () => !FixtureRunningResult.Duration.HasValue);
            Expect("the exception of the result on FixtureRunning event should be null", () => FixtureRunningResult.Exception == null);
            Expect("the steps of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.StepResults.Any());
            Expect("the results of the result on FixtureRunning event should be empty", () => !FixtureRunningResult.Results.Any());
            Expect("the status of the result on FixtureRunning event should be Running", () => FixtureRunningResult.Status == FixtureStatus.Running);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);
        }
    }
}
