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
using Thorner.RadialControls.Utilities.Extensions;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Thorner.RadialControls.Controls
{
    public class HaloSlice : Path
    {
        #region Dependency Properties

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(HaloSlice), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(HaloSlice), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty SpreadProperty = DependencyProperty.Register(
            "Spread", typeof(double), typeof(HaloSlice), new PropertyMetadata(360.0, Refresh));

        #endregion

        private PathGeometry path = new PathGeometry();
        private EllipseGeometry ellipse = new EllipseGeometry();

        private LineSegment sliceStart = new LineSegment();
        private LineSegment sliceEnd = new LineSegment();

        private ArcSegment arcSegment = new ArcSegment();
        private PathFigure arcFigure = new PathFigure();

        public HaloSlice()
        {
            arcSegment.SweepDirection = SweepDirection.Clockwise;

            arcFigure.Segments = new PathSegmentCollection
            {
                sliceStart, arcSegment, sliceEnd
            };

            path.Figures = new PathFigureCollection { arcFigure };

            Data = ellipse;

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
            ArrangeEllipse(circle);

            return finalSize;
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            var slice = (HaloSlice)o;

            if (Math.Round(slice.Spread / 360) != 0)
            {
                slice.Data = slice.ellipse;
            }
            else
            {
                slice.Data = slice.path;
            }
        }

        #endregion

        #region Private Members

        private void ArrangePath(Circle circle)
        {
            arcFigure.StartPoint = circle.Center;

            sliceStart.Point = circle.PointAt(Angle + Offset);
            sliceEnd.Point = circle.Center;

            arcSegment.Point = circle.PointAt(Angle + Offset + Spread);
            arcSegment.Size = circle.Size();
        }

        private void ArrangeEllipse(Circle circle)
        {
            ellipse.Center = circle.Center;
            ellipse.RadiusX = circle.Radius;
            ellipse.RadiusY = circle.Radius;
        }

        #endregion
    }
}
