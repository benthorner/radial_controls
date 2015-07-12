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
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 **/

using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Thorner.RadialControls.Utilities.Extensions;

namespace Thorner.RadialControls.Controls
{
    public class HaloRing : Panel
    {
        #region Dependency Properties

        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.RegisterAttached(
            "Offset", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0, Refresh));

        #endregion

        #region Properties

        public static double GetOffset(DependencyObject o)
        {
            return (double)o.GetValue(HaloRing.OffsetProperty);
        }

        public static void SetOffset(DependencyObject o, double value)
        {
            o.SetValue(HaloRing.OffsetProperty, value);
        }

        public static void SetAngle(DependencyObject o, double value)
        {
            o.SetValue(HaloRing.AngleProperty, value);
        }

        public static double GetAngle(DependencyObject o)
        {
            return (double)o.GetValue(HaloRing.AngleProperty);
        }

        #endregion

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in Children)
            {
                child.Measure(new Size(
                    Double.PositiveInfinity, Double.PositiveInfinity
                ));
            }

            SetValue(
                Halo.ThicknessProperty, RingThickness()
            );

            return RingSize(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var thickness = RingThickness();
            var size = RingSize(finalSize);

            var radius = (
                Math.Min(size.Width, size.Height) - thickness
            ) / 2;

            foreach(var child in Children)
            {
                ArrangeChild(child, size);
                TransformChild(child, radius);
            }

            return size;
        }

        #endregion

        #region Private Members

        private void ArrangeChild(UIElement child, Size size)
        {
            var topLeft = new Point(
                (size.Width - child.DesiredSize.Width) / 2,
                (size.Height - child.DesiredSize.Height) / 2
            );

            child.Arrange(new Rect(topLeft, child.DesiredSize));
        }

        private void TransformChild(UIElement child, double radius)
        {
            var offset = GetOffset(child).ToRadians();

            child.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform 
                        { 
                            X = radius * Math.Sin(offset), 
                            Y = -radius * Math.Cos(offset)
                        },
                        new RotateTransform { Angle = GetAngle(child) }
                    }
            };

            child.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        private Size RingSize(Size size)
        {
            var thickness = RingThickness();

            return new Size(
                size.Width < thickness ? thickness : size.Width,
                size.Height < thickness ? thickness : size.Height
            );
        }

        private double RingThickness()
        {
            if (Children.Count == 0) return 0.0;

            return Children.Max(child =>
            {
                return Math.Max(
                    child.DesiredSize.Width, child.DesiredSize.Height
                );
            });
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            var element = o as FrameworkElement;
            if (element == null) return;

            var parent = element.Parent as HaloRing;
            if (parent == null) return;

            parent.InvalidateMeasure();
            parent.UpdateLayout();
        }

        #endregion
    }
}
