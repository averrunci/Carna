// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration.Options;

[Specification("AssemblyOption Spec")]
class AssemblyOptionSpec
{
    [Context]
    AssemblyOptionSpec_CanApply CanApply => default!;

    [Context]
    AssemblyOptionSpec_ApplyOption ApplyOption => default!;
}