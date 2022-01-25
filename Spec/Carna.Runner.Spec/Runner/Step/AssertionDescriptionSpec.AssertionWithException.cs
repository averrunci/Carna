// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq.Expressions;

namespace Carna.Runner.Step
{
    [Context("Assertion with Exception")]
    class AssertionDescriptionSpec_AssertionWithException : FixtureSteppable
    {
        private Expression<Func<Exception, bool>> Assertion { get; set; } = _ => false;
        Exception Exception { get; } = new("Message");

        [Example("When the specified expression is otherwise")]
        void Ex01()
        {
            Given("an assertion that has 'exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = exc => exc.Message.Contains("?"));
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
            Given("an assertion that has '!exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = exc => !exc.Message.Contains("?"));
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
            Given("an assertion that has 'exc.Message.Length == 3' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length == 3);
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is NotEqual")]
        void Ex04()
        {
            Given("an assertion that has 'exc.Message.Length != 7' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length != 7);
            Expect(
                @"the description should be as follows:
Expected: not 7
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: not 7
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThan")]
        void Ex05()
        {
            Given("an assertion that has 'exc.Message.Length < 2' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length < 2);
            Expect(
                @"the description should be as follows:
Expected: less than 2
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: less than 2
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThanOrEqual")]
        void Ex06()
        {
            Given("an assertion that has 'exc.Message.Length <= 2' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length <= 2);
            Expect(
                @"the description should be as follows:
Expected: less than or equal 2
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: less than or equal 2
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThan")]
        void Ex07()
        {
            Given("an assertion that has 'exc.Message.Length > 9' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length > 9);
            Expect(
                @"the description should be as follows:
Expected: greater than 9
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: greater than 9
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThanOrEqual")]
        void Ex08()
        {
            Given("an assertion that has 'exc.Message.Length >= 9' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length >= 9);
            Expect(
                @"the description should be as follows:
Expected: greater than or equal 9
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: greater than or equal 9
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression and its right hand side is the actual expression that has Exception instance")]
        void Ex09()
        {
            Given("an assertion that has '3 == exc.Message.Length' where exc.Message='Message'", () => Assertion = exc => 3 == exc.Message.Length);
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
            Given("an assertion that has 'exc.Message.Length.Equals(3)' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length.Equals(3));
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 7"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex11()
        {
            Given("an assertion that has 'Equals(exc.Message.Length, 3)' where exc.Message='Message'", () => Assertion = exc => Equals(exc.Message.Length, 3));
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 7",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"Expected: 3
But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has two expressions.")]
        void Ex12()
        {
            Given(
                "an assertion that has 'exc.GetType() == typeof(Exception) && exc.Message.Length == 3' where the type of exc is Exception; exc.Message='Message'",
                () => Assertion = exc => exc.GetType() == typeof(Exception) && exc.Message.Length == 3
            );
            Expect(
                @"the description should be as follows:
  [passed]
  [failed]
    Expected: 3
    But was : 7
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [passed]
  [failed]
    Expected: 3
    But was : 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has more than two expressions.")]
        void Ex13()
        {

            Given(
                "an assertion that has 'exc.GetType() == typeof(Exception) && exc.Message.Length == 3 && exc.Message == \"exc\"' where the type of exc is Exception; exc.Message='Message'",
                () => Assertion = exc => exc.GetType() == typeof(Exception) && exc.Message.Length == 3 && exc.Message == "exc"
            );
            Expect(
                @"the description should be as follows:
  [passed]
  [failed]
    Expected: 3
    But was : 7
  [failed]
    Expected: exc
    But was : Message
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [passed]
  [failed]
    Expected: 3
    But was : 7
  [failed]
    Expected: exc
    But was : Message"
            );
        }
    }
}
