// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification("PauseOption Spec")]
class PauseOptionSpec
{
    [Context]
    PauseOptionSpec_CanApply CanApply => default!;

    [Context]
    PauseOptionSpec_ApplyOption ApplyOption => default!;
}