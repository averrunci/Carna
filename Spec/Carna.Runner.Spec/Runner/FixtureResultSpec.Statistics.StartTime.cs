// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;

namespace Carna.Runner
{
    [Context("Start time")]
    class FixtureResultSpec_Statistics_StartTime : FixtureSteppable
    {
        private IEnumerable<FixtureResult> FixtureResults { get; set; }

        private FixtureDescriptor ContainerFixtureDescriptor { get; } = new FixtureDescriptor("ContainerTest", new ContextAttribute());
        private FixtureDescriptor FixtureDescriptor { get; } = new FixtureDescriptor("Test", new ExampleAttribute());

        [Example("When Enumerable of FixtureResult does not have any sub results")]
        void Ex01()
        {
            Given(
                "5 FixtureResults ()",
                () => FixtureResults = new[]
                {
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 3)).Pending(),
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 2)).Pending(),
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 1)).Passed(),
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 4)).Failed(null),
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 5)).Pending()
                }
            );
            Expect("the start time should be the minimum start time in 5 FixtureResults", () => FixtureResults.StartTime() == new DateTime(2017, 1, 1));
        }

        [Example("When Enemrable of FixtureResult has any sub fixtures, the start time of sub fixtures is ignored")]
        void Ex03()
        {
            Given(
                "3 FixtureResults (one is a container fixture that has 3 FixtureResults, the others are not container fixtures)",
                () => FixtureResults = new[]
                {
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 3)).Ready(),
                    FixtureResult.Of(ContainerFixtureDescriptor).StartAt(new DateTime(2017, 1, 1)).FinishedWith(new[]
                    {
                        FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2016, 1, 1)).Pending(),
                        FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2016, 1, 2)).Pending(),
                        FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2016, 1, 3)).Pending()
                    }),
                    FixtureResult.Of(FixtureDescriptor).StartAt(new DateTime(2017, 1, 2)).Pending()
                }
            );
            Expect("the start time should be the minimum start time in 3 FixtureResults", () => FixtureResults.StartTime() == new DateTime(2017, 1, 1));
        }
    }
}
