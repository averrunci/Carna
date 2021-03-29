// Copyright (C) 2019-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;

namespace Carna.Assertions
{
    [Context("ToString")]
    class AssertionObjectSpec_ToString : FixtureSteppable
    {
        [Example("Default string expression is the one separated with the comma")]
        [Sample(Source = typeof(AssertionObjectSampleDataSource))]
        void Ex01(AssertionObject assertionObject, string expected)
        {
            Expect("all properties that are specified AssertionPropertyAttribute should be expressed", () => assertionObject.ToString() == expected);
        }

        class AssertionObjectSampleDataSource: ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included",
                    AssertionObject = new AssertionObjects.SimpleTestAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = "{StringProperty: PropertyA, Int32Property: 32, BooleanProperty: True, StringAssertionProperty: not PropertyA, Int32AssertionProperty: less than 3, StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC], IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]}"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new LessThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = "{SimpleTestAssertionProperty: {StringProperty: PropertyA, Int32Property: 32, BooleanProperty: True, StringAssertionProperty: not PropertyA, Int32AssertionProperty: less than 3, StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC], IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]}, StringProperty: PropertyA, Int32AssertionProperty: less than or equal 3, StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC], IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]}"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included and a property is specified a description",
                    AssertionObject = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = "{String Property: PropertyA, Int32 Property: 32, Boolean Property: True, String Assertion Property: not PropertyA, Int32AssertionProperty: greater than 3, String Enumerable Assertion Property: [ValueA, ValueB, ValueC], IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]}"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included and a property is specified a description",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestSpecifiedDescriptionAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new GreaterThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
                    },
                    Expected = "{Simple Test Assertion Property: {String Property: PropertyA, Int32 Property: 32, Boolean Property: True, String Assertion Property: not PropertyA, Int32AssertionProperty: greater than 3, String Enumerable Assertion Property: [ValueA, ValueB, ValueC], IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]}, String Property: PropertyA, Int32 Assertion Property: greater than or equal 3, String Enumerable Assertion Property: [ValueA, ValueB, ValueC]}"
                };
            }
        }

        [Example("String options is specified")]
        [Sample(Source = typeof(AssertionObjectSpecifiedStringOptionsSampleDataSource))]
        void Ex02(AssertionObject assertionObject, string expected, AssertionObject.StringOptions options)
        {
            Expect("all properties that are specified AssertionPropertyAttribute should be expressed", () => assertionObject.ToString(options) == expected);
        }

        class AssertionObjectSpecifiedStringOptionsSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                var options = new AssertionObject.StringOptions(level => $"{(level > 0 ? Environment.NewLine : null)}{new string(' ', level * 2)}", null, level => $"{Environment.NewLine}{new string(' ', level * 2)}");

                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included",
                    AssertionObject = new AssertionObjects.SimpleTestAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Options = options,
                    Expected = @"StringProperty: PropertyA
Int32Property: 32
BooleanProperty: True
StringAssertionProperty: not PropertyA
Int32AssertionProperty: less than 3
StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty =  new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new LessThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Options = options,
                    Expected = @"SimpleTestAssertionProperty: 
  StringProperty: PropertyA
  Int32Property: 32
  BooleanProperty: True
  StringAssertionProperty: not PropertyA
  Int32AssertionProperty: less than 3
  StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
  IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]
StringProperty: PropertyA
Int32AssertionProperty: less than or equal 3
StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included and a property is specified a description",
                    AssertionObject = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Options = options,
                    Expected = @"String Property: PropertyA
Int32 Property: 32
Boolean Property: True
String Assertion Property: not PropertyA
Int32AssertionProperty: greater than 3
String Enumerable Assertion Property: [ValueA, ValueB, ValueC]
IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included and a property is specified a description",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestSpecifiedDescriptionAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new GreaterThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
                    },
                    Options = options,
                    Expected = @"Simple Test Assertion Property: 
  String Property: PropertyA
  Int32 Property: 32
  Boolean Property: True
  String Assertion Property: not PropertyA
  Int32AssertionProperty: greater than 3
  String Enumerable Assertion Property: [ValueA, ValueB, ValueC]
  IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]
String Property: PropertyA
Int32 Assertion Property: greater than or equal 3
String Enumerable Assertion Property: [ValueA, ValueB, ValueC]"
                };
            }
        }

        [Example("For Description")]
        [Sample(Source = typeof(AssertionObjectForDescriptionSampleDataSource))]
        void Ex03(AssertionObject assertionObject, string expected)
        {
            Expect("all properties that are specified AssertionPropertyAttribute should be expressed", () => assertionObject.ToDescription() == expected);
        }

        class AssertionObjectForDescriptionSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included",
                    AssertionObject = new AssertionObjects.SimpleTestAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = @"
  StringProperty: PropertyA
  Int32Property: 32
  BooleanProperty: True
  StringAssertionProperty: not PropertyA
  Int32AssertionProperty: less than 3
  StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
  IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new LessThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new LessThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = @"
  SimpleTestAssertionProperty: 
    StringProperty: PropertyA
    Int32Property: 32
    BooleanProperty: True
    StringAssertionProperty: not PropertyA
    Int32AssertionProperty: less than 3
    StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
    IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]
  StringProperty: PropertyA
  Int32AssertionProperty: less than or equal 3
  StringEnumerableAssertionProperty: [ValueA, ValueB, ValueC]
  IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is not included and a property is specified a description",
                    AssertionObject = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                    {
                        StringProperty = "PropertyA",
                        Int32Property = 32,
                        BooleanProperty = true,
                        StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                        Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                        IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                    },
                    Expected = @"
  String Property: PropertyA
  Int32 Property: 32
  Boolean Property: True
  String Assertion Property: not PropertyA
  Int32AssertionProperty: greater than 3
  String Enumerable Assertion Property: [ValueA, ValueB, ValueC]
  IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]"
                };
                yield return new
                {
                    Description = "When a property whose type is the AssertionObject is included and a property is specified a description",
                    AssertionObject = new AssertionObjects.NestedAssertionObjectTestSpecifiedDescriptionAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestSpecifiedDescriptionAssertion
                        {
                            StringProperty = "PropertyA",
                            Int32Property = 32,
                            BooleanProperty = true,
                            StringAssertionProperty = new NotEqualAssertionProperty<string>("PropertyA"),
                            Int32AssertionProperty = new GreaterThanAssertionProperty<int>(3),
                            NotAssertionDoubleProperty = 3.14,
                            StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" }),
                            IntEnumerableAssertionProperty = new EnumerableAssertionProperty<int>(new[] { 1, 2, 3, 4, 5 })
                        },
                        StringProperty = "PropertyA",
                        Int32AssertionProperty = new GreaterThanOrEqualAssertionProperty<int>(3),
                        NotAssertionDoubleProperty = 3.14,
                        StringEnumerableAssertionProperty = new EnumerableAssertionProperty<string>(new[] { "ValueA", "ValueB", "ValueC" })
                    },
                    Expected = @"
  Simple Test Assertion Property: 
    String Property: PropertyA
    Int32 Property: 32
    Boolean Property: True
    String Assertion Property: not PropertyA
    Int32AssertionProperty: greater than 3
    String Enumerable Assertion Property: [ValueA, ValueB, ValueC]
    IntEnumerableAssertionProperty: [1, 2, 3, 4, 5]
  String Property: PropertyA
  Int32 Assertion Property: greater than or equal 3
  String Enumerable Assertion Property: [ValueA, ValueB, ValueC]"
                };
            }
        }
    }
}
