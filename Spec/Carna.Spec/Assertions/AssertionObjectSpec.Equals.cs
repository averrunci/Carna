// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;

namespace Carna.Assertions
{
    [Context("Equals")]
    class AssertionObjectSpec_Equals : FixtureSteppable
    {
        [Example("Verifies the AssertionObject with the Equals method")]
        [Sample(Source = typeof(AssertionObjectSampleDataSource))]
        void Ex01(AssertionObject expected, AssertionObject actual, bool expectedResult)
        {
            Expect($"the expected result should be {expectedResult}", () => Equals(expected, actual) == expectedResult);
            if (expectedResult) Expect("the hash code should be the same", () => expected.GetHashCode() == actual.GetHashCode());
        }

        [Example("Verifies the AssertionObject with the == operator")]
        [Sample(Source = typeof(AssertionObjectSampleDataSource))]
        void Ex02(AssertionObject expected, AssertionObject actual, bool expectedResult)
        {
            Expect($"the expected result should be {expectedResult}", () => (expected == actual) == expectedResult);
            if (expectedResult) Expect("the hash code should be the same", () => expected.GetHashCode() == actual.GetHashCode());
        }

        [Example("Verifies the AssertionObject with the != operator")]
        [Sample(Source = typeof(AssertionObjectSampleDataSource))]
        void Ex03(AssertionObject expected, AssertionObject actual, bool expectedResult)
        {
            Expect($"the expected result should be {expectedResult}", () => (expected != actual) == !expectedResult);
            if (expectedResult) Expect("the hash code should be the same", () => expected.GetHashCode() == actual.GetHashCode());
        }

        class AssertionObjectSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new
                {
                    Description = "When all properties are equal",
                    Expected = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                    Actual = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 2.72 },
                    ExpectedResult = true
                };
                yield return new
                {
                    Description = "When all properties are not equal",
                    Expected = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                    Actual = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 31, BooleanProperty = true, NotAssertionDoubleProperty = 2.72 },
                    ExpectedResult = false
                };
                yield return new
                {
                    Description = "When all properties are equal (including a property whose type is the AssertionObject)",
                    Expected = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                        StringProperty = "PropertyA",
                        NotAssertionDoubleProperty = 3.14
                    },
                    Actual = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                        StringProperty = "PropertyA",
                        NotAssertionDoubleProperty = 3.14
                    },
                    ExpectedResult = true
                };
                yield return new
                {
                    Description = "When all properties are not equal (including a property whose type is the AssertionObject)",
                    Expected = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                        StringProperty = "PropertyA",
                        NotAssertionDoubleProperty = 3.14
                    },
                    Actual = new AssertionObjects.NestedAssertionObjectTestAssertion
                    {
                        SimpleTestAssertionProperty = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 31, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 },
                        StringProperty = "PropertyA",
                        NotAssertionDoubleProperty = 3.14
                    },
                    ExpectedResult = false
                };

                var assertion = new AssertionObjects.SimpleTestAssertion { StringProperty = "PropertyA", Int32Property = 32, BooleanProperty = true, NotAssertionDoubleProperty = 3.14 };
                yield return new
                {
                    Description = "When the reference is the same",
                    Expected = assertion,
                    Actual = assertion,
                    ExpectedResult = true
                };
                yield return new
                {
                    Description = "When the expected assertion object is null",
                    Expected = (AssertionObject)null,
                    Actual = assertion,
                    ExpectedResult = false
                };
                yield return new
                {
                    Description = "When the actual assertion object is null",
                    Expected = assertion,
                    Actual = (AssertionObject)null,
                    ExpectedResult = false
                };
            }
        }
    }
}
