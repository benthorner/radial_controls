using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Thorner.RadialControls.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Thorner.RadialControls.TemplateControls
{
    public class Ring : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty InsetsProperty = DependencyProperty.Register(
            "Content", typeof(List<UIElement>), typeof(Ring), new PropertyMetadata(new List<UIElement>()));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle", typeof(double), typeof(Ring), new PropertyMetadata(0.0));

        public static readonly DependencyProperty OriginProperty = DependencyProperty.RegisterAttached(
            "Origin", typeof(double), typeof(Ring), new PropertyMetadata(0.0));

        #endregion

        private Grid _grid;

        public Ring()
        {
            DefaultStyleKey = typeof(Ring);
        }

        #region Properties

        public static double GetOrigin(DependencyObject o)
        {
            return (double)o.GetValue(Ring.OriginProperty);
        }

        public static void SetOrigin(DependencyObject o, double value)
        {
            o.SetValue(Ring.OriginProperty, value);
        }

        public List<UIElement> Insets
        {
            get { return (List<UIElement>)GetValue(InsetsProperty); }
            set { SetValue(InsetsProperty, value); }
        }

        public static void SetAngle(DependencyObject o, double value)
        {
            o.SetValue(Ring.AngleProperty, value);
        }

        public static double GetAngle(DependencyObject o)
        {
            return (double)o.GetValue(Ring.AngleProperty);
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _grid = GetTemplateChild("PART_Grid") as Grid;

            foreach(var child in Insets)
            {
                _grid.Children.Add(child);
            }

            _grid.Children.Add((UIElement) Content);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(availableSize);

            foreach(var child in Insets)
            {
                child.Measure(
                    new Size(Double.PositiveInfinity, Double.PositiveInfinity)
                );
            }

            var ringThickness = RingThickness();

            if (Content != null)
            {
                ((UIElement) Content).Measure(
                    new Size(Double.PositiveInfinity, Double.PositiveInfinity)
                );

                ringThickness += Math.Max(
                    ((UIElement)Content).DesiredSize.Width, ((UIElement)Content).DesiredSize.Height
                );
            }

            return new Size(ringThickness, ringThickness);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var ringThickness = RingThickness();
            var thickness = Math.Min(finalSize.Width, finalSize.Height);
            var ringRadius = (thickness - ringThickness) / 2;

            foreach(var child in Insets)
            {
                var origin = (double)child.GetValue(Ring.OriginProperty);
                var angle = (double)child.GetValue(Ring.AngleProperty);

                child.RenderTransformOrigin = new Point(0.5, 0.5);

                child.RenderTransform = new TransformGroup
                {
                    Children = new TransformCollection
                    {
                        new TranslateTransform { 
                            X = ringRadius * Math.Sin(DegreeToRadian(origin)), 
                            Y = -ringRadius * Math.Cos(DegreeToRadian(origin))
                        },
                        new RotateTransform { Angle = angle }
                    }
                };

                var topLeft = new Point(
                    (finalSize.Width - child.DesiredSize.Width) / 2,
                    (finalSize.Height - child.DesiredSize.Height) / 2
                );

                child.Arrange(new Rect(topLeft, child.DesiredSize));
            }

            if (Content != null)
            {
                var topLeft = new Point(
                    (finalSize.Width - thickness + ringThickness) / 2,
                    (finalSize.Height - thickness + ringThickness) / 2
                );

                var size = new Size(
                   thickness - ringThickness, thickness - ringThickness
                );

                ((UIElement)Content).Arrange(new Rect(0,0,500,500));
            }

            return base.ArrangeOverride(finalSize);
        }

        #endregion

        #region Private Members

        private double RingThickness()
        {
            return Insets.Max((child) =>
            {
                return Math.Max(
                    child.DesiredSize.Width, child.DesiredSize.Height
                );
            });
        }

        private double DegreeToRadian(double degree)
        {
            return degree / 180 * Math.PI;
        }

        #endregion
    }
}
