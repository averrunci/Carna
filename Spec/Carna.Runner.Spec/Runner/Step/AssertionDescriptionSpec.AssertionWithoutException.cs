// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

namespace Carna.Runner.Step
{
    [Context("Assertion without Exception")]
    class AssertionDescriptionSpec_AssertionWithoutException :FixtureSteppable
    {
        private Expression<Func<bool>> Assertion { get; set; }

        [Example("When the specified expression is otherwise")]
        void Ex01()
        {
            Given("an assertion", () => { var x = false; Assertion = () => x; });
            Expect(
                "the description should be 'expected: True but was: False'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: True but was: False"
            );
        }

        [Example("When the specified expression is UnaryExpression the expression type of which is Not")]
        void Ex02()
        {
            Given("an assertion that has '!x' where x = true", () => { var x = true; Assertion = () => !x; });
            Expect(
                "the description should be 'expected: False but was: True'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: False but was: True"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is Equal")]
        void Ex03()
        {
            Given("an assertion that has 'x == 3' where x = 5", () => { var x = 5; Assertion = () => x == 3; });
            Expect(
                "the description should be 'expected: 3 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: 3 but was: 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is NotEqual")]
        void Ex04()
        {
            Given("an assertion that has 'x != 5' where x = 5", () => { var x = 5; Assertion = () => x != 5; });
            Expect(
                "the description should be 'expected: not 5 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: not 5 but was: 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThan")]
        void Ex05()
        {
            Given("an assertion that has 'x < 2' where x = 5", () => { var x = 5; Assertion = () => x < 2; });
            Expect(
                "the description should be 'expected: less than 2 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: less than 2 but was: 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThanOrEqual")]
        void Ex06()
        {
            Given("an assertion that has 'x <= 2' where x = 5", () => { var x = 5; Assertion = () => x <= 2; });
            Expect(
                "the description should be 'expected: less than or equal 2 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: less than or equal 2 but was: 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThan")]
        void Ex07()
        {
            Given("an assertion that has 'x > 7' where x = 5", () => { var x = 5; Assertion = () => x > 7; });
            Expect(
                "the description should be 'expected: greater than 7 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: greater than 7 but was: 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThanOrEqual")]
        void Ex08()
        {
            Given("an assertion that has 'x >= 7' where x = 5", () => { var x = 5; Assertion = () => x >= 7; });
            Expect(
                "the description should be 'expected: greater than or equal 7 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: greater than or equal 7 but was: 5"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object)")]
        void Ex09()
        {
            Given("an assertion that has 'x.Equals(3)' where x = 5", () => { var x = 5; Assertion = () => x.Equals(3); });
            Expect(
                "the description should be 'expected: 3 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: 3 but was: 5"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex10()
        {
            Given("an assertion that has 'Equals(x, 3)' where x = 5", () => { var x = 5; Assertion = () => Equals(x, 3); });
            Expect(
                "the description should be 'expected: 3 but was: 5'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: 3 but was: 5"
            );
        }

        [Example("When the actual value is null")]
        void Ex11()
        {
            Given("an assertion that has 'x == \"3\"' where x = null", () => { var x = (string)null; Assertion = () => x == "3"; });
            Expect(
                "the description should be 'expected: 3 but was: null'",
                () => AssertionDescription.Of(Assertion).ToString() == "expected: 3 but was: null"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has two expressions.")]
        void Ex12()
        {
            Given("an assertion that has 'x == 3 && y == 5' where x = 5; y = 5", () => { var x = 5; var y = 5 ; Assertion = () => x == 3 && y == 5; });
            var e = AssertionDescription.Of(Assertion).ToString();
            Expect(
                @"the description should be as follows:
  [failed] expected: 3 but was: 5
  [passed]
",
                () => AssertionDescription.Of(Assertion).ToString() == @"
  [failed] expected: 3 but was: 5
  [passed]"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has more than two expressions.")]
        void Ex13()
        {
            Given("an assertion that has 'x == 3 && y == 5 && z == 7 && u == 9' where x = 5; y = 5; z == 7; u == 7", () =>
                { var x = 5; var y = 5; var z = 7; var u = 7; Assertion = () => x == 3 && y == 5 && z == 7 && u == 9; }
            );
            var e = AssertionDescription.Of(Assertion).ToString();
            Expect(
                @"the description should be as follows:
  [failed] expected: 3 but was: 5
  [passed]
  [passed]
  [failed] expected: 9 but was: 7
",
                () => AssertionDescription.Of(Assertion).ToString() == @"
  [failed] expected: 3 but was: 5
  [passed]
  [passed]
  [failed] expected: 9 but was: 7"
            );
        }
    }
}
