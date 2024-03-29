﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification(
    $"{nameof(HelpOption)} Spec",
    typeof(HelpOptionSpec_CanApply),
    typeof(HelpOptionSpec_ApplyOption)
)]
class HelpOptionSpec;