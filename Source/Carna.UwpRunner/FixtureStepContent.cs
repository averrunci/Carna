// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Fievus.Windows.Mvc.Bindings;

using Carna.Runner.Step;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a content of a fixture step.
    /// </summary>
    public class FixtureStepContent
    {
        /// <summary>
        /// Gets a description of a fixture step.
        /// </summary>
        public ObservableProperty<string> Description { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets a status of a fixture step running.
        /// </summary>
        public ObservableProperty<FixtureStepStatus> Status { get; } = FixtureStepStatus.Ready.ToObservableProperty();

        /// <summary>
        /// Gets a duration of a fixture step running.
        /// </summary>
        public ObservableProperty<string> Duration { get; } = string.Empty.ToObservableProperty();

        /// <summary>
        /// Gets an exception that occurred while a fixture step was running.
        /// </summary>
        public ObservableProperty<string> Exception { get; } = new ObservableProperty<string>();
    }
}
