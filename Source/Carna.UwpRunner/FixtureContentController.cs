// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Fievus.Windows.Mvc;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Provides the function to handle events on the view of <see cref="FixtureContent"/>.
    /// </summary>
    public class FixtureContentController
    {
        /// <summary>
        /// Gets or sets <see cref="FixtureContent"/>.
        /// </summary>
        [DataContext]
        public FixtureContent Content { get; set; }

        /// <summary>
        /// Gets or sets <see cref="UserControl"/> that is the root control on the view.
        /// </summary>
        [Element]
        public UserControl Root { get; set; }

        [EventHandler(ElementName = "StatusEllipse", Event = "PointerEntered")]
        private void OnStatusEllipsePointerEntered()
        {
            Content.IsChildOpenTextVisible.Value = true;

            VisualStateManager.GoToState(Root, "PointerEntered", false);
        }

        [EventHandler(ElementName = "StatusEllipse", Event = "PointerExited")]
        private void OnStatusEllipsePointerExited()
        {
            Content.IsChildOpenTextVisible.Value = false;

            VisualStateManager.GoToState(Root, "PointerExited", false);
        }

        [EventHandler(ElementName = "StatusEllipse", Event = "Tapped")]
        private void OnStatusEllipseTapped()
        {
            Content.IsChildOpen.Value = !Content.IsChildOpen.Value;
        }
    }
}
