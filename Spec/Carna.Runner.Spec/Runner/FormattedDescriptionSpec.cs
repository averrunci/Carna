// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner
{
    [Specification("FormattedDescription Spec")]
    class FormattedDescriptionSpec
    {
        [Context]
        FormattedDescription_LinesConcatenation LinesConcatenation { get; }

        [Context]
        FormattedDescription_StringRepresentation StringRepresentation { get; }
    }
}
