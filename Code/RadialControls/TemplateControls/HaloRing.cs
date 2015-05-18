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

            var length = Math.Min(
                availableSize.Width, availableSize.Height
            );

            var thickness = CalculateThickness(Children);
            SetValue(Halo.ThicknessProperty, new Thickness(thickness));
            return new Size(length, length);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var radius = Math.Min(
                finalSize.Width, finalSize.Height
            ) / 2;

            foreach(var child in Children)
            {
                var topLeft = new Point(
                    radius - child.DesiredSize.Width / 2,
                    radius - child.DesiredSize.Height / 2
                );

                child.Arrange(
                    new Rect(topLeft, child.DesiredSize)
                );
            }

            var thickness = CalculateThickness(Children);
            SetValue(Halo.ThicknessProperty, new Thickness(thickness));
            var ringRadius = radius - thickness / 2;

            foreach (var child in Children)
            {
                TransformChild(child, ringRadius);
            }

            return new Size(radius * 2, radius * 2);
        }

        #endregion

        #region Private Members

        private void TransformChild(UIElement child, double ringRadius)
        {
            var origin = GetOrigin(child).ToRadians();

            child.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform { 
                            X = ringRadius * Math.Sin(origin), 
                            Y = -ringRadius * Math.Cos(origin)
                        },
                        new RotateTransform { Angle = GetAngle(child) }
                    }
            };

            child.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        private double CalculateThickness(UIElementCollection children)
        {
            if (children.Count == 0) return 0.0;

            return children.Max(child =>
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
