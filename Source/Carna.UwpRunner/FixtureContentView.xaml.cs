﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a view for FixtureContent.
    /// </summary>
    public sealed partial class FixtureContentView : UserControl
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
    }
}