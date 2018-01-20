﻿// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using Carna.Runner.Step;

namespace Carna.Runner
{
    /// <summary>
    /// Provides a basic function of a fixture.
    /// </summary>
    public abstract class FixtureBase : IFixture
    {
        /// <summary>
        /// Occurs when a fixture is ready.
        /// </summary>
        public event FixtureRunEventHandler FixtureReady;

        /// <summary>
        /// Occurs when to start running a fixture.
        /// </summary>
        public event FixtureRunEventHandler FixtureRunning;

        /// <summary>
        /// Occurs when to complete running a fixture.
        /// </summary>
        public event FixtureRunEventHandler FixtureRun;

        /// <summary>
        /// Occurs when to start running a fixture step.
        /// </summary>
        public event FixtureStepRunEventHandler FixtureStepRunning;

        /// <summary>
        /// Occurs when to complete running a fixture step.
        /// </summary>
        public event FixtureStepRunEventHandler FixtureStepRun;

        /// <summary>
        /// Gets a descriptor of a fixture.
        /// </summary>
        protected FixtureDescriptor FixtureDescriptor { get; }

        /// <summary>
        /// Gets or sets a parent fixture.
        /// </summary>
        protected IFixture ParentFixture { get; set; }

        /// <summary>
        /// Gets the parameters in a fixture.
        /// </summary>
        protected IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets a type of an instance of a fixture.
        /// </summary>
        protected Type FixtureInstanceType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified name and attribute that specifies a fixture.
        /// </summary>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(string name, FixtureAttribute attribute)
        {
            FixtureDescriptor = new FixtureDescriptor(name, attribute.RequireNonNull(nameof(attribute)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified type of an instance of a fixture.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(Type fixtureInstanceType) : this(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)), fixtureInstanceType.Name, fixtureInstanceType.FullName, fixtureInstanceType.GetTypeInfo().GetCustomAttribute<FixtureAttribute>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified type of an instance of a fixture and fixture method.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <param name="fixtureMethod">The fixture method.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureMethod"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(Type fixtureInstanceType, MethodInfo fixtureMethod) : this(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)), fixtureMethod.RequireNonNull(nameof(fixtureMethod)), fixtureMethod.GetCustomAttribute<FixtureAttribute>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified type of an instance of a fixture, fixture method
        /// and attribute that specifies a fixture.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <param name="fixtureMethod">The fixture method.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureMethod"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(Type fixtureInstanceType, MethodInfo fixtureMethod, FixtureAttribute attribute) : this(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)), fixtureMethod.RequireNonNull(nameof(fixtureMethod)).Name, $"{fixtureInstanceType.FullName}.{fixtureMethod.Name}", attribute.RequireNonNull(nameof(attribute)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified type of an instance of a fixture, name, full name,
        /// and attribute that specifies a fixture.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <param name="name">The name of a fixture.</param>
        /// <param name="fullName">The full name of a fixture.</param>
        /// <param name="attribute">The attribute that specifies a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attribute"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(Type fixtureInstanceType, string name, string fullName, FixtureAttribute attribute) : this(fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType)), new FixtureDescriptor(name, fullName, attribute.RequireNonNull(nameof(attribute))))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureBase"/> class
        /// with the specified type of an instance of a fixture and a descriptor of a fixture.
        /// </summary>
        /// <param name="fixtureInstanceType">The type of an instance of a fixture.</param>
        /// <param name="fixtureDescriptor">The descriptor of a fixture.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureInstanceType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fixtureDescriptor"/> is <c>null</c>.
        /// </exception>
        protected FixtureBase(Type fixtureInstanceType, FixtureDescriptor fixtureDescriptor)
        {
            FixtureInstanceType = fixtureInstanceType.RequireNonNull(nameof(fixtureInstanceType));
            FixtureDescriptor = fixtureDescriptor.RequireNonNull(nameof(fixtureDescriptor));
        }

        /// <summary>
        /// Ensures a parent.
        /// </summary>
        /// <returns>The instance of the <see cref="IFixture"/>.</returns>
        protected virtual IFixture EnsureParent() => this;

        /// <summary>
        /// Readies a fixture state.
        /// </summary>
        protected virtual void Ready()
            => OnFixtureReady(new FixtureRunEventArgs(FixtureResult.Of(FixtureDescriptor).Ready()));

        /// <summary>
        /// Gets a value that indicates whether to be able to run a fixture.
        /// </summary>
        /// <param name="filter">
        /// The filter that determines whether to run a fixture.
        /// </param>
        /// <returns>
        /// <c>true</c> if a fixture can be run; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanRun(IFixtureFilter filter)
            => filter == null || filter.Accept(FixtureDescriptor);

        /// <summary>
        /// Runs a fixture with the specified filter, step runner factory,
        /// and a value that indicates whether to run a fixture in parallel.
        /// </summary>
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
        protected virtual FixtureResult Run(IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        {
            if (!CanRun(filter)) { return null; }

            var startTime = DateTime.UtcNow;
            OnFixtureRunning(new FixtureRunEventArgs(FixtureResult.Of(FixtureDescriptor).StartAt(startTime).Running()));
            var result = RunCore(startTime, filter, stepRunnerFactory, parallel);
            OnFixtureRun(new FixtureRunEventArgs(result));

            return result;
        }

        private FixtureResult RunCore(DateTime startTime, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        {
            var result = FixtureResult.Of(FixtureDescriptor).StartAt(startTime);
            try
            {
                var aroundFixtureAttributes = RetrieveAroundFixtureAttributes().ToList();
                aroundFixtureAttributes.ForEach(attribute => attribute.OnFixtureRunning(FixtureContext.Of(FixtureDescriptor)));
                try
                {
                    return RunCore(result, filter, stepRunnerFactory, parallel);
                }
                finally
                {
                    aroundFixtureAttributes.ForEach(attribute => attribute.OnFixtureRun(FixtureContext.Of(FixtureDescriptor)));
                }
            }
            catch (Exception exc)
            {
                return RecordEndTime(result).Failed(exc);
            }
        }

        private FixtureResult RunCore(FixtureResult.Builder result, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
        {
            try
            {
                return Run(result.StartTime.Value, filter, stepRunnerFactory, parallel);
            }
            catch (Exception exc)
            {
                return RecordEndTime(result).Failed(exc);
            }
        }

        /// <summary>
        /// Retrieves around fixture attributes that specify a fixture.
        /// </summary>
        /// <returns>The around fixture attributes that specify a fixture.</returns>
        protected virtual IEnumerable<AroundFixtureAttribure> RetrieveAroundFixtureAttributes() => Enumerable.Empty<AroundFixtureAttribure>();

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
        protected abstract FixtureResult Run(DateTime startTime, IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel);

        /// <summary>
        /// Creates a new instance of the <see cref="FixtureInstanceType"/>.
        /// </summary>
        /// <returns>The new instance of the <see cref="FixtureInstanceType"/>.</returns>
        protected virtual object CreateFixtureInstance()
        {
            if (FixtureInstanceType == null) { return null; }

            var constructor = GetConstructor();
            if (constructor == null) { return EnsureParameters(Activator.CreateInstance(FixtureInstanceType)); }

            var constructorParameters = constructor.GetParameters();
            var parameterValues = new object[constructorParameters.Length];
            Enumerable.Range(0, constructorParameters.Length).ForEach(index =>
            {
                var parameterName = constructorParameters[index].Name;
                if (ParentFixture.Parameters.ContainsKey(parameterName))
                {
                    parameterValues[index] = ParentFixture.Parameters[parameterName];
                }
            });

            return EnsureParameters(constructor.Invoke(parameterValues));
        }

        /// <summary>
        /// Gets a constructor with parameters.
        /// </summary>
        /// <returns>
        /// The constructor with parameters. If parameters do not exist or the constructor
        /// whose parameters do not match, returns <c>null</c>.
        /// </returns>
        protected virtual ConstructorInfo GetConstructor()
        {
            if (FixtureInstanceType == null) { return null; }
            if (ParentFixture == null || ParentFixture.Parameters.IsEmpty()) { return null; }

            return FixtureInstanceType.GetTypeInfo().DeclaredConstructors
                .Where(c => ParentFixture.Parameters.Keys.Any(k => c.GetParameters().Any(p => p.Name == k)))
                .FirstOrDefault();
        }

        private ConstructorInfo GetDefaultConstructor()
            => FixtureInstanceType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => c.GetParameters().Length == 0);

        /// <summary>
        /// Gets background of a fixture.
        /// </summary>
        /// <returns>The background of a fixture.</returns>
        protected virtual string RetrieveBackground()
        {
            if (FixtureInstanceType == null) { return string.Empty; }

            return string.Join(
                Environment.NewLine,
                RetrieveBackgroundAttributes(GetConstructor() ?? GetDefaultConstructor())
                    .SelectMany(background => background.Description.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            );
        }

        /// <summary>
        /// Retrieves background attributes that specify a fixture that has the specified constructor.
        /// </summary>
        /// <param name="constructor">The constructor of a fixture.</param>
        /// <returns>The background attributes that specify a fixture that has the specified constructor.</returns>
        protected virtual List<BackgroundAttribute> RetrieveBackgroundAttributes(MethodBase constructor)
        {
            var backgroundList = new List<BackgroundAttribute>();
            if (constructor == null || constructor.DeclaringType == typeof(object)) { return backgroundList; }

            try
            {
                backgroundList.AddRange(RetrieveBackgroundAttributes(
                    constructor.Module.ResolveMethod(
                        BitConverter.ToInt32(
                            constructor.GetMethodBody()
                                .GetILAsByteArray()
                                .SkipWhile(b => b != OpCodes.Call.Value)
                                .ToArray(),
                            1
                        )
                    )
                ));
            }
            catch { }

            backgroundList.AddRange(constructor.GetCustomAttributes<BackgroundAttribute>().ToList());
            return backgroundList;
        }

        /// <summary>
        /// Ensures parameters in the specified instance of a fixtrue.
        /// </summary>
        /// <param name="fixtureInstance">The instance of a fixture that has parameters.</param>
        /// <returns>The instance of a fixture that ensures parameters.</returns>
        protected object EnsureParameters(object fixtureInstance)
        {
            fixtureInstance.GetType().GetRuntimeFields()
                .Select(f => new { FieldInfo = f, Attribute = f.GetCustomAttribute<ParameterAttribute>() })
                .Where(f => f.Attribute != null)
                .ForEach(f => Parameters[f.Attribute.Name ?? f.FieldInfo.Name] = f.FieldInfo.GetValue(fixtureInstance));
            fixtureInstance.GetType().GetRuntimeProperties()
                .Select(p => new { PropertyInfo = p, Attribute = p.GetCustomAttribute<ParameterAttribute>() })
                .Where(p => p.Attribute != null)
                .ForEach(p => Parameters[p.Attribute.Name ?? p.PropertyInfo.Name] = p.PropertyInfo.GetValue(fixtureInstance));
            fixtureInstance.GetType().GetRuntimeMethods()
                .Select(m => new { MethodInfo = m, Attribute = m.GetCustomAttribute<ParameterAttribute>() })
                .Where(m => m.Attribute != null)
                .ForEach(m => Parameters[m.Attribute.Name ?? m.MethodInfo.Name] = m.MethodInfo.Invoke(fixtureInstance, null));
            return fixtureInstance;
        }

        /// <summary>
        /// Records an end time to the specified fixture result.
        /// </summary>
        /// <param name="result">The fixture result to record an end time.</param>
        /// <returns>The fixture result in which an end time is recorded.</returns>
        protected FixtureResult.Builder RecordEndTime(FixtureResult.Builder result)
            => result.EndAt(DateTime.UtcNow);

        /// <summary>
        /// Raises the <see cref="FixtureReady"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureReady(FixtureRunEventArgs e) => FixtureReady?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureRunning"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureRunning(FixtureRunEventArgs e) => FixtureRunning?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureRun"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureRun(FixtureRunEventArgs e) => FixtureRun?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureStepRunning"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureStepRunning(FixtureStepRunEventArgs e) => FixtureStepRunning?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="FixtureStepRun"/> event with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnFixtureStepRun(FixtureStepRunEventArgs e) => FixtureStepRun?.Invoke(this, e);

        FixtureDescriptor IFixture.FixtureDescriptor => FixtureDescriptor;
        IFixture IFixture.ParentFixture
        {
            get { return ParentFixture; }
            set { ParentFixture = value; }
        }
        IDictionary<string, object> IFixture.Parameters => Parameters;

        IFixture IFixture.EnsureParent() => EnsureParent();
        void IFixture.Ready() => Ready();
        bool IFixture.CanRun(IFixtureFilter filter) => CanRun(filter);
        FixtureResult IFixture.Run(IFixtureFilter filter, IFixtureStepRunnerFactory stepRunnerFactory, bool parallel)
            => Run(filter, stepRunnerFactory, parallel);
    }
}
