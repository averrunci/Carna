// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna;

[Specification("FixtureSteppable Spec")]
class FixtureSteppableSpec
{
    [Context]
    FixtureSteppable_ExpectStep ExpectStep => default!;

    [Context]
    FixtureSteppable_GivenStep GivenStep => default!;

    [Context]
    FixtureSteppable_WhenStep WhenStep => default!;

    [Context]
    FixtureSteppable_ThenStep ThenStep => default!;

    [Context]
    FixtureSteppable_NoteStep NoteStep => default!;
}