// Copyright (C) 2017-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a view for FixtureStepContent.
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

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is FixtureStepContent fixtureStepContent)) return;

            if (fixtureStepContent.IsFirstFailed)
            {
                await StartBringContentIntoView();
            }
            else
            {
                fixtureStepContent.PropertyChanged += OnFixtureStepContentPropertyChanged;
            }
        }

        private async void OnFixtureStepContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(FixtureStepContent.IsFirstFailed)) return;

            await StartBringContentIntoView();
        }

        private async Task StartBringContentIntoView()
            => await Dispatcher.RunIdleAsync(args => StartBringIntoView(new BringIntoViewOptions { TargetRect = new Rect(0, 0, ActualWidth, ActualHeight) }));
    }
}
