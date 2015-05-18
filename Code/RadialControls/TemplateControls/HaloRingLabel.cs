using System;
using System.Linq;
using System.Collections.Generic;
using Thorner.RadialControls.ViewModels;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class HaloRingLabel : HaloRingCluster
    {
        #region DependencyProperties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(HaloRingLabel), new PropertyMetadata("", QueueSplitText));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(HaloRingLabel), new PropertyMetadata(0.5));

        #endregion

        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        #endregion

        #region Event Handlers

        protected override Size ArrangeOverride(Size finalSize)
        {
            base.ArrangeOverride(finalSize);

            var children = Children.OfType<TextBlock>();
            var offset = (double)GetValue(HaloRing.OriginProperty) + AlignmentFactor(children);

            var parent = Parent as HaloRing;

            for (var i = 0; i < children.Count(); i++)
            {
                var slat = children.ElementAt(i);

                if (i > 0)
                {
                    offset += ArcAngle(slat) / 2;
                }

                slat.SetValue(
                    HaloRing.AngleProperty, offset
                );

                slat.SetValue(
                    HaloRing.OriginProperty, GetValue(HaloRing.OriginProperty)
                );

                offset += ArcAngle(slat) / 2;
            }

            return new Size(0, 0);
        }

        private static void QueueSplitText(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = o as HaloRingLabel;

            label.Children.Clear();

            foreach(var slat in (string)e.NewValue)
            {
                label.Children.Add(new TextBlock
                {
                    Text = slat.ToString(),
                    FontSize = 30
                });
            }
        }

        #endregion

        #region Private Members

        private double AlignmentFactor(IEnumerable<TextBlock> children)
        {
            if (children.Count() == 0) return 0.0;

            double offset = children.Sum(
                (slat) => ArcAngle(slat)
            );

            offset -= ArcAngle(children.First()) / 2;
            offset -= ArcAngle(children.Last()) / 2;

            return -(offset * Offset);
        }

        private double ArcAngle(TextBlock element)
        {
            var parent = Parent as HaloRing;
            if (parent == null) return 0.0;

            var length = Math.Min(
                parent.DesiredSize.Width, parent.DesiredSize.Height
            ) - parent.Thickness;

            return Math.Atan2(
                element.ActualWidth / 2, Math.Abs(length) / 2
            ) * 360 / Math.PI;
        }

        #endregion
    }
}
