// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification("HelpOption Spec")]
class HelpOptionSpec
{
    [Context]
    HelpOptionSpec_CanApply CanApply => default!;

    [Context]
    HelpOptionSpec_ApplyOption ApplyOption => default!;
}