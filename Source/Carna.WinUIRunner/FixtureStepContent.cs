// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Carna.Runner;
using Carna.Runner.Step;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a content of a fixture step.
/// </summary>
public class FixtureStepContent : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets a description of a fixture step.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets a status of a fixture step running.
    /// </summary>
    public FixtureStepStatus Status { get; }

    /// <summary>
    /// Gets a duration of a fixture step running.
    /// </summary>
    public string Duration { get; }

    /// <summary>
    /// Gets an exception that occurred while a fixture step was running.
    /// </summary>
    public string Exception { get; }

    /// <summary>
    /// Gets a value that indicates whether the step is first failed.
    /// </summary>
    public bool IsFirstFailed
    {
        get => isFirstFailed;
        set
        {
            if (isFirstFailed == value) return;

            isFirstFailed = value;
            RaisePropertyChanged();
        }
    }
    private bool isFirstFailed;

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureStepContent"/> class
    /// with the specified result of the fixture step running and the formatter
    /// to format the description of the fixture step.
    /// </summary>
    /// <param name="result">The result of the fixture step running.</param>
    /// <param name="formatter">The formatter to format the description of the fixture step.</param>
    public FixtureStepContent(FixtureStepResult result, IFixtureFormatter formatter)
    {
        Description = formatter.FormatFixtureStep(result.Step).ToString();
        Status = result.Status;
        Duration = result.Duration.HasValue ? $"{result.Duration.Value.TotalSeconds:0.000} s" : string.Empty;
        Exception = result.Exception?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event that occurs when the value of the specified property name changes.
    /// </summary>
    /// <param name="propertyName">The name of the property whose value changes.</param>
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
}