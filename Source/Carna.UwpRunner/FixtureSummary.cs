// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Fievus.Windows.Mvc.Bindings;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a summary of fixtures.
    /// </summary>
    public class FixtureSummary
    {
        /// <summary>
        /// Gets a total count of fixtures.
        /// </summary>
        public ObservableProperty<long> TotalCount { get; } = new ObservableProperty<long>(0);

        /// <summary>
        /// Gets a passed count of fixtures.
        /// </summary>
        public ObservableProperty<long> PassedCount { get; } = new ObservableProperty<long>(0);

        /// <summary>
        /// Gets a failed count of fixtures.
        /// </summary>
        public ObservableProperty<long> FailedCount { get; } = new ObservableProperty<long>(0);

        /// <summary>
        /// Gets a pending count of fixtures.
        /// </summary>
        public ObservableProperty<long> PendingCount { get; } = new ObservableProperty<long>(0);

        /// <summary>
        /// Gets a passed rate of fixtures.
        /// </summary>
        public ObservableProperty<int> PassedRate { get; } = new ObservableProperty<int>(0);

        /// <summary>
        /// Gets a value that indicates whether a time text is visible.
        /// </summary>
        public ObservableProperty<bool> IsTimeVisible { get; } = new ObservableProperty<bool>();

        /// <summary>
        /// Gets a start date time of fixture running.
        /// </summary>
        public ObservableProperty<string> StartDateTime { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets an end date time of fixture running.
        /// </summary>
        public ObservableProperty<string> EndDateTime { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets a duration of fixture running.
        /// </summary>
        public ObservableProperty<string> Duration { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets a value that indicates whether a fixture is building.
        /// </summary>
        public ObservableProperty<bool> IsFixtureBuilding { get; } = false.ToObservableProperty();

        /// <summary>
        /// Gets a value that indicates whether a fixture is built.
        /// </summary>
        public ObservableProperty<bool> IsFixtureBuilt { get; } = false.ToObservableProperty();

        /// <summary>
        /// Gets a value that indicates whether a fixture is running.
        /// </summary>
        public ObservableProperty<bool> IsFixtureRunning { get; } = false.ToObservableProperty();

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureSummary"/> class.
        /// </summary>
        public FixtureSummary()
        {
            PassedRate.Bind(PassedCount, passedCount =>
                TotalCount.Value == 0 ? 0 : (int)((double)passedCount / TotalCount.Value * 100)
            );

            IsTimeVisible.Bind(TotalCount, totalCount => totalCount > 0);
        }
    }
}
