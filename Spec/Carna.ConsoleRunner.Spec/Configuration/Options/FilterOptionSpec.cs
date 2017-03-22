// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options
{
    [Specification("FilterOption Spec")]
    class FilterOptionSpec
    {
        [Context]
        FilterOptionSpec_CanApply CanApply { get; }

        [Context]
        FilterOptionSpec_ApplyOption ApplyOption { get; }
    }
}
