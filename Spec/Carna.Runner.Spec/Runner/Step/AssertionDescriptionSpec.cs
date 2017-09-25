// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step
{
    [Specification("AssertionDescription Spec")]
    class AssertionDescriptionSpec
    {
        [Context]
        AssertionDescriptionSpec_AssertionWithoutException AssertionWithountException { get; }

        [Context]
        AssertionDescriptionSpec_AssertionWithException AssertionWithException { get; }

        [Context]
        AssertionDescriptionSpec_AssertionWithTypedException AssertionWithTypedException { get; }

        [Context]
        AssertionDescriptionSpec_AssertionFallback AssertionFallback { get; }
    }
}
