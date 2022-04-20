// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Carna.WinUIRunner;

/// <summary>
/// Represents a view for <see cref="FixtureSummary"/>.
/// </summary>
public sealed partial class FixtureSummaryView
{
    private const double PassedRateRadius = 40;

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureSummaryView"/> class.
    /// </summary>
    public FixtureSummaryView()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not FixtureSummary summary) return;

        summary.PropertyChanged += OnFixtureSummaryPropertyChanged;
    }

    private void OnFixtureSummaryPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not FixtureSummary summary) return;
        if (e.PropertyName is not nameof(FixtureSummary.PassedRate)) return;

        PassedRatePath.Data = summary.PassedRate < 100 ? CreatePathGeometry(summary.PassedRate) : CreateEllipseGeometry();
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