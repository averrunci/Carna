// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

internal sealed class FixtureContext : IFixtureContext
{
    public string Name => Descriptor.Name;

    public string FullName => Descriptor.FullName;

    public FixtureAttribute Attribute => Descriptor.Attribute;

    private FixtureDescriptor Descriptor { get; }

    private FixtureContext(FixtureDescriptor descriptor)
    {
        Descriptor = descriptor;
    }

    public static FixtureContext Of(FixtureDescriptor descriptor) => new(descriptor);
}