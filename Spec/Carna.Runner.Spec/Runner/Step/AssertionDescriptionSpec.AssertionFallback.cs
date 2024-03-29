﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
#pragma warning disable CS8602
using System.Linq.Expressions;

namespace Carna.Runner.Step;

[Context("Assertion Fallback")]
class AssertionDescriptionSpec_AssertionFallback : FixtureSteppable
{
    Expression<Func<bool>> Assertion { get; set; } = () => false;

    string NullReferenceExceptionMessage => GetExpectedExceptionMessage(new NullReferenceException());
    string IndexOutOfRangeExceptionMessage => GetExpectedExceptionMessage(new IndexOutOfRangeException());

    string GetExpectedExceptionMessage(Exception exc) => $"{exc.GetType().FullName}: {exc.Message}";

    [Example("When the specified expression is BinaryExpression the expression type of which is Equal and an exception occurred for an actual value of its expression")]
    void Ex01()
    {
        Given("an assertion that has 's.Length == 3' where s = null", () => { var s = (string?)null; Assertion = () => s.Length == 3; });
        Expect(
            $@"the description should be as follows:
Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})'",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})"
        );
    }

    [Example("When the specified expression is BinaryExpression the expression type of which is Equal and an exception occurred for an expected value of its expression")]
    void Ex02()
    {
        Given("an assertion that has 'x == s.Length' where x = 3, s = null", () => { var x = 3; var s = (string?)null; Assertion = () => x == s.Length; });
        Expect(
            $@"the description should be as follows:
Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3"
        );
    }

    [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object) and an exception occurred for an expected value of its expression")]
    void Ex03()
    {
        Given("an assertion that has 'x.Length.Equals(3)' where x = null", () => { var x = (string?)null; Assertion = () => x.Length.Equals(3); });
        Expect(
            $@"the description should be as follows:
Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})'",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})"
        );
    }

    [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object) and an exception occurred for an actual value of its expression")]
    void Ex04()
    {
        Given("an assertion that has 'x.Equals(s.Length)' where x = 3, s = null", () => { var x = 3; var s = (string?)null; Assertion = () => x.Equals(s.Length); });
        Expect(
            $@"the description should be as follows:
Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3"
        );
    }

    [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object) and an exception occurred for an expected value of its expression")]
    void Ex05()
    {
        Given("an assertion that has 'Equals(x.Length, 3)' where x = null", () => { var x = (string?)null; Assertion = () => Equals(x.Length, 3); });
        Expect(
            $@"the description should be as follows:
Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})'",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: 3
But was : throwing an exception ({NullReferenceExceptionMessage})"
        );
    }

    [Example("When the specified expression is MethodCallExpression that has a method object.Equals(object, object) and an exception occurred for an actual value of its expression")]
    void Ex06()
    {
        Given("an assertion that has 'Equals(x, s.Length)' where x = 3, s = null", () => { var x = 3; var s = (string?)null; Assertion = () => Equals(x, s.Length); });
        Expect(
            $@"the description should be as follows:
Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : 3"
        );
    }

    [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso and an exception occurred for an actual value of its expression ")]
    void Ex07()
    {
        Given("an assertion that has 'items.Length == 2 && items[0] == 1 && items[1] == 2' where items = new[] { 1 }", () =>
        {
            var items = new[] { 1 };
            Assertion = () => items.Length == 2 && items[0] == 1 && items[1] == 2;
        });
        Expect($@"the description should be as follows:
  [failed]
    Expected: 2
    But was : 1
  [passed]
  [failed]
    Expected: 2
    But was : throwing an exception ({IndexOutOfRangeExceptionMessage})
",
            () => AssertionDescription.Of(Assertion).ToString() == $@"
  [failed]
    Expected: 2
    But was : 1
  [passed]
  [failed]
    Expected: 2
    But was : throwing an exception ({IndexOutOfRangeExceptionMessage})");
    }

    [Example("When the specified expression is BinaryExpression the expression type of which is AndAlso and an exception occurred for an expected value of its expression ")]
    void Ex08()
    {
        Given("an assertion that has 'items.Length == 2 && items[0] == s.Length && items[1] == 2' where items = new[] { 1, 2 }, s = null", () =>
        {
            var items = new[] { 1, 2 };
            var s = (string?)null;
            Assertion = () => items.Length == 2 && items[0] == s.Length && items[1] == 2;
        });
        Expect($@"the description should be as follows:
  [passed]
  [failed]
    Expected: an exception occurred ({NullReferenceExceptionMessage})
    But was : 1
  [passed]
",
            () => AssertionDescription.Of(Assertion).ToString() == $@"
  [passed]
  [failed]
    Expected: an exception occurred ({NullReferenceExceptionMessage})
    But was : 1
  [passed]");
    }

    class TestItem
    {
        public int[] Items { get; init; } = Array.Empty<int>();
    }

    [Example("When the specified expression is MethodCallExpression that has a method Enumerable.SequenceEqual(IEnumerable, IEnumerable) and an exception occurred for an expected value of its expression")]
    void Ex09()
    {
        Given("an assertion that has 'x.Items.SequenceEqual(y.Items)' where x = null, y = { Items = [3, 4] }", () =>
        {
            var x = (TestItem?)null;
            var y = new TestItem { Items = new[] { 3, 4 } };
            Assertion = () => x.Items.SequenceEqual(y.Items);
        });
        Expect(
            $@"the description should be as follows:
Expected: [3, 4]
But was : throwing an exception ({NullReferenceExceptionMessage})'",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: [3, 4]
But was : throwing an exception ({NullReferenceExceptionMessage})"
        );
    }

    [Example("When the specified expression is MethodCallExpression that has a method Enumerable.SequenceEqual(IEnumerable, IEnumerable) and an exception occurred for an actual value of its expression")]
    void Ex10()
    {
        Given("an assertion that has 'x.Items.SequenceEqual(y.Items)' where x = { Items = [1, 2, 3] }, y = null", () =>
        {
            var x = new TestItem { Items = new[] { 1, 2, 3 } };
            var y = (TestItem?)null;
            Assertion = () => x.Items.SequenceEqual(y.Items);
        });
        Expect(
            $@"the description should be as follows:
Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : [1, 2, 3]",
            () => AssertionDescription.Of(Assertion).ToString() == $@"Expected: an exception occurred ({NullReferenceExceptionMessage})
But was : [1, 2, 3]"
        );
    }

}