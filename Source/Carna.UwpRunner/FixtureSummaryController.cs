// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

using Fievus.Windows.Mvc;

namespace Carna.UwpRunner
{
    /// <summary>
    /// Provides the function to handle events on the view of <see cref="FixtureSummary"/>.
    /// </summary>
    public class FixtureSummaryController
    {
        private const double passedRateRadius = 40;

        /// <summary>
        /// Gets or sets <see cref="FixtureSummary"/>.
        /// </summary>
        [DataContext]
        public FixtureSummary FixtureSummary { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Path"/> that indicates a passed rate.
        /// </summary>
        [Element]
        public Path PassedRatePath { get; set; }

        [EventHandler(Event = "Loaded")]
        private void OnLoaded()
        {
            FixtureSummary.PassedRate.PropertyValueChanged += (s, e) =>
                PassedRatePath.Data = e.NewValue < 100 ? CreatePathGeometry(e.NewValue) : CreateEllipseGeometry();
        }

        private Geometry CreateEllipseGeometry()
            => new EllipseGeometry
            {
                Center = new Point(passedRateRadius, passedRateRadius),
                RadiusX = passedRateRadius,
                RadiusY = passedRateRadius
            };

        private Geometry CreatePathGeometry(int passedRate)
        {
            var figure = new PathFigure
            {
                IsClosed = true,
                StartPoint = new Point(passedRateRadius, 0)
            };

            var angle = 2 * Math.PI * passedRate / 100;
            figure.Segments.Add(new ArcSegment
            {
                Size = new Size(passedRateRadius, passedRateRadius),
                Point = new Point(
                    passedRateRadius + passedRateRadius * Math.Cos(angle - Math.PI / 2),
                    passedRateRadius + passedRateRadius * Math.Sin(angle - Math.PI / 2)
                ),
                IsLargeArc = passedRate >= 50,
                SweepDirection = SweepDirection.Clockwise
            });

            figure.Segments.Add(new LineSegment
            {
                Point = new Point(passedRateRadius, passedRateRadius)
            });

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
