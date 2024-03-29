﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;

namespace Carna.Assertions;

[Specification("AssertionProperty Spec")]
class AssertionPropertySpec : FixtureSteppable
{
    [Example("The specified assertion property is the same type")]
    [Sample(Source = typeof(SpecifiedAssertionPropertySameTypeSampleDataSource))]
    void Ex01(IAssertionProperty expected, IAssertionProperty actual, bool expectedResult)
    {
        Expect($"The asserted result should be {expectedResult}", () => expected.Assert(actual) == expectedResult);
    }

    class SpecifiedAssertionPropertySameTypeSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "For the EqualAssertionProperty, the actual value and the expected value are the same",
                Expected = new EqualAssertionProperty<string>("PropertyA"),
                Actual = new EqualAssertionProperty<string>("PropertyA"),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty, the actual value and the expected value are different",
                Expected = new EqualAssertionProperty<string>("PropertyA"),
                Actual = new EqualAssertionProperty<string>("PropertyB"),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new EqualAssertionProperty<string>("PropertyA"),
                Actual = new ActualValueProperty<string>("PropertyA"),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are different",
                Expected = new EqualAssertionProperty<string>("PropertyA"),
                Actual = new ActualValueProperty<string>("PropertyB"),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty, the actual value and the expected value are the same",
                Expected = new NotEqualAssertionProperty<string>("PropertyA"),
                Actual = new NotEqualAssertionProperty<string>("PropertyA"),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty, the actual value and the expected value are different",
                Expected = new NotEqualAssertionProperty<string>("PropertyA"),
                Actual = new NotEqualAssertionProperty<string>("PropertyB"),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new NotEqualAssertionProperty<string>("PropertyA"),
                Actual = new ActualValueProperty<string>("PropertyA"),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are different",
                Expected = new NotEqualAssertionProperty<string>("PropertyA"),
                Actual = new ActualValueProperty<string>("PropertyB"),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value and the expected value are the same",
                Expected = new LessThanAssertionProperty<int>(3),
                Actual = new LessThanAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value is less than the expected value",
                Expected = new LessThanAssertionProperty<int>(4),
                Actual = new LessThanAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value is greater than the expected value",
                Expected = new LessThanAssertionProperty<int>(2),
                Actual = new LessThanAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new LessThanAssertionProperty<int>(3),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value that is ActualValueProperty is less than the expected value",
                Expected = new LessThanAssertionProperty<int>(4),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty, the actual value that is ActualValueProperty is greater than the expected value",
                Expected = new LessThanAssertionProperty<int>(2),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value and the expected value are the same",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new LessThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value is less than the expected value",
                Expected = new LessThanOrEqualAssertionProperty<int>(4),
                Actual = new LessThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value is greater than the expected value",
                Expected = new LessThanOrEqualAssertionProperty<int>(2),
                Actual = new LessThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value that is ActualValueProperty is less than the expected value",
                Expected = new LessThanOrEqualAssertionProperty<int>(4),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty, the actual value that is ActualValueProperty is greater than the expected value",
                Expected = new LessThanOrEqualAssertionProperty<int>(2),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value and the expected value are the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new GreaterThanAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value is less than the expected value",
                Expected = new GreaterThanAssertionProperty<int>(4),
                Actual = new GreaterThanAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value is greater than the expected value",
                Expected = new GreaterThanAssertionProperty<int>(2),
                Actual = new GreaterThanAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value that is ActualValueProperty is less than the expected value",
                Expected = new GreaterThanAssertionProperty<int>(4),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty, the actual value that is ActualValueProperty is greater than the expected value",
                Expected = new GreaterThanAssertionProperty<int>(2),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value and the expected value are the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new GreaterThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value is less than the expected value",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(4),
                Actual = new GreaterThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value is greater than the expected value",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(2),
                Actual = new GreaterThanOrEqualAssertionProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value that is ActualValueProperty is less than the expected value",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(4),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty, the actual value that is ActualValueProperty is greater than the expected value",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(2),
                Actual = new ActualValueProperty<int>(3),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value and the expected value are the same",
                Expected = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                Actual = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value and the expected value are different",
                Expected = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                Actual = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueD" }),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 }),
                Actual = new ActualValueProperty<IEnumerable<int>>(new[] { 1, 2, 3, 4, 5 }),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value that is ActualValueProperty and the expected value are the same (null)",
                Expected = new EnumerableAssertionProperty<int>(null),
                Actual = new ActualValueProperty<IEnumerable<int>?>(null),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value that is ActualValueProperty and the expected value are different",
                Expected = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 }),
                Actual = new ActualValueProperty<IEnumerable<int>>(new[] { 1, 2, 3 }),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value that is ActualValueProperty is not null and the expected value is null",
                Expected = new EnumerableAssertionProperty<int>(null),
                Actual = new ActualValueProperty<IEnumerable<int>>(new[] { 1, 2, 3 }),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty, the actual value that is ActualValueProperty is null and the expected value is not null",
                Expected = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 }),
                Actual = new ActualValueProperty<IEnumerable<int>?>(null),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value and the expected value are the same",
                Expected = new EnumerableAssertionProperty<IEnumerable<string>>(new[] { new[] { "Value1-1", "Value1-2", "Value1-3" }, new[] { "Value2-1" }, new[] { "Value3-1", "Value3-2" } }),
                Actual = new EnumerableAssertionProperty<IEnumerable<string>>(new[] { new[] { "Value1-1", "Value1-2", "Value1-3" }, new[] { "Value2-1" }, new[] { "Value3-1", "Value3-2" } }),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value and the expected value are different",
                Expected = new EnumerableAssertionProperty<IEnumerable<string>>(new[] { new[] { "Value1-1", "Value1-2", "Value1-3" }, new[] { "Value2-1" }, new[] { "Value3-1", "Value3-2" } }),
                Actual = new EnumerableAssertionProperty<IEnumerable<string>>(new[] { new[] { "Value1-1", "Value1-2", "Value1-3" }, new[] { "Value2-2" }, new[] { "Value3-1", "Value3-2" } }),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value that is ActualValueProperty and the expected value are the same",
                Expected = new EnumerableAssertionProperty<IEnumerable<int>>(new[] { new[] { 1, 2, 3 }, new[] { 4 }, new[] { 5, 6 } }),
                Actual = new ActualValueProperty<IEnumerable<IEnumerable<int>>>(new[] { new[] { 1, 2, 3 }, new[] { 4 }, new[] { 5, 6 } }),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value that is ActualValueProperty and the expected value are the same (null)",
                Expected = new EnumerableAssertionProperty<IEnumerable<int>>(null),
                Actual = new ActualValueProperty<IEnumerable<IEnumerable<int>>?>(null),
                ExpectedResult = true
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value that is ActualValueProperty and the expected value are different",
                Expected = new EnumerableAssertionProperty<IEnumerable<int>>(null),
                Actual = new ActualValueProperty<IEnumerable<IEnumerable<int>>>(new[] { new[] { 1, 2, 3 }, new[] { 4 }, new[] { 5, 6 } }),
                ExpectedResult = false
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty whose value is IEnumerable, the actual value that is ActualValueProperty is not null and the expected value is null",
                Expected = new EnumerableAssertionProperty<IEnumerable<int>>(new[] { new[] { 1, 2, 3 }, new[] { 4 }, new[] { 5, 6 } }),
                Actual = new ActualValueProperty<IEnumerable<IEnumerable<int>>?>(null),
                ExpectedResult = false
            };
        }
    }

    [Example("The specified assertion property is the different type")]
    [Sample(Source = typeof(SpecifiedAssertionPropertyDifferentTypeSampleDataSource))]
    void Ex02(IAssertionProperty expected, IAssertionProperty actual)
    {
        Expect("The asserted result should be False", () => !expected.Assert(actual));
    }

    class SpecifiedAssertionPropertyDifferentTypeSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "For the EqualAssertionProperty and EqualAssertionProperty whose value type is different",
                Expected = new EqualAssertionProperty<int>(3),
                Actual = new EqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and NotEqualAssertionProperty whose value type is different",
                Expected = new NotEqualAssertionProperty<int>(3),
                Actual = new NotEqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty and LessThanAssertionProperty whose value type is different",
                Expected = new LessThanAssertionProperty<int>(3),
                Actual = new LessThanAssertionProperty<double>(4.5)
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty and LessThanOrEqualAssertionProperty whose value type is different",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new LessThanOrEqualAssertionProperty<double>(4.5)
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty and GreaterThanAssertionProperty whose value type is different",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new GreaterThanAssertionProperty<double>(4.5)
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is different",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new GreaterThanOrEqualAssertionProperty<double>(4.5)
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty and EnumerableAssertionProperty whose value type is different",
                Expected = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3 }),
                Actual = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and NotEqualAssertionProperty whose value type is the same",
                Expected = new NotEqualAssertionProperty<string>("PropertyA"),
                Actual = new EqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and LessThanAssertionProperty whose value type is the same",
                Expected = new LessThanAssertionProperty<int>(3),
                Actual = new EqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and LessThanOrEqualAssertionProperty whose value type is the same",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new EqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and GreaterThanAssertionProperty whose value type is the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new EqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the EqualAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and LessThanAssertionProperty whose value type is the same",
                Expected = new LessThanAssertionProperty<int>(3),
                Actual = new NotEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and LessThanOrEqualAssertionProperty whose value type is the same",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new NotEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and GreaterThanAssertionProperty whose value type is the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new NotEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new NotEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty and LessThanOrEqualAssertionProperty whose value type is the same",
                Expected = new LessThanOrEqualAssertionProperty<int>(3),
                Actual = new LessThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty and GreaterThanAssertionProperty whose value type is the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new LessThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new LessThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty and GreaterThanAssertionProperty whose value type is the same",
                Expected = new GreaterThanAssertionProperty<int>(3),
                Actual = new LessThanOrEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new LessThanOrEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty and GreaterThanOrEqualAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new GreaterThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty and EnumerableAssertionProperty whose value type is the same",
                Expected = new GreaterThanOrEqualAssertionProperty<int>(3),
                Actual = new EnumerableAssertionProperty<int>(new[] { 3 })
            };
        }
    }

    [Example("The specified assertion property is null")]
    [Sample(Source = typeof(SpecifiedAssertionPropertyNullSampleDataSource))]
    void Ex03(IAssertionProperty assertionProperty)
    {
        Expect("The asserted result should be False", () => !assertionProperty.Assert(null));
    }

    class SpecifiedAssertionPropertyNullSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "For the EqualAssertionProperty",
                AssertionProperty = new EqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty",
                AssertionProperty = new NotEqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty",
                AssertionProperty = new LessThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty",
                AssertionProperty = new LessThanOrEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty",
                AssertionProperty = new GreaterThanAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty",
                AssertionProperty = new GreaterThanOrEqualAssertionProperty<int>(3)
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty",
                AssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
            };
        }
    }

    [Example("The ActualValueProperty does not assert any AssertionProperty")]
    [Sample(Source = typeof(AssertedPropertyNotAssertSampleDataSource))]
    void Ex04(IAssertionProperty assertedProperty, IAssertionProperty assertionProperty)
    {
        When("the ActualValueProperty asserts the AssertionProperty", () => assertedProperty.Assert(assertionProperty));
        Then<InvalidOperationException>($"{typeof(InvalidOperationException)} should be thrown");
    }

    class AssertedPropertyNotAssertSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "For the EqualAssertionProperty",
                AssertedProperty = new ActualValueProperty<string>("PropertyA"),
                AssertionProperty = new EqualAssertionProperty<string>("PropertyA")
            };
            yield return new
            {
                Description = "For the NotEqualAssertionProperty",
                AssertedProperty = new ActualValueProperty<string>("PropertyA"),
                AssertionProperty = new NotEqualAssertionProperty<string>("PropertyB")
            };
            yield return new
            {
                Description = "For the LessThanAssertionProperty",
                AssertedProperty = new ActualValueProperty<int>(3),
                AssertionProperty = new LessThanAssertionProperty<int>(2)
            };
            yield return new
            {
                Description = "For the LessThanOrEqualAssertionProperty",
                AssertedProperty = new ActualValueProperty<int>(3),
                AssertionProperty = new LessThanOrEqualAssertionProperty<int>(2)
            };
            yield return new
            {
                Description = "For the GreaterThanAssertionProperty",
                AssertedProperty = new ActualValueProperty<int>(3),
                AssertionProperty = new GreaterThanAssertionProperty<int>(4)
            };
            yield return new
            {
                Description = "For the GreaterThanOrEqualAssertionProperty",
                AssertedProperty = new ActualValueProperty<int>(3),
                AssertionProperty = new GreaterThanOrEqualAssertionProperty<int>(4)
            };
            yield return new
            {
                Description = "For the EnumerableAssertionProperty",
                AssertedProperty = new ActualValueProperty<string>("PropertyA"),
                AssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
            };
            yield return new
            {
                Description = "For the ActualValueProperty",
                AssertedProperty = new ActualValueProperty<int>(3),
                AssertionProperty = new ActualValueProperty<int>(3)
            };
        }
    }
}