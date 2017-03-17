// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna
{
    [Specification("FixtureSteppable Spec")]
    class FixtureSteppableSpec
    {
        [Context]
        FixtureSteppable_ExpectStep ExpectStep { get; }

        [Context]
        FixtureSteppable_GivenStep GivenStep { get; }

        [Context]
        FixtureSteppable_WhenStep WhenStep { get; }

        [Context]
        FixtureSteppable_ThenStep ThenStep { get; }

        [Context]
        FixtureSteppable_NoteStep NoteStep { get; }
    }
}
