// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;
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
            [Background("Simple Fixture Background")]
            public SimpleFixture() { }

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

        [Context("Simple Fixture with some methods")]
        public class SimpleFixtureWithSomeMethods
        {
            [Example("Fixture Method Example #1")]
            void FixtureMethod1()
            {
            }

            [Example("Fixture Method Example #2")]
            void FixtureMethod2()
            {
            }

            [Example("Fixture Method Example #3")]
            void FixtureMethod3()
            {
            }
        }

        [Requirement("Simple Fixture with container fixture as a nested class")]
        public class SimpleFixtureWithContainerFixtureAsNestedClass
        {
            [Context("Context #1")]
            class Context1
            {
                [Example("Fixture Method Example")]
                void FixtureMethod()
                {
                }
            }
        }

        [Requirement("Simple Fixture with container fixture as a property")]
        public class SimpleFixtureWithContainerFixtureAsProperty
        {
            [Context("Context #1")]
            SimpleFixture Context01 { get; }
        }

        [Requirement("Simple Fixture with container fixture as a field")]
        public class SimpleFixtureWithContainerFixtureAsField
        {
            [Context("Context #1")]
            SimpleFixture context01;
        }

        [Context("Simple Fixture with sample")]
        public class SimpleFixtureWithSample
        {
            [Example("Fixture Method with a sample Example")]
            [Sample(1, "parameter", true)]
            void FixtureMethodWithSample(int parameter1, string parameter2, bool parameter3)
            {
            }
        }

        [Context("Simple Fixture with some samples")]
        public class SimpleFixtureWithSamples
        {
            [Example("Fixture Method with some samples Example")]
            [Sample(1, "parameter1", true, Description = "Sample #1")]
            [Sample(2, "parameter2", false, Description = "Sample #2")]
            [Sample(3, "parameter3", true, Description = "Sample #3")]
            void FixtureMethodWithSamples(int parameter1, string parameter2, bool parameter3)
            {
            }
        }

        [Context("Simple Fixture with sample data source")]
        public class SimpleFixtureWithSampleDataSource
        {
            [Example("Fixture Method with sample data source Example")]
            [Sample(Source = typeof(SimpleSampleDataSource), Description = "Sample #1")]
            void FixtureMethodWithSampleDataSource(int parameter1, string parameter2, bool parameter3)
            {
            }
        }

        [Context("Simple Fixture with Invalid SampleDataSource that does not implement ISampleDataSource")]
        public class SimpleFixtureWithInvalidSampleDataSource
        {
            [Example]
            [Sample(Source = typeof(object))]
            void Ex01()
            {
            }
        }

        [Context("Simple Fixture with Invalid SampleDataSource that does not have a parameter less constructor")]
        public class SimpleFixtureWithSampleDataSourceWithoutParameterlessConstructor
        {
            [Example]
            [Sample(Source = typeof(SampleDataSourceWithoutParameterlessConstructor))]
            void Ex01()
            {
            }
        }

        public class SimpleSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new { Parameter1 = 1, Parameter2 = "parameter1", Parameter3 = true, Description = "Sample #1" };
                yield return new { Parameter1 = 2, Parameter2 = "parameter2", Parameter3 = false };
                yield return new { Parameter1 = 3, Parameter2 = "parameter3", Parameter3 = true, Description = "Sample #3" };
            }
        }

        public class SampleDataSourceWithoutParameterlessConstructor : ISampleDataSource
        {
            public SampleDataSourceWithoutParameterlessConstructor(int parameter)
            {
            }

            IEnumerable ISampleDataSource.GetData() => null;
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
