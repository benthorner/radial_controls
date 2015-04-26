using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Thorner.RadialControls.ViewModels
{
    public class Fan
    {
        public enum Extent { Half, All, None };

        public enum Position { Centre, Left, Right };

        private IEnumerable<FrameworkElement> _slats;

        public Fan(IEnumerable<FrameworkElement> slats)
        {
            _slats = slats ?? new List<FrameworkElement>();
        }

        #region Properties

        public Extent Frill { get; set; }

        public Position Alignment { get; set; }

        public double Radius { get; set; }

        public double Offset { get; set; }

        #endregion

        public void Flourish()
        {
            var radius = Radius + RadiusFactor();
            var offset = Offset + AlignmentFactor(radius);

            for (var i = 0; i < _slats.Count(); i++)
            {
                var element = _slats.ElementAt(i);
                var arc = ArcAngle(element, radius);

                if (i > 0)
                {
                    offset += arc / 2;
                }

                FanOut(element, radius, offset);
                offset += arc / 2;
            }
        }

        #region Private Members

        private double RadiusFactor()
        {
            if (_slats.Count() == 0) return 0.0;

            var frill = _slats.Max(
                (slat) => slat.ActualHeight / 2
            );

            switch (Frill)
            {
                case Fan.Extent.None: return -frill / 2;
                case Fan.Extent.All: return frill / 2;
            }

            return 0.0;
        }

        private double AlignmentFactor(double radius)
        {
            if (_slats.Count() == 0) return 0.0;

            double offset = _slats.Sum(
                (slat) => ArcAngle(slat, radius)
            );

            offset -= ArcAngle(_slats.First(), radius) / 2;
            offset -= ArcAngle(_slats.Last(), radius) / 2;

            switch (Alignment)
            {
                case Fan.Position.Centre: return -offset / 2;
                case Fan.Position.Right: return -offset;
            }

            return 0.0;
        }

        private void FanOut(FrameworkElement element, double radius, double angle)
        {
            var origin = new Point { X = 0.5, Y = 0.5 };
            element.RenderTransformOrigin = origin;

            element.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform { Y = - radius },
                        new RotateTransform { Angle = angle }
                    }
            };

            element.VerticalAlignment = VerticalAlignment.Center;
            element.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private double ArcAngle(FrameworkElement element, double radius)
        {
            return Math.Atan2(element.ActualWidth / 2, radius) * 360 / Math.PI;
        }

        #endregion
    }
}
