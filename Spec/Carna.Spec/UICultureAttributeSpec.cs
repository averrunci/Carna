// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using System.Threading;

using NSubstitute;

namespace Carna
{
    [Specification("UICultureAttribute Spec")]
    class UICultureAttributeSpec : FixtureSteppable
    {
        UICultureAttribute UICultureAttribute { get; } = new UICultureAttribute("fr-FR");

        IFixtureContext FixtureContext { get; } = Substitute.For<IFixtureContext>();
        CultureInfo CurrentUICultureInfo { get; } = Thread.CurrentThread.CurrentUICulture;

        [Example("Sets the UI Culture on running a fixture")]
        void Ex01()
        {
            When("to occur before running a fixture", () => UICultureAttribute.OnFixtureRunning(FixtureContext));
            Then("the current UI culture should be changed to the specified culture", () => Equals(Thread.CurrentThread.CurrentUICulture, UICultureAttribute.UICulture));
            When("to occur after running a fixture", () => UICultureAttribute.OnFixtureRun(FixtureContext));
            Then("the current UI culture should be restored to the value before running a fixture", () => Equals(Thread.CurrentThread.CurrentUICulture, CurrentUICultureInfo));
        }
    }
}
