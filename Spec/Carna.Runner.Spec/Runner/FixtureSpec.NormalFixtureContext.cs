﻿// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

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

        IFixture Fixture { get; }
        IFixtureFilter Filter { get; }

        FixtureResult FixtureRunningResult { get; set; }
        FixtureResult FixtureRunResult { get; set; }
        FixtureStepResult FixtureStepRunningResult { get; set; }
        FixtureStepResult FixtureStepRunResult { get; set; }

        FixtureDescriptorAssertion ExpectedFixtureDescriptor { get; set; }
        FixtureResultAssertion ExpectedFixtureResult { get; set; }
        FixtureDescriptorAssertion ExpectedFixtureRunningDescriptor { get; set; }
        FixtureResultAssertion ExpectedFixtureRunningResult { get; set; }

        protected FixtureSpec_NormalFixtureContext()
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

            ExpectedFixtureDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 0, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

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

            ExpectedFixtureDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNullException(true, true, true, 0, 0, FixtureStatus.Passed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

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

            ExpectedFixtureDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result should be as follows:{ExpectedFixtureDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(result.FixtureDescriptor) == ExpectedFixtureDescriptor);
            ExpectedFixtureResult = FixtureResultAssertion.ForNotNullException(true, true, true, 0, 0, FixtureStatus.Failed);
            Expect($"the result should be as follows:{ExpectedFixtureResult.ToDescription()}", () => FixtureResultAssertion.Of(result) == ExpectedFixtureResult);

            Expect("FixtureRunning event should be raised", () => FixtureRunningResult != null);

            ExpectedFixtureRunningDescriptor = FixtureDescriptorAssertion.Of(TargetFixtureDescription, TargetMethodDescription, TargetMethodFullName, typeof(ExampleAttribute));
            Expect($"the descriptor of the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningDescriptor.ToDescription()}", () => FixtureDescriptorAssertion.Of(FixtureRunningResult.FixtureDescriptor) == ExpectedFixtureRunningDescriptor);
            ExpectedFixtureRunningResult = FixtureResultAssertion.ForNullException(true, false, false, 0, 0, FixtureStatus.Running);
            Expect($"the result on FixtureRunning event should be as follows:{ExpectedFixtureRunningResult.ToDescription()}", () => FixtureResultAssertion.Of(FixtureRunningResult) == ExpectedFixtureRunningResult);

            Expect("FixtureRun event should be raised", () => FixtureRunResult != null);

            Expect("the result on FixtureRun event should be the result that is returned by Run method", () => FixtureRunResult == result);

            Expect("FixtureStepRunning event should not be raised", () => FixtureStepRunningResult == null);
            Expect("FixtureStepRun event should not be raised", () => FixtureStepRunResult == null);
        }
    }
}
