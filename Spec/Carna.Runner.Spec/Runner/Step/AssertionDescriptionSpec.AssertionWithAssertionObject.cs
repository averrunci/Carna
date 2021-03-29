// Copyright (C) 2019-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq.Expressions;

using Carna.Assertions;

namespace Carna.Runner.Step
{
    [Context("Assertion with AssertionObject")]
    class AssertionDescriptionSpec_AssertionWithAssertionObject : FixtureSteppable
    {
        TestAssertion Expected { get; }

        Expression<Func<bool>> Assertion { get; set; }

        [Background("Expected is (x:5, y:3)")]
        public AssertionDescriptionSpec_AssertionWithAssertionObject()
        {
            Expected = new TestAssertion(5, 3);
        }

        [Example("When the specified expression is BinaryExpression whose expression type is Equal")]
        void Ex01()
        {
            Given("an assertion that has '(x:3, y:3) == Expected'", () => Assertion = () => new TestAssertion(3, 3) == Expected);
            Expect(
                @"the description should be as follows:
Expected: {X: 5, Y: 3}
But was : {X: 3, Y: 3}",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: {X: 5, Y: 3}
But was : {X: 3, Y: 3}"
            );
        }

        [Example("When the specified expression is BinaryExpression whose expression type is NotEqual")]
        void Ex02()
        {
            Given("an assertion that has '(x: 5, y: 3) != Expected'", () => Assertion = () => new TestAssertion(5, 3) != Expected);
            Expect(
                @"the description should be as follows:
Expected: not {X: 5, Y: 3}
But was : {X: 5, Y: 3}",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: not {X: 5, Y: 3}
But was : {X: 5, Y: 3}"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object)")]
        void Ex03()
        {
            Given("an assertion that has '(x: 3 y: 3).Equals(Expected)'", () => Assertion = () => new TestAssertion(3, 3).Equals(Expected));
            Expect(
                @"the description should be as follows:
Expected: {X: 5, y: 3}
But was : {X: 3, y: 3}",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: {X: 5, Y: 3}
But was : {X: 3, Y: 3}"
            );
        }

        [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object)")]
        void Ex04()
        {
            Given("an assertion that has 'Equals((x: 3, y: 3), Expected)'", () => Assertion = () => Equals(new TestAssertion(3, 3), Expected));
            Expect(
                @"the description should be as follows:
Expected: {X: 5, Y: 3}
But was : {X: 3, Y: 3}",
                () => AssertionDescription.Of(Assertion).ToString() == @"Expected: {X: 5, Y: 3}
But was : {X: 3, Y: 3}"
            );
        }

        class TestAssertion : AssertionObject
        {
            [AssertionProperty]
            private int X { get; }

            [AssertionProperty]
            private int Y { get; }

            public TestAssertion(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
