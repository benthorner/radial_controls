/**
 *  RadialControls - A circular controls library for Windows 8 Apps
 *  Copyright (C) Ben Thorner 2015
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.If not, see <http://www.gnu.org/licenses/>.
 **/

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Thorner.RadialControls.Utilities.Extensions;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Controls
{
    public class HaloArc : Path
    {
        #region Dependency Properties

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty SpreadProperty = DependencyProperty.Register(
            "Spread", typeof(double), typeof(HaloArc), new PropertyMetadata(360.0, Refresh));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty TensionProperty = DependencyProperty.Register(
            "Tension", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        #endregion

        private PathFigure figure = new PathFigure();
        private ArcSegment segment = new ArcSegment();
        private PathGeometry path = new PathGeometry();

        public HaloArc()
        {
            segment.SweepDirection = SweepDirection.Clockwise;
            figure.Segments = new PathSegmentCollection { segment };
            path.Figures = new PathFigureCollection { figure };

            Data = path;

            BindingOperations.SetBinding(this, Halo.ThicknessProperty,
                new Binding
                {
                    Source = this, Mode = BindingMode.TwoWay,
                    Path = new PropertyPath("StrokeThickness"),
                });
        }

        #region Properties

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public double Spread
        {
            get { return (double)GetValue(SpreadProperty); }
            set { SetValue(SpreadProperty, value); }
        }

        public double Tension
        {
            get { return (double)GetValue(TensionProperty); }
            set { SetValue(TensionProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var circle = new Circle(finalSize);
            circle.Radius -= StrokeThickness / 2;

            ArrangePath(circle);

            return finalSize;
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            ((HaloArc)o).InvalidateMeasure();
            ((HaloArc)o).UpdateLayout();
        }

        #endregion

        #region Private Members

        private void ArrangePath(Circle circle)
        {
            var tension = Tension % 1;
            var angle = Angle + Offset;

            var startAngle = angle - tension * Spread;
            var endAngle = angle + (1 - tension) * Spread;

            figure.StartPoint = circle.PointAt(startAngle);
            segment.Point = circle.PointAt(endAngle);

            segment.Size = circle.Size();
        }

        #endregion
    }
}
