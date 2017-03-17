// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Represents a description when an assertion was failed.
    /// </summary>
    public class AssertionDescription
    {
        /// <summary>
        /// Gets an assertion.
        /// </summary>
        protected LambdaExpression Assertion { get; }

        /// <summary>
        /// Gets an exception that was thrown when an assertion was failed.
        /// </summary>
        protected Exception Exception { get; }

        /// <summary>
        /// Gets a format of a description.
        /// </summary>
        /// <remarks>
        /// The parameters of a format are as follows:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// When an expression of an assertion is the binary expression,
        /// {0} is the right hand side and {1} is the left hand side.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// When an expression of an assertion is the method call expression (object.Equals(object)),
        /// {0} is the parameter object and {1} is the caller object.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// When an expression of an assertion is the method call expression (Equals(object1, object2)),
        /// {0} is the second parameter object and {1} is the first parameter object.
        /// </description>
        /// </item> 
        /// </list>
        /// </remarks>
        protected virtual string DescriptionFormat { get; } = "expected: {0} but was: {1}";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionDescription"/> class
        /// with the specified assertion and exception.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="exception">the exception that was thrown when an assertion was failed.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        protected AssertionDescription(LambdaExpression assertion, Exception exception)
        {
            Assertion = assertion.RequireNonNull(nameof(assertion));
            Exception = exception;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AssertionDescription"/> class
        /// with the specified assertion.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <returns>
        /// The new instance of the <see cref="AssertionDescription"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        public static AssertionDescription Of(LambdaExpression assertion)
            => Of(assertion.RequireNonNull(nameof(assertion)), null);

        /// <summary>
        /// Creates a new instance of the <see cref="AssertionDescription"/> class
        /// with the specified assertion and exception.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="exception">The exception that was thrown when an assertion was failed.</param>
        /// <returns>
        /// The new instance of the <see cref="AssertionDescription"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        public static AssertionDescription Of(LambdaExpression assertion, Exception exception)
            => new AssertionDescription(assertion.RequireNonNull(nameof(assertion)), exception);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>The description of an assertion.</returns>
        public override string ToString()
        {
            try
            {
                return CreateDescription();
            }
            catch
            {
                return CreateDefaultDescription();
            }
        }

        /// <summary>
        /// Creates a description of an assertion.
        /// </summary>
        /// <returns>The description of the assertion.</returns>
        protected virtual string CreateDescription()
        {
            switch (Assertion.Body.NodeType)
            {
                case ExpressionType.Not:
                    return Format(false, true);
                case ExpressionType.Equal:
                    return CreateDescription(Assertion.Body as BinaryExpression);
                case ExpressionType.NotEqual:
                    return CreateDescription(Assertion.Body as BinaryExpression, "not ");
                case ExpressionType.LessThan:
                    return CreateDescription(Assertion.Body as BinaryExpression, "less than ");
                case ExpressionType.LessThanOrEqual:
                    return CreateDescription(Assertion.Body as BinaryExpression, "less than or equal ");
                case ExpressionType.GreaterThan:
                    return CreateDescription(Assertion.Body as BinaryExpression, "greater than ");
                case ExpressionType.GreaterThanOrEqual:
                    return CreateDescription(Assertion.Body as BinaryExpression, "greater than or equal ");
                case ExpressionType.Call:
                    return CreateDescription(Assertion.Body as MethodCallExpression);
                default:
                    return CreateDefaultDescription();
            }
        }

        /// <summary>
        /// Creates a default description of an assertion.
        /// </summary>
        /// <returns>The default description of the assertion.</returns>
        protected virtual string CreateDefaultDescription() => Format(true, false);

        /// <summary>
        /// Creates a description of an assertion with the specified expression and prefix.
        /// </summary>
        /// <param name="expression">The expression that is the binary expression of the assertion.</param>
        /// <param name="prefix">The prefix of a description of an assertion.</param>
        /// <returns>The description of the assertion.</returns>
        protected virtual string CreateDescription(BinaryExpression expression, string prefix = "")
            => expression == null ? CreateDefaultDescription() :
                Format($"{prefix}{RetrieveValue(expression.Right)}", RetrieveValue(expression.Left, Exception));

        /// <summary>
        /// Creates a description of an assertion with the specified expression.
        /// </summary>
        /// <param name="expression">The expression that is the method call expression of the assertion.</param>
        /// <returns>The description of the assertion.</returns>
        protected virtual string CreateDescription(MethodCallExpression expression)
        {
            if (expression == null || expression.Method.Name != "Equals" || expression.Arguments.Count == 0 || expression.Arguments.Count > 2)
            {
                return CreateDefaultDescription();
            }

            if (expression.Arguments.Count == 1)
            {
                return Format(RetrieveValue(expression.Arguments[0]), RetrieveValue(expression.Object, Exception));
            }

            return Format(RetrieveValue(expression.Arguments[1]), RetrieveValue(expression.Arguments[0], Exception));
        }

        /// <summary>
        /// Retrieves a value returned by the specified expression.
        /// </summary>
        /// <param name="expression">The expression that returns the value.</param>
        /// <param name="exception">The exception that is the parameter of the expression.</param>
        /// <returns>The value returned by the specified expression.</returns>
        protected object RetrieveValue(Expression expression, Exception exception = null)
            => exception == null ? Expression.Lambda(expression).Compile().DynamicInvoke() :
                Expression.Lambda(expression, Assertion.Parameters[0]).Compile().DynamicInvoke(exception);

        /// <summary>
        /// Formats the specified expected value and actual value.
        /// </summary>
        /// <param name="expected">The expected value of the assertion.</param>
        /// <param name="actual">The actual value of the assertion.</param>
        /// <returns>
        /// The string representation formatted with the specified expected value and actual value.
        /// </returns>
        protected string Format(object expected, object actual) => string.Format(DescriptionFormat, expected, actual);
    }
}
