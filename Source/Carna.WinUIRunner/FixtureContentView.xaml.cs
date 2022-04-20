// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Windows.Foundation;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a view for <see cref="FixtureContent"/>.
/// </summary>
public sealed partial class FixtureContentView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureContentView"/> class.
    /// </summary>
    public FixtureContentView()
    {
        InitializeComponent();
    }

    private void OnStatusEllipsePointerEntered(object? sender, PointerRoutedEventArgs e)
    {
        if (DataContext is not FixtureContent fixture) return;

        fixture.IsChildOpenTextVisible = true;

        VisualStateManager.GoToState(this, "StatusPointerEntered", false);
    }

    private void OnStatusEllipsePointerExited(object? sender, PointerRoutedEventArgs e)
    {
        if (DataContext is not FixtureContent fixture) return;

        fixture.IsChildOpenTextVisible = false;

        VisualStateManager.GoToState(this, "StatusPointerExited", false);
    }

    private void OnStatusEllipseTapped(object? sender, TappedRoutedEventArgs e)
    {
        if (DataContext is not FixtureContent fixture) return;

        fixture.IsChildOpen = !fixture.IsChildOpen;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not FixtureContent fixtureContent) return;

        if (fixtureContent.IsFirstFailed)
        {
            StartBringContentIntoView();
        }
        else
        {
            fixtureContent.PropertyChanged += OnFixtureContentPropertyChanged;
        }
    }

    private void OnFixtureContentPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(FixtureContent.IsFirstFailed)) return;

        StartBringContentIntoView();
    }

    private void StartBringContentIntoView()
        => DispatcherQueue.TryEnqueue(
            DispatcherQueuePriority.Normal,
            () => StartBringIntoView(new BringIntoViewOptions { TargetRect = new Rect(0, 0, ActualWidth, ActualHeight) })
        );

}