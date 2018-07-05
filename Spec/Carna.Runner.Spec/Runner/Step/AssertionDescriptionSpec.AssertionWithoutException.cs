// Copyright (C) 2017-2018 Fievus
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
        Expression<Func<bool>> Assertion { get; set; }

        [Example("When the specified expression is otherwise")]
        void Ex01()
        {
            Given("an assertion", () => { var x = false; Assertion = () => x; });
            Expect(
                @"the description should be as follows:
Expected: True
But was : False",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: True
But was : False"
            );
        }

        [Example("When the specified expression is UnaryExpression the expression type of which is Not")]
        void Ex02()
        {
            Given("an assertion that has '!x' where x = true", () => { var x = true; Assertion = () => !x; });
            Expect(
                @"the description should be as follows:
Expected: False
But was : True",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: False
But was : True"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is Equal")]
        void Ex03()
        {
            Given("an assertion that has 'x == 3' where x = 5", () => { var x = 5; Assertion = () => x == 3; });
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: 3
But was : 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is NotEqual")]
        void Ex04()
        {
            Given("an assertion that has 'x != 5' where x = 5", () => { var x = 5; Assertion = () => x != 5; });
            Expect(
                @"the description should be as follows:
Expected: not 5
But was : 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: not 5
But was : 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThan")]
        void Ex05()
        {
            Given("an assertion that has 'x < 2' where x = 5", () => { var x = 5; Assertion = () => x < 2; });
            Expect(
                @"the description should be as follows:
Expected: less than 2
But was : 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: less than 2
But was : 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is LessThanOrEqual")]
        void Ex06()
        {
            Given("an assertion that has 'x <= 2' where x = 5", () => { var x = 5; Assertion = () => x <= 2; });
            Expect(
                @"the description should be as follows:
Expected: less than or equal 2
But was : 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: less than or equal 2
But was : 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThan")]
        void Ex07()
        {
            Given("an assertion that has 'x > 7' where x = 5", () => { var x = 5; Assertion = () => x > 7; });
            Expect(
                @"the description should be as follows:
Expected: greater than 7
But was : 5'",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: greater than 7
But was : 5"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is GreaterThanOrEqual")]
        void Ex08()
        {
            Given("an assertion that has 'x >= 7' where x = 5", () => { var x = 5; Assertion = () => x >= 7; });
            Expect(
                @"the description should be as follows:
Expected: greater than or equal 7
But was : 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: greater than or equal 7
But was : 5"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object)")]
        void Ex09()
        {
            Given("an assertion that has 'x.Equals(3)' where x = 5", () => { var x = 5; Assertion = () => x.Equals(3); });
            Expect(
                @"the description should be as follows:
Expected: 3
But was : 5'",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: 3
But was : 5"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex10()
        {
            Given("an assertion that has 'Equals(x, 3)' where x = 5", () => { var x = 5; Assertion = () => Equals(x, 3); });
            Expect(
                @"the description should be as follows:
Expected: 3 But was: 5",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: 3
But was : 5"
            );
        }

        [Example("When the actual value is null")]
        void Ex11()
        {
            Given("an assertion that has 'x == \"3\"' where x = null", () => { var x = (string)null; Assertion = () => x == "3"; });
            Expect(
                @"the description should be as follows:
Expected: 3
But was : null'",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: 3
But was : null"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has two expressions.")]
        void Ex12()
        {
            Given("an assertion that has 'x == 3 && y == 5' where x = 5; y = 5", () => { var x = 5; var y = 5 ; Assertion = () => x == 3 && y == 5; });
            Expect(
                @"the description should be as follows:
  [failed]
    Expected: 3
    But was : 5
  [passed]
",
                () => AssertionDescription.Of(Assertion).ToString() == @"
  [failed]
    Expected: 3
    But was : 5
  [passed]"
            );
        }

        [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso that has more than two expressions.")]
        void Ex13()
        {
            Given("an assertion that has 'x == 3 && y == 5 && z == 7 && u == 9' where x = 5; y = 5; z == 7; u == 7", () =>
                { var x = 5; var y = 5; var z = 7; var u = 7; Assertion = () => x == 3 && y == 5 && z == 7 && u == 9; }
            );
            Expect(
                @"the description should be as follows:
  [failed]
    Expected: 3
    But was : 5
  [passed]
  [passed]
  [failed]
    Expected: 9
    But was : 7
",
                () => AssertionDescription.Of(Assertion).ToString() == @"
  [failed]
    Expected: 3
    But was : 5
  [passed]
  [passed]
  [failed]
    Expected: 9
    But was : 7"
            );
        }
    }
}
