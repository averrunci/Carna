// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;

using Fievus.Windows.Mvc.Bindings;

using Carna.Runner;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a content of a fixture.
    /// </summary>
    public class FixtureContent
    {
        /// <summary>
        /// Gets a description of a fixture.
        /// </summary>
        public ObservableProperty<string> Description { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets a status of a fixture running.
        /// </summary>
        public ObservableProperty<FixtureStatus> Status { get; } = FixtureStatus.Ready.ToObservableProperty();

        /// <summary>
        /// Gets a duration of a fixture running.
        /// </summary>
        public ObservableProperty<string> Duration { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets an exception that occurred while a fixture was running.
        /// </summary>
        public ObservableProperty<string> Exception { get; } = new ObservableProperty<string>();

        /// <summary>
        /// Gets a value that indicates whether a fixture is running.
        /// </summary>
        public ObservableProperty<bool> IsFixtureRunning { get; } = false.ToObservableProperty();

        /// <summary>
        /// Gets a value that indicates whether a fixture status is visible.
        /// </summary>
        public ObservableProperty<bool> IsFixtureStatusVisible { get; } = true.ToObservableProperty();

        /// <summary>
        /// Gets fixture contents.
        /// </summary>
        public ObservableCollection<FixtureContent> Fixtures { get; } = new ObservableCollection<FixtureContent>();

        /// <summary>
        /// Gets fixture step contents.
        /// </summary>
        public ObservableCollection<FixtureStepContent> Steps { get; } = new ObservableCollection<FixtureStepContent>();

        /// <summary>
        /// Gets a value that indicates whether the child content is open.
        /// </summary>
        public ObservableProperty<bool> IsChildOpen { get; } = false.ToObservableProperty();

        /// <summary>
        /// Gets a value that indicates whether a text that represents whether the child content is open is visible.
        /// </summary>
        public ObservableProperty<bool> IsChildOpenTextVisible { get; } = false.ToObservableProperty();

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContent"/> class.
        /// </summary>
        public FixtureContent()
        {
            IsFixtureRunning.Bind(Status, status => status == FixtureStatus.Running);
            IsFixtureStatusVisible.Bind(Status, status => status != FixtureStatus.Running);
        }
    }
}
