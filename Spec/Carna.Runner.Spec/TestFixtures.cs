// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Carna.Runner;
using Carna.Runner.Step;
using Carna.Step;

namespace Carna
{
    internal static class TestFixtures
    {
        public static IFixture CreateFixture<T>(string methodName) => CreateFixture(typeof(T), methodName);

        public static IFixture CreateFixture(Type fixtureInstanceType, string methodName) =>
            new Fixture(fixtureInstanceType, fixtureInstanceType.GetRuntimeMethods().Where(m => m.Name == methodName).First());

        public static bool RaiseException
        {
            get { return raiseException.Value; }
            set { raiseException.Value = value; }
        }
        private static ThreadLocal<bool> raiseException = new ThreadLocal<bool>();

        public static bool DisposeCalled
        {
            get { return disposeCalled.Value; }
            set { disposeCalled.Value = value; }
        }
        private static ThreadLocal<bool> disposeCalled = new ThreadLocal<bool>();

        public static HashSet<Type> CalledFixtureMethods
        {
            get
            {
                if (calledFixtureMethods.Value == null)
                {
                    calledFixtureMethods.Value = new HashSet<Type>();
                }
                return calledFixtureMethods.Value;
            }
        }
        private static ThreadLocal<HashSet<Type>> calledFixtureMethods = new ThreadLocal<HashSet<Type>>();

        [Context("Simple Fixture")]
        public class SimpleFixture
        {
            [Example("Fixture Method Example")]
            void FixtureMethod()
            {
                if (RaiseException) { throw new Exception(); }

                CalledFixtureMethods.Add(GetType());
            }
        }

        [Context("Simple Asynchronous Fixture")]
        public class SimpleAsyncFixture
        {
            [Example("Fixture Asynchronous Method Example")]
            async Task FixtureMethodAsync()
            {
                var raiseException = RaiseException;
                var calledFixtureMethods = CalledFixtureMethods;

                await Task.Delay(100);

                if (raiseException) { throw new Exception(); }

                calledFixtureMethods.Add(GetType());
            }
        }

        [Context("Simple Disposable Fixture")]
        public class SimpleDisposableFixture : IDisposable
        {
            public void Dispose()
            {
                DisposeCalled = true;
            }

            [Example("Fixture Method Example")]
            void FixtureMethod()
            {
                if (RaiseException) { throw new Exception(); }

                CalledFixtureMethods.Add(GetType());
            }
        }

        [Context("Simple Disposable Asynchronous Fixture")]
        public class SimpleDisposableAsyncFixture : IDisposable
        {
            public void Dispose()
            {
                DisposeCalled = true;
            }

            [Example("Fixture Asynchronous Method Example")]
            async Task FixtureMethodAsync()
            {
                var raiseException = RaiseException;
                var calledFixtureMethods = CalledFixtureMethods;

                await Task.Delay(100);

                if (raiseException) { throw new Exception(); }

                calledFixtureMethods.Add(GetType());
            }
        }

        [Context("Simple Fixture Steppable")]
        public class SimpleFixtureSteppable : IFixtureSteppable
        {
            public IFixtureStepper Stepper { get; set; }

            [Example("Fixture Method Example")]
            void FixtureMethod()
            {
                if (RaiseException) { throw new Exception(); }

                CalledFixtureMethods.Add(GetType());

                Stepper.Take(new ExpectStep("Description", GetType(), string.Empty, string.Empty, 0));
            }
        }

        [Context("Simple Asynchronous Fixture Steppable")]
        public class SimpleAsyncFixtureSteppable : IFixtureSteppable
        {
            public IFixtureStepper Stepper { get; set; }

            [Example("Fixture Asynchronous Method Example")]
            async Task FixtureMethodAsync()
            {
                var raiseException = RaiseException;
                var calledFixtureMethods = CalledFixtureMethods;

                await Task.Delay(100);

                if (raiseException) { throw new Exception(); }

                calledFixtureMethods.Add(GetType());

                Stepper.Take(new ExpectStep("Description", GetType(), string.Empty, string.Empty, 0));
            }
        }

        public class SimpleFixtureStepRunnerFactory : IFixtureStepRunnerFactory
        {
            public IFixtureStepRunner Create(FixtureStep step)
            {
                return new SimpleFixtureStepRunner(step);
            }

            public void RegisterFrom(IEnumerable<Assembly> assemblies)
            {
            }
        }

        public class SimpleFixtureStepRunner : IFixtureStepRunner
        {
            private FixtureStep Step { get; }

            public SimpleFixtureStepRunner(FixtureStep step)
            {
                Step = step;
            }

            public FixtureStepResult.Builder Run(FixtureStepResultCollection results)
            {
                return FixtureStepResult.Of(Step).Passed();
            }
        }
    }
}
