// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;

namespace Carna;

/// <summary>
/// Specifies a culture during running a fixture.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CultureAttribute : AroundFixtureAttribute
{
    /// <summary>
    /// Gets a culture during running a fixture.
    /// </summary>
    public CultureInfo Culture { get; }

    private CultureInfo OriginalCulture { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CultureAttribute"/> class
    /// with the specified name of the culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture.</param>
    public CultureAttribute(string cultureName)
    {
        Culture = new CultureInfo(cultureName, false);
        OriginalCulture = Culture;
    }

    /// <summary>
    /// Stores the current culture and replaces it with the culture specified in the constructor.
    /// </summary>
    /// <param name="context">The context of the fixture.</param>
    public override void OnFixtureRunning(IFixtureContext context)
    {
        OriginalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = Culture;
        CultureInfo.CurrentCulture.ClearCachedData();
    }

    /// <summary>
    /// Restores the culture that is one before running the fixture.
    /// </summary>
    /// <param name="context">The context of the fixture.</param>
    public override void OnFixtureRun(IFixtureContext context)
    {
        Thread.CurrentThread.CurrentCulture = OriginalCulture;
        CultureInfo.CurrentCulture.ClearCachedData();
    }
}