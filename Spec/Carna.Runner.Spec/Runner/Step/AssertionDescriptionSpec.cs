// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner.Step;

[Specification(
    $"{nameof(AssertionDescription)} Spec",
    typeof(AssertionDescriptionSpec_AssertionWithoutException),
    typeof(AssertionDescriptionSpec_AssertionWithException),
    typeof(AssertionDescriptionSpec_AssertionWithTypedException),
    typeof(AssertionDescriptionSpec_AssertionFallback),
    typeof(AssertionDescriptionSpec_AssertionWithAssertionObject)
)]
class AssertionDescriptionSpec;