// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a view for FixtureContent.
    /// </summary>
    public sealed partial class FixtureContentView
    {
        private FixtureContent Fixture => DataContext as FixtureContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureContentView"/> class.
        /// </summary>
        public FixtureContentView()
        {
            InitializeComponent();
        }

        private void OnStatusEllipsePointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Fixture.IsChildOpenTextVisible = true;

            VisualStateManager.GoToState(this, "PointerEntered", false);
        }

        private void OnStatusEllipsePointerExited(object sender, PointerRoutedEventArgs e)
        {
            Fixture.IsChildOpenTextVisible = false;

            VisualStateManager.GoToState(this, "PointerExited", false);
        }

        private void OnStatusEllipseTapped(object sender, TappedRoutedEventArgs e)
        {
            Fixture.IsChildOpen = !Fixture.IsChildOpen;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is FixtureContent fixtureContent)) return;

            if (fixtureContent.IsFirstFailed)
            {
                await StartBringContentIntoView();
            }
            else
            {
                fixtureContent.PropertyChanged += OnFixtureContentPropertyChanged;
            }
        }

        private async void OnFixtureContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(FixtureContent.IsFirstFailed)) return;

            await StartBringContentIntoView();
        }

        private async Task StartBringContentIntoView()
            => await Dispatcher.RunIdleAsync(args => StartBringIntoView(new BringIntoViewOptions { TargetRect = new Rect(0, 0, ActualWidth, ActualHeight) }));
    }
}
