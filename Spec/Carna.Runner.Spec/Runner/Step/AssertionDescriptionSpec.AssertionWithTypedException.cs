// Copyright (C) 2017-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

namespace Carna.Runner.Step
{
    [Context("Assertion with Typed Exception")]
    class AssertionDescriptionSpec_AssertionWithTypedException : FixtureSteppable
    {
        Expression<Func<Exception, bool>> Assertion { get; set; }
        ArgumentNullException Exception { get; } = new ArgumentNullException("Parameter name", "Message");
        ParameterExpression Parameter { get; } = Expression.Parameter(typeof(Exception));
        Expression<Func<Exception, ArgumentNullException>> ConvertExpression { get; } = exc => (ArgumentNullException)exc;

        Expression<Func<Exception, bool>> CreateAssertion(Expression<Func<ArgumentNullException, bool>> assertion)
            => Expression.Lambda<Func<Exception, bool>>(Expression.Invoke(assertion, Expression.Invoke(ConvertExpression, Parameter)), Parameter);

        [Example("When the specified expression is otherwise")]
        void Ex01()
        {
            Given("an assertion that has 'exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = CreateAssertion(exc => exc.Message.Contains("?")));
            Expect(
                @"the description should be as follows:
Expected: True
But was : False",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: True
But was : False"
            );
        }

        [Example("When the specified expression is UnaryExpression the expression type of which is Not")]
        void Ex02()
        {
            Given("an assertion that has '!exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = CreateAssertion(exc => !exc.Message.Contains("?")));
            Expect(
                @"the description should be as follows:
Expected: False
But was : True",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: False
But was : True"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is Equal")]
        void Ex03()
        {
            Given("an assertion that has 'exc.ParamName.Length == 3' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length == 3));
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is NotEqual")]
        void Ex04()
        {
            Given("an assertion that has 'exc.ParamName.Length != 14' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length != 14));
            Expect(
                @"the description should be as follows:
Expected: not 14
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: not 14
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThan")]
        void Ex05()
        {
            Given("an assertion that has 'exc.ParamName.Length < 10' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length < 10));
            Expect(
                @"the description should be as follows:
Expected: less than 10
but was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: less than 10
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThanOrEqual")]
        void Ex06()
        {
            Given("an assertion that has 'exc.ParamName.Length <= 10' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length <= 10));
            Expect(
                @"the description should be as follows:
Expected: less than or equal 10
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: less than or equal 10
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThan")]
        void Ex07()
        {
            Given("an assertion that has 'exc.ParamName.Length > 15' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length > 15));
            Expect(
                @"the description should be as follows:
Expected: greater than 15
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: greater than 15
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThanOrEqual")]
        void Ex08()
        {
            Given("an assertion that has 'exc.ParamName.Length >= 15' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length >= 15));
            Expect(
                @"the description should be as follows:
Expected: greater than or equal 15
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: greater than or equal 15
But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression and its right hand side is the actual expression that has Exception instance")]
        void Ex09()
        {
            Given("an assertion that has '10 == exc.ParamName.Length' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => 10 == exc.ParamName.Length));
            Expect(
                @"the description should be as follows:
Expected: True
But was : False",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: True
But was : False"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object)")]
        void Ex10()
        {
            Given("an assertion that has 'exc.ParameterName.Length.Equals(3)' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => exc.ParamName.Length.Equals(3)));
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 14"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex11()
        {
            Given("an assertion that has 'Equals(exc.ParamName.Length, 3)' where exc.ParamName='Parameter name'", () => Assertion = CreateAssertion(exc => Equals(exc.ParamName.Length, 3)));
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 14",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 14"
            );
        }

        [Example("When the actual value is null")]
        void Ex12()
        {
            Given(
                "an assertion that has 'exc.ParamName == 'Param'' where exc is an instance of the ArgumentNullException class whose ParamName is null",
                () => Assertion = CreateAssertion(exc => exc.ParamName == "Param")
            );
            Expect(
                @"the description should be as follows:
Expected: Param
But was : null",
                () => AssertionDescription.Of(Assertion, new ArgumentNullException(null)).ToString() == @"Expected: Param
But was : null"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has two expressions.")]
        void Ex13()
        {
            Given(
                "an assertion that has 'exc.ParamName == 'Parameter name' where the type of exc is ArgumentNullException; exc.ParamName='Parameter name';",
                () => Assertion = CreateAssertion(exc => exc.ParamName == "Parameter name" && exc.ParamName.Length == 3)
            );
            Expect(
                @"the description should be as follows:
  [passed]
  [failed]
    Expected: 3
    But was : 14
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [passed]
  [failed]
    Expected: 3
    But was : 14"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has more than two expressions.")]
        void Ex14()
        {

            Given(
                "an assertion that has 'exc.ParamName == 'Parameter' && exc.Parameter name.Length == 3 && exc.Message != null' where the type of exc is ArgumentNullException; exc.ParamName='Parameter name'; exc.Message='Message'",
                () => Assertion = CreateAssertion(exc => exc.ParamName == "Parameter" && exc.ParamName.Length == 3 && exc.Message != null)
            );
            Expect(
                @"the description should be as follows:
  [failed]
    Expected: Parameter
    But was : Parameter name
  [failed]
    Expected: 3
    But was : 14
  [passed]
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [failed]
    Expected: Parameter
    But was : Parameter name
  [failed]
    Expected: 3
    But was : 14
  [passed]"
            );
        }
    }
}
