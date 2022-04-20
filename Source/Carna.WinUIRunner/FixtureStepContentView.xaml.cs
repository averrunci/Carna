// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Windows.Foundation;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a view for <see cref="FixtureStepContent"/>.
/// </summary>
public sealed partial class FixtureStepContentView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureStepContentView"/> class.
    /// </summary>
    public FixtureStepContentView()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not FixtureStepContent fixtureStepContent) return;

        if (fixtureStepContent.IsFirstFailed)
        {
            StartBringContentIntoView();
        }
        else
        {
            fixtureStepContent.PropertyChanged += OnFixtureStepContentPropertyChanged;
        }
    }

    private void OnFixtureStepContentPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(FixtureStepContent.IsFirstFailed)) return;

        StartBringContentIntoView();
    }

    private void StartBringContentIntoView()
        => DispatcherQueue.TryEnqueue(
            DispatcherQueuePriority.Normal,
            () => StartBringIntoView(new BringIntoViewOptions { TargetRect = new Rect(0, 0, ActualWidth, ActualHeight) })
        );

}