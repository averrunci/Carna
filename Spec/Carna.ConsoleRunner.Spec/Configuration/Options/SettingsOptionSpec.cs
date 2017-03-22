// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Specification("SettingsOption Spec")]
    class SettingsOptionSpec
    {
        [Context]
        SettingsOptionSpec_CanApply CanApply { get; }

        [Context]
        SettingsOptionSpec_ApplyOption ApplyOption { get; }
    }
}
