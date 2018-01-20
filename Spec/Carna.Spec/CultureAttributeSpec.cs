// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using System.Threading;

using NSubstitute;

namespace Carna
{
    [Specification("CultureAttribute Spec")]
    class CultureAttributeSpec : FixtureSteppable
    {
        CultureAttribute CultureAttribute { get; } = new CultureAttribute("fr-FR");

        IFixtureContext FixtureContext = Substitute.For<IFixtureContext>();
        CultureInfo CurrentCultureInfo { get; } = Thread.CurrentThread.CurrentCulture;

        [Example("Sets the Culture on running a fixture")]
        void Ex01()
        {
            When("to occur before running a fixture", () => CultureAttribute.OnFixtureRunning(FixtureContext));
            Then("the current culture should be changed to the specified culture", () => Thread.CurrentThread.CurrentCulture == CultureAttribute.Culture);
            When("to occur after running a fixture", () => CultureAttribute.OnFixtureRun(FixtureContext));
            Then("the current culture should be restored to the value before running a fixture", () => Thread.CurrentThread.CurrentCulture == CurrentCultureInfo);
        }
    }
}
