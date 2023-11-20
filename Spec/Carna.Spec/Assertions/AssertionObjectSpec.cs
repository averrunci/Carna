// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Assertions;

[Specification(
    $"{nameof(AssertionObject)} Spec",
    typeof(AssertionObjectSpec_Equals),
    typeof(AssertionObjectSpec_ToString)
)]
class AssertionObjectSpec;