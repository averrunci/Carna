﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;

namespace Carna;

/// <summary>
/// Specifies a UI culture during running a fixture.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UICultureAttribute : AroundFixtureAttribute
{
    /// <summary>
    /// Gets a UI culture during running a fixture.
    /// </summary>
    public CultureInfo UICulture { get; }

    private CultureInfo OriginalUICulture { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UICultureAttribute"/> class
    /// with the specified name of the UI culture.
    /// </summary>
    /// <param name="uiCultureName">The name of the UI culture.</param>
    public UICultureAttribute(string uiCultureName)
    {
        UICulture = new CultureInfo(uiCultureName, false);
        OriginalUICulture = UICulture;
    }

    /// <summary>
    /// Stores the current UI culture and replaces it with the UI culture specified in the constructor.
    /// </summary>
    /// <param name="context">The context of the fixture.</param>
    public override void OnFixtureRunning(IFixtureContext context)
    {
        OriginalUICulture = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentUICulture = UICulture;
        CultureInfo.CurrentUICulture.ClearCachedData();
    }

    /// <summary>
    /// Restores the UI culture that is one before running the fixture.
    /// </summary>
    /// <param name="context">The context of the fixture.</param>
    public override void OnFixtureRun(IFixtureContext context)
    {
        Thread.CurrentThread.CurrentUICulture = OriginalUICulture;
        CultureInfo.CurrentUICulture.ClearCachedData();
    }
}