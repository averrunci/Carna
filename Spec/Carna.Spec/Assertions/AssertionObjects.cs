// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions
{
    internal static class AssertionObjects
    {
        public class SimpleTestAssertion : AssertionObject
        {
            [AssertionProperty]
            public string StringProperty { get; set; }

            [AssertionProperty]
            public int Int32Property { get; set; }

            [AssertionProperty]
            public bool BooleanProperty { get; set; }

            public double NotAssertionDoubleProperty { get; set; }
        }

        public class NestedAssertionObjectTestAssertion : AssertionObject
        {
            [AssertionProperty]
            public SimpleTestAssertion SimpleTestAssertionProperty { get; set; }

            [AssertionProperty]
            public string StringProperty { get; set; }

            public double NotAssertionDoubleProperty { get; set; }
        }

        public class SimpleTestSpecifiedDescriptionAssertion : AssertionObject
        {
            [AssertionProperty("String Property")]
            public string StringProperty { get; set; }

            [AssertionProperty("Int32 Property")]
            public int Int32Property { get; set; }

            [AssertionProperty("Boolean Property")]
            public bool BooleanProperty { get; set; }

            public double NotAssertionDoubleProperty { get; set; }
        }

        public class NestedAssertionObjectTestSpecifiedDescriptionAssertion : AssertionObject
        {
            [AssertionProperty("Simple Test Assertion Property")]
            public SimpleTestSpecifiedDescriptionAssertion SimpleTestAssertionProperty { get; set; }

            [AssertionProperty("String Property")]
            public string StringProperty { get; set; }

            public double NotAssertionDoubleProperty { get; set; }
        }
    }
}
