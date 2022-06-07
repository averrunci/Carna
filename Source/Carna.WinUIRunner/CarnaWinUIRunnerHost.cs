// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Carna.Runner;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a host of the CarnaWinUIRunner.
/// </summary>
public class CarnaWinUIRunnerHost : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets a title of CarnaWinUIRunner.
    /// </summary>
    public string Title => $"{typeof(CarnaWinUIRunner).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product} {typeof(CarnaWinUIRunner).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version}";

    /// <summary>
    /// Gets a fixture summary.
    /// </summary>
    public FixtureSummary Summary { get; } = new();

    /// <summary>
    /// Gets fixture contents.
    /// </summary>
    public ObservableCollection<FixtureContent> Fixtures { get; } = new();

    /// <summary>
    /// Gets or sets an error message that occurs while CarnaWinUIRunner is running.
    /// </summary>
    public string ErrorMessage
    {
        get => errorMessage;
        set
        {
            if (errorMessage == value) return;

            errorMessage = value;
            RaisePropertyChanged();
        }
    }
    private string errorMessage = string.Empty;

    /// <summary>
    /// Gets a value that indicates whether to exit the application automatically
    /// after the running of CarnaWinUIRunner is completed.
    /// </summary>
    public bool AutoExit { get; set; }

    /// <summary>
    /// Gets or sets a formatter of a fixture.
    /// </summary>
    public IFixtureFormatter Formatter { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarnaWinUIRunnerHost"/> class
    /// with the specified formatter.
    /// </summary>
    /// <param name="formatter">The formatter of a fixture.</param>
    public CarnaWinUIRunnerHost(IFixtureFormatter formatter) => Formatter = formatter;

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