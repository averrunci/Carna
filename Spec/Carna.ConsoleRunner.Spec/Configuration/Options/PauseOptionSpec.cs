﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification(
    $"{nameof(PauseOption)} Spec",
    typeof(PauseOptionSpec_CanApply),
    typeof(PauseOptionSpec_ApplyOption)
)]
class PauseOptionSpec;