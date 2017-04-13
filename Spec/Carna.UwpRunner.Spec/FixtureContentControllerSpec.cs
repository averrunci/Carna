// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

using Fievus.Windows.Mvc;

namespace Carna.UwpRunner
{
    [Specification("FixtureContentController Spec")]
    class FixtureContentControllerSpec : FixtureSteppable, IDisposable
    {
        FixtureContentController Controller { get; } = new FixtureContentController
        {
            Content = new FixtureContent()
        };

        Grid Grid { get; set; }
        VisualStateGroup StatusEllipseStateGroup { get; set; }

        public async void Dispose()
        {
            if (Grid != null)
            {
                await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    VisualStateManager.GetVisualStateGroups(Grid).Clear()
                );
            }
        }

        [Example("State of the StatusEllipse")]
        async Task Ex01()
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Grid = new Grid();
                using (var reader = new StreamReader(GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Carna.UwpRunner.StatusEllipseVisualStateGroup.xaml")))
                {
                    VisualStateManager.GetVisualStateGroups(Grid).Add(XamlReader.Load(reader.ReadToEnd()) as VisualStateGroup);
                }
                Controller.Root = new UserControl { Content = Grid };

                When("the pointer is entered", () => RaiseEventOnStatusEllipse(nameof(UIElement.PointerEntered)));
                Then("the state should PointerEntered", () => VisualStateManager.GetVisualStateGroups(Grid)[0].CurrentState.Name == "PointerEntered");
                When("the pointer is exited", () => RaiseEventOnStatusEllipse(nameof(UIElement.PointerExited)));
                Then("the state should PointerExited", () => VisualStateManager.GetVisualStateGroups(Grid)[0].CurrentState.Name == "PointerExited");
            });
        }

        void RaiseEventOnStatusEllipse(string eventName)
            => UwpController.RetrieveEventHandlers(Controller)
                .GetBy("StatusEllipse")
                .Raise(eventName);

        [Example("Determines whether the text that represents whether the child content is open is visible")]
        async Task Ex02()
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Controller.Root = new UserControl();

                Expect("the text should not be visible", () => !Controller.Content.IsChildOpenTextVisible.Value);
                When("the pointer is entered", () => RaiseEventOnStatusEllipse(nameof(UIElement.PointerEntered)));
                Then("the text should be visble", () => Controller.Content.IsChildOpenTextVisible.Value);
                When("the pointer is exited", () => RaiseEventOnStatusEllipse(nameof(UIElement.PointerExited)));
                Expect("the text should not be visible", () => !Controller.Content.IsChildOpenTextVisible.Value);
            });
        }

        [Example("Determines whether the child content is open")]
        void Ex03()
        {
            Expect("the child content should be close", () => !Controller.Content.IsChildOpen.Value);
            When("StatusEllipse is tapped", () => TapStatusEllipse());
            Then("the child content should be open", () => Controller.Content.IsChildOpen.Value);
            When("StatusEllipse is tapped", () => TapStatusEllipse());
            Then("the child content should be close", () => !Controller.Content.IsChildOpen.Value);
        }

        void TapStatusEllipse()
            => UwpController.RetrieveEventHandlers(Controller)
                .GetBy("StatusEllipse")
                .Raise(nameof(UIElement.Tapped));
    }
}
