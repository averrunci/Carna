// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification("AssertionDescription Spec")]
class AssertionDescriptionSpec
{
    [Context]
    AssertionDescriptionSpec_AssertionWithoutException AssertionWithoutException => default!;

    [Context]
    AssertionDescriptionSpec_AssertionWithException AssertionWithException => default!;

    [Context]
    AssertionDescriptionSpec_AssertionWithTypedException AssertionWithTypedException => default!;

    [Context]
    AssertionDescriptionSpec_AssertionFallback AssertionFallback => default!;

    [Context]
    AssertionDescriptionSpec_AssertionWithAssertionObject AssertionWithAssertionObject => default!;
}