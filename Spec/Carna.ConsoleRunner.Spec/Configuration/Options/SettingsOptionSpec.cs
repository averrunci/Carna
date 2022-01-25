// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification("SettingsOption Spec")]
class SettingsOptionSpec
{
    [Context]
    SettingsOptionSpec_CanApply CanApply => default!;

    [Context]
    SettingsOptionSpec_ApplyOption ApplyOption => default!;
}