// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

using Fievus.Windows.Mvc;

namespace Carna.UwpRunner
{
    [Specification("FixtureSummaryController Spec")]
    class FixtureSummaryControllerSpec : FixtureSteppable
    {
        FixtureSummaryController Controller { get; } = new FixtureSummaryController
        {
            FixtureSummary = new FixtureSummary()
        };

        [Example("Creates the appropriate geometry when the passed rate is changed")]
        [Sample(100, typeof(EllipseGeometry), Description = "When the passed rate is 100")]
        [Sample(99, typeof(PathGeometry), Description = "When the passed rate is less than 100")]
        async Task Ex01(int passedRate, Type expectedPathType)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Controller.PassedRatePath = new Path();

                Given("the root element is loaded", () =>
                    UwpController.RetrieveEventHandlers(Controller)
                        .GetBy(null)
                        .Raise(nameof(FrameworkElement.Loaded))
                );
                When($"the passed rate is changed", () => Controller.FixtureSummary.PassedRate.Value = passedRate);
                Then($"the type of the created path data should be {expectedPathType}", () =>
                    Controller.PassedRatePath.Data.GetType() == expectedPathType
                );
            });
        }
    }
}
