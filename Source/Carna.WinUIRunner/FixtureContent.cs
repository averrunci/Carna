// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Carna.Runner;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a content of a fixture.
/// </summary>
public class FixtureContent : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets a description of a fixture.
    /// </summary>
    public string Description
    {
        get => description;
        set
        {
            if (description == value) return;

            description = value;
            RaisePropertyChanged();
        }
    }
    private string description = string.Empty;

    /// <summary>
    /// Gets or sets a status of a fixture running.
    /// </summary>
    public FixtureStatus Status
    {
        get => status;
        set
        {
            if (status == value) return;

            status = value;
            IsFixtureRunning = status is FixtureStatus.Running;
            IsFixtureStatusVisible = status is not FixtureStatus.Running;

            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsFixtureRunning));
            RaisePropertyChanged(nameof(IsFixtureStatusVisible));
        }
    }
    private FixtureStatus status;

    /// <summary>
    /// Gets or sets a duration of a fixture running.
    /// </summary>
    public string Duration
    {
        get => duration;
        set
        {
            if (duration == value) return;

            duration = value;
            RaisePropertyChanged();
        }
    }
    private string duration = string.Empty;

    /// <summary>
    /// Gets or sets an exception that occurred while a fixture was running.
    /// </summary>
    public string Exception
    {
        get => exception;
        set
        {
            if (exception == value) return;

            exception = value;
            RaisePropertyChanged();
        }
    }
    private string exception = string.Empty;

    /// <summary>
    /// Gets or sets a value that indicates whether a fixture is running.
    /// </summary>
    public bool IsFixtureRunning { get; private set; }

    /// <summary>
    /// Gets a value that indicates whether a fixture status is visible.
    /// </summary>
    public bool IsFixtureStatusVisible { get; private set; } = true;

    /// <summary>
    /// Gets fixture contents.
    /// </summary>
    public ObservableCollection<FixtureContent> Fixtures { get; } = new();

    /// <summary>
    /// Gets fixture step contents.
    /// </summary>
    public ObservableCollection<FixtureStepContent> Steps { get; } = new();

    /// <summary>
    /// Gets or sets a value that indicates whether the child content is open.
    /// </summary>
    public bool IsChildOpen
    {
        get => isChildOpen;
        set
        {
            if (isChildOpen == value) return;

            isChildOpen = value;
            RaisePropertyChanged();
        }
    }
    private bool isChildOpen;

    /// <summary>
    /// Gets or sets a value that indicates whether a text that represents whether the child content is open is visible.
    /// </summary>
    public bool IsChildOpenTextVisible
    {
        get => isChildOpenTextVisible;
        set
        {
            if (isChildOpenTextVisible == value) return;

            isChildOpenTextVisible = value;
            RaisePropertyChanged();
        }
    }
    private bool isChildOpenTextVisible;

    /// <summary>
    /// Gets a value that indicates whether the fixture is first failed.
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
    /// Initializes a new instance of the <see cref="FixtureContent"/> class.
    /// </summary>
    public FixtureContent()
    {
    }

    /// <summary>
    /// Sets the state when the fixture running is completed.
    /// </summary>
    /// <param name="result">The result of the fixture running.</param>
    /// <param name="formatter">The formatter to format the description of the fixture.</param>
    public void OnFixtureRunningCompleted(FixtureResult result, IFixtureFormatter formatter)
    {
        Description = formatter.FormatFixture(result.FixtureDescriptor).ToString();
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