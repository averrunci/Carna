// Copyright (C) 2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions
{
    [Specification("AssertionObject Spec")]
    class AssertionObjectSpec
    {
        [Context]
        AssertionObjectSpec_Equals AssertionObjectEquals { get; }

        [Context]
        AssertionObjectSpec_ToString AssertionObjectToString { get; }
    }
}
