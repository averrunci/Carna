// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Carna.Runner;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a summary of fixtures.
    /// </summary>
    public class FixtureSummary : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a total count of fixtures.
        /// </summary>
        public long TotalCount
        {
            get { return totalCount; }
            set
            {
                if (totalCount == value) { return; }

                totalCount = value;
                IsTimeVisible = totalCount > 0;

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsTimeVisible));
            }
        }
        private long totalCount;

        /// <summary>
        /// Gets or sets a passed count of fixtures.
        /// </summary>
        public long PassedCount
        {
            get { return passedCount; }
            set
            {
                if (passedCount == value) { return; }

                passedCount = value;
                PassedRate = TotalCount == 0 ? 0 : (int)((double)passedCount / TotalCount * 100);

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(PassedRate));
            }
        }
        private long passedCount;

        /// <summary>
        /// Gets or sets a failed count of fixtures.
        /// </summary>
        public long FailedCount
        {
            get { return failedCount; }
            set
            {
                if (failedCount == value) { return; }

                failedCount = value;
                RaisePropertyChanged();
            }
        }
        private long failedCount;

        /// <summary>
        /// Gets or sets a pending count of fixtures.
        /// </summary>
        public long PendingCount
        {
            get { return pendingCount; }
            set
            {
                if (pendingCount == value) { return; }

                pendingCount = value;
                RaisePropertyChanged();
            }
        }
        private long pendingCount;

        /// <summary>
        /// Gets or sets a passed rate of fixtures.
        /// </summary>
        public int PassedRate { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether a time text is visible.
        /// </summary>
        public bool IsTimeVisible { get; private set; }

        /// <summary>
        /// Gets or sets a start date time of fixture running.
        /// </summary>
        public string StartDateTime
        {
            get { return startDateTime; }
            set
            {
                if (startDateTime == value) { return; }

                startDateTime = value;
                RaisePropertyChanged();
            }
        }
        private string startDateTime;

        /// <summary>
        /// Gets or sets an end date time of fixture running.
        /// </summary>
        public string EndDateTime
        {
            get { return endDateTime; }
            set
            {
                if (endDateTime == value) { return; }

                endDateTime = value;
                RaisePropertyChanged();
            }
        }
        private string endDateTime;

        /// <summary>
        /// Gets or sets a duration of fixture running.
        /// </summary>
        public string Duration
        {
            get { return duration; }
            set
            {
                if (duration == value) { return; }

                duration = value;
                RaisePropertyChanged();
            }
        }
        private string duration;

        /// <summary>
        /// Gets or sets a value that indicates whether a fixture is building.
        /// </summary>
        public bool IsFixtureBuilding
        {
            get { return isFixtureBuilding; }
            set
            {
                if (isFixtureBuilding == value) { return; }

                isFixtureBuilding = value;
                RaisePropertyChanged();
            }
        }
        private bool isFixtureBuilding;

        /// <summary>
        /// Gets or sets a value that indicates whether a fixture is built.
        /// </summary>
        public bool IsFixtureBuilt
        {
            get { return isFixtureBuilt; }
            set
            {
                if (isFixtureBuilt == value) { return; }

                isFixtureBuilt = value;
                RaisePropertyChanged();
            }
        }
        private bool isFixtureBuilt;

        /// <summary>
        /// Gets or sets a value that indicates whether a fixture is running.
        /// </summary>
        public bool IsFixtureRunning
        {
            get { return isFixtureRunning; }
            set
            {
                if (isFixtureRunning == value) { return; }

                isFixtureRunning = value;
                RaisePropertyChanged();
            }
        }
        private bool isFixtureRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureSummary"/> class.
        /// </summary>
        public FixtureSummary()
        {
        }

        /// <summary>
        /// Sets the state when the fixture building is starting.
        /// </summary>
        public void OnFixtureBuildingStarting()
        {
            TotalCount = 0;
            PassedCount = 0;
            FailedCount = 0;
            PendingCount = 0;

            StartDateTime = null;
            EndDateTime = null;
            Duration = null;

            IsFixtureBuilding = true;
            IsFixtureBuilt = false;
            IsFixtureRunning = false;
        }

        /// <summary>
        /// Sets the state when the fixture building is completed.
        /// </summary>
        /// <param name="dateTime">The date time to complete building fixtures.</param>
        public void OnFixtureBuildingCompleted(DateTime dateTime)
        {
            IsFixtureBuilding = false;
            IsFixtureBuilt = true;
            StartDateTime = dateTime.ToString("u");
        }

        /// <summary>
        /// Sets the state when the fixture running is starting.
        /// </summary>
        public void OnFixtureRunningStarting()
        {
            IsFixtureRunning = true;
        }

        /// <summary>
        /// Sets the state when the fixture running is completed.
        /// </summary>
        /// <param name="results">The results of the fixture running.</param>
        public void OnFixtureRunningCompleted(IList<FixtureResult> results)
        {
            StartDateTime = results.StartTime().ToString("u");
            EndDateTime = results.EndTime().ToString("u");
            Duration = $"{(results.EndTime() - results.StartTime()).TotalSeconds:0.000} seconds";

            IsFixtureRunning = false;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event that occurs when the value of the specified property name changes.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value changes.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    }
}
