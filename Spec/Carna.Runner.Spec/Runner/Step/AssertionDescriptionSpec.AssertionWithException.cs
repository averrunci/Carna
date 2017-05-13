// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

namespace Carna.Runner.Step
{
    [Context("Assertion with Exception")]
    class AssertionDescriptionSpec_AssertionWithException : FixtureSteppable
    {
        private Expression<Func<Exception, bool>> Assertion { get; set; }
        private Exception Exception { get; } = new Exception("Message");

        [Example("When the specified expression is otherwise")]
        void Ex01()
        {
            Given("an assertion that has 'exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = exc => exc.Message.Contains("?"));
            Expect(
                "the description should be 'expected: True but was: False'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: True but was: False"
            );
        }

        [Example("When the specified expression is UnaryExpression the expression type of which is Not")]
        void Ex02()
        {
            Given("an assertion that has '!exc.Message.Contains(\"?\")' where exc.Message='Message'", () => Assertion = exc => !exc.Message.Contains("?"));
            Expect(
                "the description should be 'expected: False but was: True'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: False but was: True"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is Equal")]
        void Ex03()
        {
            Given("an assertion that has 'exc.Message.Length == 3' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length == 3);
            Expect(
                "the description should be 'expected: 3 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: 3 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is NotEqual")]
        void Ex04()
        {
            Given("an assertion that has 'exc.Message.Length != 7' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length != 7);
            Expect(
                "the description should be 'expected: not 7 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: not 7 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThan")]
        void Ex05()
        {
            Given("an assertion that has 'exc.Message.Length < 2' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length < 2);
            Expect(
                "the description should be 'expected: less than 2 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: less than 2 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThanOrEqual")]
        void Ex06()
        {
            Given("an assertion that has 'exc.Message.Length <= 2' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length <= 2);
            Expect(
                "the description should be 'expected: less than or equal 2 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: less than or equal 2 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThan")]
        void Ex07()
        {
            Given("an assertion that has 'exc.Message.Length > 9' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length > 9);
            Expect(
                "the description should be 'expected: greater than 9 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: greater than 9 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThanOrEqual")]
        void Ex08()
        {
            Given("an assertion that has 'exc.Message.Length >= 9' where exc.Message='Message'", () => Assertion = exc => exc.Message.Length >= 9);
            Expect(
                "the description should be 'expected: greater than or equal 9 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: greater than or equal 9 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression and its right hand side is the actual expression that has Exception instance")]
        void Ex09()
        {
            Given("an assertion that has '3 == exc.Message.Length' where exc.Message='Message'", () => Assertion = exc => 3 == exc.Message.Length);
            Expect(
                "the description should be 'expected: True but was: False'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: True but was: False"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object)")]
        void Ex10()
        {
            Given("an assertion that has 'exc.Message.Length.Equals(3)'", () => Assertion = exc => exc.Message.Length.Equals(3));
            Expect(
                "the description should be 'expected: 3 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: 3 but was: 7"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex11()
        {
            Given("an assertion that has 'Equals(exc.Message.Length, 3)'", () => Assertion = exc => Equals(exc.Message.Length, 3));
            Expect(
                "the description should be 'expected: 3 but was: 7'",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == "expected: 3 but was: 7"
            );
        }

        [Example("When the actual value is null")]
        void Ex12()
        {
            Given(
                "an assertion that has 'exc.Message == 'message'' where exc is an instance of the NullMessageException class whose message is null",
                () => Assertion = exc => exc.Message == "message"
            );
            Expect(
                "the description should be 'expected: message but was: null'",
                () => AssertionDescription.Of(Assertion, new NullMessageException()).ToString() == "expected: message but was: null"
            );
        }

        class NullMessageException : Exception
        {
            public override string Message => null;
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has two expressions.")]
        void Ex13()
        {
            Given(
                "an assertion that has 'exc.GetType() == typeof(Exception) && exc.Message.Length == 3' where the type of exc is Exception;exc.Message='Message'",
                () => Assertion = exc => exc.GetType() == typeof(Exception) && exc.Message.Length == 3
            );
            Expect(
                @"the description should be as follows:
  [passed]
  [failed] expected: 3 but was: 7
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [passed]
  [failed] expected: 3 but was: 7"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has more than two expressions.")]
        void Ex14()
        {

            Given(
                "an assertion that has 'exc.GetType() == typeof(Exception) && exc.Message.Length == 3 && exc.Message == \"exc\"' where the type of exc is Exception;exc.Message='Message'",
                () => Assertion = exc => exc.GetType() == typeof(Exception) && exc.Message.Length == 3 && exc.Message == "exc"
            );
            Expect(
                @"the description should be as follows:
  [passed]
  [failed] expected: 3 but was 7
  [failed] expected: exc but was: Message
",
                () => AssertionDescription.Of(Assertion, Exception).ToString() == @"
  [passed]
  [failed] expected: 3 but was: 7
  [failed] expected: exc but was: Message"
            );
        }
    }
}
