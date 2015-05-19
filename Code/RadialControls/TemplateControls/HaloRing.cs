using System;
using System.Linq;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;
using Thorner.RadialControls.ViewModels;

namespace Thorner.RadialControls.TemplateControls
{
    public class HaloRing : Panel
    {
        #region Dependency Properties

        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty OriginProperty = DependencyProperty.RegisterAttached(
            "Origin", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0, Refresh));

        #endregion

        #region Properties

        public static double GetOrigin(DependencyObject o)
        {
            return (double)o.GetValue(HaloRing.OriginProperty);
        }

        public static void SetOrigin(DependencyObject o, double value)
        {
            o.SetValue(HaloRing.OriginProperty, value);
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

            var thickness = RingThickness();

            SetValue(
                Halo.ThicknessProperty, new Thickness(thickness)
            );

            return RingSize(availableSize, thickness);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var thickness = RingThickness();

            var size = RingSize(finalSize, thickness);

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
            var origin = GetOrigin(child).ToRadians();

            child.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform 
                        { 
                            X = radius * Math.Sin(origin), 
                            Y = -radius * Math.Cos(origin)
                        },
                        new RotateTransform { Angle = GetAngle(child) }
                    }
            };

            child.RenderTransformOrigin = new Point(0.5, 0.5);
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

        private Size RingSize(Size size, double thickness)
        {
            return new Size(
                size.Width < thickness ? thickness : size.Width,
                size.Height < thickness ? thickness : size.Height
            );
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
