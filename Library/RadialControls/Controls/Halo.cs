﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.Controls
{
    public class Halo : Panel
    {
        #region Dependency Properties

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            "Thickness", typeof(double), typeof(Halo), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty BandProperty = DependencyProperty.RegisterAttached(
            "Band", typeof(int), typeof(Halo), new PropertyMetadata(0, Refresh));

        #endregion

        #region Properties

        public static double GetThickness(DependencyObject o)
        {
            return (double)o.GetValue(ThicknessProperty);
        }

        public static void SetThickness(DependencyObject o, double value)
        {
            o.SetValue(ThicknessProperty, value);
        }

        public static int GetBand(DependencyObject o)
        {
            return (int)o.GetValue(BandProperty);
        }

        public static void SetBand(DependencyObject o, int value)
        {
            o.SetValue(BandProperty, value);
        }

        #endregion

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            var bands = Children.OrderByDescending(child => GetBand(child))
                .GroupBy(child => GetBand(child));
                
            var area = new Rect(new Point(0, 0), availableSize);

            foreach(var band in bands)
            {
                foreach(var child in band)
                {
                    child.Measure(new Size(area.Width, area.Height));
                }

                area = InnerArea(area, BandThickness(band));
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var bands = Children.OrderByDescending(child => GetBand(child))
                .GroupBy(child => GetBand(child));

            var thickness = bands.Sum(band => BandThickness(band));

            var size = new Size(
                Math.Max(thickness * 2, finalSize.Width),
                Math.Max(thickness * 2, finalSize.Height)
            );

            var area = new Rect(new Point(0, 0), size);

            foreach(var band in bands)
            {
                foreach(var child in band) child.Arrange(area);
                area = InnerArea(area, BandThickness(band));
            }

            return size;
        }

        #endregion

        #region Private Members

        private double BandThickness(IEnumerable<UIElement> band)
        {
            if (band.Count() == 0) return 0.0;
            return band.Max(child => GetThickness(child));
        }

        private Rect InnerArea(Rect area, double thickness)
        {
            if (area.Width < thickness * 2 || area.Height < thickness * 2)
            {
                return new Rect(0,0,0,0);
            }

            return new Rect(
                area.X + thickness, area.Y + thickness,
                area.Width - thickness * 2, area.Height - thickness * 2
            );
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            var element = o as FrameworkElement;
            if (element == null) return;

            var parent = element.Parent as Halo;
            if (parent == null) return;

            parent.InvalidateMeasure();
            parent.UpdateLayout();
        }

        #endregion
    }
}
