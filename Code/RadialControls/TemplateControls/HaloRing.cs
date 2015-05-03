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
            "Angle", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0));

        public static readonly DependencyProperty OriginProperty = DependencyProperty.RegisterAttached(
            "Origin", typeof(double), typeof(HaloRing), new PropertyMetadata(0.0));

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

        public double Thickness
        {
            get
            {
                if (Children.Count == 0) return 0.0;

                return Children.Max(child => Math.Max(
                    child.DesiredSize.Width, child.DesiredSize.Height
                ));
            }
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
            var length = Math.Min(
                availableSize.Width, availableSize.Height
            );

            foreach (var child in Children)
            {
                child.Measure(new Size(length / 2, length / 2));
            }

            return new Size(length, length);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var length = Math.Min(
                finalSize.Width, finalSize.Height
            );

            var ringRadius = (length - Thickness) / 2;

            foreach (var child in Children)
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

                var topLeft = new Point(
                    (length - child.DesiredSize.Width) / 2,
                    (length - child.DesiredSize.Height) / 2
                );

                child.Arrange(new Rect(topLeft, child.DesiredSize));
            }

            return new Size(length, length);
        }

        #endregion
    }
}
