// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Provides extension methods of the <see cref="FixtureStep"/>.
    /// </summary>
    public static class FixtureStepExtensions
    {
        /// <summary>
        /// Executes the specified assertion.
        /// </summary>
        /// <param name="this">The fixture step that runs the assertion.</param>
        /// <param name="assertion">The assertion to run.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        public static void ExecuteAssertion(this FixtureStep @this, Expression<Func<bool>> assertion)
        {
            @this?.ExecuteAssertion(assertion, () => assertion.Compile()());
        }

        /// <summary>
        /// Executes the specified assertion with the specified exception that is a parameter of the assertion.
        /// </summary>
        /// <param name="this">The fixture step that runs the assertion.</param>
        /// <param name="assertion">The assertion to run.</param>
        /// <param name="exception">The exception that is a parameter of the assertion.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        public static void ExecuteAssertion(this FixtureStep @this, Expression<Func<Exception, bool>> assertion, Exception exception)
        {
            @this?.ExecuteAssertion(assertion, () => assertion.Compile()(exception), exception);
        }

        private static void ExecuteAssertion(this FixtureStep @this, LambdaExpression expression, Func<bool> assertion, Exception exception = null)
        {
            if (expression == null) return;

            bool result;
            try
            {
                result = assertion();
            }
            catch (Exception exc)
            {
                throw new AssertionException(@this, exc);
            }

            if (!result)
            {
                throw new AssertionException(@this, AssertionDescription.Of(expression, exception));
            }
        }
    }
}
