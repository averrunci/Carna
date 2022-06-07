// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Carna.Runner.Step;

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
    protected Exception? Exception { get; }

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
    protected virtual string DescriptionFormat { get; } = $"Expected: {{0}}{Environment.NewLine}But was : {{1}}";

    /// <summary>
    /// Gets a format of an expected value when an exception occurred at retrieving it.
    /// </summary>
    /// <remarks>
    /// The parameter({0}) of a format is a message of an exception.
    /// </remarks>
    protected virtual string FallbackExpectedValueFormat { get; } = "an exception occurred ({0})";

    /// <summary>
    /// Gets a format of an actual value when an exception occurred at retrieving it.
    /// </summary>
    /// <remarks>
    /// The parameter({0}) of a format is a message of an exception.
    /// </remarks>
    protected virtual string FallbackActualValueFormat { get; } = "throwing an exception ({0})";

    /// <summary>
    /// Gets or sets a value that indicates whether a new line is required at first line.
    /// </summary>
    protected bool RequireFirstNewLine { get; set; }

    /// <summary>
    /// Gets or sets an indent count for each line.
    /// </summary>
    protected int IndentCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionDescription"/> class
    /// with the specified assertion and exception.
    /// </summary>
    /// <param name="assertion">The assertion.</param>
    /// <param name="exception">the exception that was thrown when an assertion was failed.</param>
    protected AssertionDescription(LambdaExpression assertion, Exception? exception)
    {
        Assertion = assertion;
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
    public static AssertionDescription Of(LambdaExpression assertion)
        => Of(assertion, null);

    /// <summary>
    /// Creates a new instance of the <see cref="AssertionDescription"/> class
    /// with the specified assertion and exception.
    /// </summary>
    /// <param name="assertion">The assertion.</param>
    /// <param name="exception">The exception that was thrown when an assertion was failed.</param>
    /// <returns>
    /// The new instance of the <see cref="AssertionDescription"/> class.
    /// </returns>
    public static AssertionDescription Of(LambdaExpression assertion, Exception? exception)
        => new(assertion, exception);

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
        RequireFirstNewLine = false;
        var description = CreateDescription(Assertion.Body, Assertion.Parameters.FirstOrDefault());
        return RequireFirstNewLine ? $"{Environment.NewLine}  {description}" : description;
    }

    /// <summary>
    /// Creates a description of an assertion with the specified expression and parameter expression.
    /// </summary>
    /// <param name="expression">The expression of the assertion.</param>
    /// <param name="parameter">The parameter expression of the assertion.</param>
    /// <returns>The description of the assertion.</returns>
    protected virtual string CreateDescription(Expression? expression, ParameterExpression? parameter)
    {
        switch (expression?.NodeType)
        {
            case ExpressionType.Not:
                return Format(false, true);
            case ExpressionType.Equal:
                return CreateDescription(expression as BinaryExpression, parameter);
            case ExpressionType.NotEqual:
                return CreateDescription(expression as BinaryExpression, parameter, "not ");
            case ExpressionType.LessThan:
                return CreateDescription(expression as BinaryExpression, parameter, "less than ");
            case ExpressionType.LessThanOrEqual:
                return CreateDescription(expression as BinaryExpression, parameter, "less than or equal ");
            case ExpressionType.GreaterThan:
                return CreateDescription(expression as BinaryExpression, parameter, "greater than ");
            case ExpressionType.GreaterThanOrEqual:
                return CreateDescription(expression as BinaryExpression, parameter, "greater than or equal ");
            case ExpressionType.AndAlso:
                return CreateAndAlsoDescription(expression as BinaryExpression, parameter);
            case ExpressionType.Call:
                return CreateDescription(expression as MethodCallExpression, parameter);
            case ExpressionType.Invoke:
                return CreateDescription(expression as InvocationExpression);
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
    /// Creates a description of an assertion with the specified expression, parameter expression, and prefix.
    /// </summary>
    /// <param name="expression">The expression that is the binary expression of the assertion.</param>
    /// <param name="parameter">The parameter expression of the assertion.</param>
    /// <param name="prefix">The prefix of a description of an assertion.</param>
    /// <returns>The description of the assertion.</returns>
    protected virtual string CreateDescription(BinaryExpression? expression, ParameterExpression? parameter, string prefix = "")
        => expression is null ? CreateDefaultDescription() :
            Format(
                $"{prefix}{RetrieveValue(expression.Right, fallbackValueFormat: FallbackExpectedValueFormat)}",
                RetrieveValue(expression.Left, parameter, Exception, FallbackActualValueFormat)
            );

    /// <summary>
    /// Creates a description of an assertion the the specified expression the expression type of which is AndAlso
    /// and parameter expression.
    /// </summary>
    /// <param name="expression">
    /// The expression that is the binary expression of the assertion. Its expression type of AndAlso.
    /// </param>
    /// <param name="parameter">The parameter expression of the assertion.</param>
    /// <returns>The description of the assertion.</returns>
    protected virtual string CreateAndAlsoDescription(BinaryExpression? expression, ParameterExpression? parameter)
    {
        if (expression is null) return CreateDefaultDescription();

        RequireFirstNewLine = true;
        IndentCount = 4;
        var lines = new List<string>();
        CreateAndAlsoDescription(expression.Left, parameter, lines);
        CreateAndAlsoDescription(expression.Right, parameter, lines);
        return $"{string.Join(Environment.NewLine + "  ", lines)}";
    }

    private void CreateAndAlsoDescription(Expression? expression, ParameterExpression? parameter, List<string> lines)
    {
        if (expression is null) return;

        if (expression.NodeType is ExpressionType.AndAlso)
        {
            lines.Add(CreateDescription(expression, parameter));
        }
        else
        {
            try
            {
                var assertionResult = Exception is null ? Expression.Lambda(expression).Compile().DynamicInvoke() :
                    parameter is null ? Expression.Lambda(expression).Compile().DynamicInvoke(Exception) :
                    Expression.Lambda(expression, parameter).Compile().DynamicInvoke(Exception);
                if (assertionResult is true)
                {
                    lines.Add("[passed]");
                    return;
                }
            }
            catch
            {
                // ignored
            }

            lines.Add($"[failed]{Environment.NewLine}{CreateDescription(expression, parameter)}");
        }
    }

    /// <summary>
    /// Creates a description of an assertion with the specified expression and parameter expression.
    /// </summary>
    /// <param name="expression">The expression that is the method call expression of the assertion.</param>
    /// <param name="parameter">The parameter expression of the assertion.</param>
    /// <returns>The description of the assertion.</returns>
    protected virtual string CreateDescription(MethodCallExpression? expression, ParameterExpression? parameter)
    {
        if (IsEnumerableSequenceEqualMethod(expression))
        {
            var expected = RetrieveValue(expression.Arguments[1], fallbackValueFormat: FallbackExpectedValueFormat) as IEnumerable;
            var actual = RetrieveValue(expression.Arguments[0], parameter, Exception, FallbackActualValueFormat) as IEnumerable;
            if (expected is null || actual is null) return Format(expected, actual);

            return Format(RetrieveEnumerableValue(expected), RetrieveEnumerableValue(actual));

        }

        if (!IsObjectEqualsMethod(expression)) return CreateDefaultDescription();

        if (expression.Arguments.Count is 1)
        {
            return Format(
                RetrieveValue(expression.Arguments[0], fallbackValueFormat: FallbackExpectedValueFormat),
                RetrieveValue(expression.Object, parameter, Exception, FallbackActualValueFormat)
            );
        }

        return Format(
            RetrieveValue(expression.Arguments[1], fallbackValueFormat: FallbackExpectedValueFormat),
            RetrieveValue(expression.Arguments[0], parameter, Exception, FallbackActualValueFormat)
        );

    }

    private bool IsEnumerableSequenceEqualMethod([NotNullWhen(true)] MethodCallExpression? expression)
        => expression is not null && expression.Method.DeclaringType == typeof(Enumerable) && expression.Method.Name is nameof(Enumerable.SequenceEqual) && expression.Arguments.Count is 2;

    private string RetrieveEnumerableValue(IEnumerable values) => values as string ?? $"[{string.Join(", ", values.OfType<object>())}]";

    private bool IsObjectEqualsMethod([NotNullWhen(true)] MethodCallExpression? expression)
        => expression?.Method.Name is nameof(Equals) && expression.Arguments.Count is 1 or 2;

    /// <summary>
    /// Creates a description of an assertion with the specified expression and parameter expression.
    /// </summary>
    /// <param name="expression">The expression that is the invocation expression of the assertion.</param>
    /// <returns>The description of the assertion.</returns>
    protected virtual string CreateDescription(InvocationExpression? expression)
    {
        var lambda = expression?.Expression as LambdaExpression;
        var parameter = lambda?.Parameters[0];
        return CreateDescription(lambda?.Body, parameter);
    }

    /// <summary>
    /// Retrieves a value returned by the specified expression.
    /// </summary>
    /// <param name="expression">The expression that returns the value.</param>
    /// <param name="parameter">The parameter expression of the assertion.</param>
    /// <param name="exception">The exception that is the parameter of the expression.</param>
    /// <param name="fallbackValueFormat">The format of a value when an exception occurred.</param>
    /// <returns>The value returned by the specified expression.</returns>
    protected object RetrieveValue(Expression? expression, ParameterExpression? parameter = null, Exception? exception = null, string? fallbackValueFormat = null)
    {
        if (expression is null) return "null";

        try
        {
            return (exception is null ? Expression.Lambda(expression).Compile().DynamicInvoke() :
                parameter is null ? Expression.Lambda(expression).Compile().DynamicInvoke(exception) :
                Expression.Lambda(expression, parameter).Compile().DynamicInvoke(exception)) ?? "null";
        }
        catch (Exception exc)
        {
            if (string.IsNullOrWhiteSpace(fallbackValueFormat) || exc.InnerException is null) throw;

            return string.Format(fallbackValueFormat, $"{exc.InnerException.GetType().FullName}: {exc.InnerException.Message}");
        }
    }

    /// <summary>
    /// Formats the specified expected value and actual value.
    /// </summary>
    /// <param name="expected">The expected value of the assertion.</param>
    /// <param name="actual">The actual value of the assertion.</param>
    /// <returns>
    /// The string representation formatted with the specified expected value and actual value.
    /// </returns>
    protected string Format(object? expected, object? actual)
    {
        var indent = new string(' ', IndentCount);
        return string.Format($"{indent}{string.Join($"{Environment.NewLine}{indent}", DescriptionFormat.Split(new[] { Environment.NewLine }, StringSplitOptions.None))}", expected, actual);
    }
}