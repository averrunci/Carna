// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Respresents a collection of the <see cref="FixtureStepResult"/>.
    /// </summary>
    public class FixtureStepResultCollection : IEnumerable<FixtureStepResult>
    {
        /// <summary>
        /// Gets results of the containing <see cref="FixtureStepResult"/>.
        /// </summary>
        protected ICollection<FixtureStepResult> Results { get; } = new Collection<FixtureStepResult>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureStepResultCollection"/> class.
        /// </summary>
        public FixtureStepResultCollection()
        {
        }

        /// <summary>
        /// Adds the specified result of a fixture step running.
        /// </summary>
        /// <param name="result">The result of a fixture step running to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="result"/> is <c>null</c>.
        /// </exception>
        public void Add(FixtureStepResult result) => Results.Add(result.RequireNonNull(nameof(result)));

        /// <summary>
        /// Gets a value that indicates whether to have the result of the specified fixture step types.
        /// </summary>
        /// <param name="fixtureStepTypes">The types of the fixture step.</param>
        /// <returns>
        /// <c>true</c> if the result of the specified fixture step types is contained; otherwise, <c>false</c>.
        /// </returns>
        public bool Has(params Type[] fixtureStepTypes) => Results.Any(result => fixtureStepTypes.Contains(result.Step.GetType()));

        /// <summary>
        /// Gets a value that indicates whether to have the result of the specified fixture step type that has an exception.
        /// </summary>
        /// <typeparam name="T">The type of the fixture step.</typeparam>
        /// <returns>
        /// <c>true</c> if the result of the specified fixture step type that has an exception is contained; otherwise, <c>false</c>.
        /// </returns>
        public bool HasExceptionAt<T>() where T : FixtureStep
            => Results.Where(result => typeof(T).Equals(result.Step.GetType()))
                .Any(result => result.Exception != null);

        /// <summary>
        /// Gets the latest fixture step results of the specified fixture step type.
        /// </summary>
        /// <typeparam name="T">The type of the fixture step.</typeparam>
        /// <returns>
        /// The latest fixture step results of the specified fixture step type.
        /// </returns>
        public IEnumerable<FixtureStepResult> GetLatestStepResultsOf<T>() where T : FixtureStep
            => Results.Reverse()
                .SkipWhile(result => result.Step.GetType() != typeof(T))
                .TakeWhile(result => result.Step.GetType() == typeof(T));

        /// <summary>
        /// Gets a value that indicates whether the latest results of the specified fixture step type have an exception.
        /// </summary>
        /// <typeparam name="T">The type of the fixture step.</typeparam>
        /// <returns>
        /// <c>true</c> if the latest results of the specified fixture step have an exception; otherwise, <c>false</c>.
        /// </returns>
        public bool HasLatestExceptionAt<T>() where T : FixtureStep
            => GetLatestStepResultsOf<T>().Any(result => result.Exception != null);

        /// <summary>
        /// Gets the latest exception that the result of the specified fixture step type has.
        /// </summary>
        /// <typeparam name="T">The type of the fixture step.</typeparam>
        /// <returns>
        /// The latest exception that the result of the specified fixture step type has,
        /// or <c>null</c> if the results of the specified fixture step do not have any exceptions.
        /// </returns>
        public Exception GetLatestExceptionAt<T>() where T : FixtureStep
            => GetLatestStepResultsOf<T>().FirstOrDefault(result => result.Exception != null)?.Exception;

        /// <summary>
        /// Clears an exception of the result that has the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be cleared.</param>
        public void ClearException(Exception exception)
        {
            if (exception == null) { return; }

            Results.Where(result => exception.Equals(result.Exception))
                .ForEach(result => result.ClearException());
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="FixtureStepResultCollection"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> for the <see cref="FixtureStepResultCollection"/>.</returns>
        public IEnumerator<FixtureStepResult> GetEnumerator() => Results.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
