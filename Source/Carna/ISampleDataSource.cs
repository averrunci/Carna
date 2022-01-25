// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;

namespace Carna;

/// <summary>
/// Provides the function to get sample data.
/// </summary>
public interface ISampleDataSource
{
    /// <summary>
    /// Gets sample data.
    /// </summary>
    /// <remarks>
    /// The instance of the sample data must have properties
    /// that are specified to a fixture method. Its name
    /// (case is ignored) is equal to a parameter name of the
    /// fixture method. The property named "Description" is
    /// used the description of the sample fixture.
    /// </remarks>
    /// <returns>The sample data.</returns>
    IEnumerable GetData();
}