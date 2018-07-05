// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Represents a view for FixtureSummary.
    /// </summary>
    public sealed partial class FixtureSummaryView : UserControl
    {
        private const double PassedRateRadius = 40;

        private FixtureSummary Summary => DataContext as FixtureSummary;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureSummaryView"/>.
        /// </summary>
        public FixtureSummaryView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Summary.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == nameof(FixtureSummary.PassedRate))
                {
                    PassedRatePath.Data = Summary.PassedRate < 100 ? CreatePathGeometry(Summary.PassedRate) : CreateEllipseGeometry();
                }
            };
        }

        private Geometry CreateEllipseGeometry()
            => new EllipseGeometry
            {
                Center = new Point(PassedRateRadius, PassedRateRadius),
                RadiusX = PassedRateRadius,
                RadiusY = PassedRateRadius
            };

        private Geometry CreatePathGeometry(int passedRate)
        {
            var figure = new PathFigure
            {
                IsClosed = true,
                StartPoint = new Point(PassedRateRadius, 0)
            };

            var angle = 2 * Math.PI * passedRate / 100;
            figure.Segments.Add(new ArcSegment
            {
                Size = new Size(PassedRateRadius, PassedRateRadius),
                Point = new Point(
                    PassedRateRadius + PassedRateRadius * Math.Cos(angle - Math.PI / 2),
                    PassedRateRadius + PassedRateRadius * Math.Sin(angle - Math.PI / 2)
                ),
                IsLargeArc = passedRate >= 50,
                SweepDirection = SweepDirection.Clockwise
            });

            figure.Segments.Add(new LineSegment
            {
                Point = new Point(PassedRateRadius, PassedRateRadius)
            });

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
