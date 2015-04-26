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
        public enum Position { Middle, Start, End };

        private IEnumerable<FrameworkElement> _slats;

        public Fan(IEnumerable<FrameworkElement> slats)
        {
            _slats = slats ?? new List<FrameworkElement>();
        }

        #region Properties

        public Position Alignment { get; set; }

        public double Radius { get; set; }

        public double Offset { get; set; }

        #endregion

        public void Flourish()
        {
            var offset = Offset + AlignmentFactor();

            for (var i = 0; i < _slats.Count(); i++)
            {
                var element = _slats.ElementAt(i);
                var arc = ArcAngle(element, Radius);

                if (i > 0)
                {
                    offset += arc / 2;
                }

                FanOut(element, Radius, offset);
                offset += arc / 2;
            }
        }

        #region Private Members

        private double AlignmentFactor()
        {
            if (_slats.Count() == 0) return 0.0;

            double offset = _slats.Sum(
                (slat) => ArcAngle(slat, Radius)
            );

            offset -= ArcAngle(_slats.First(), Radius) / 2;
            offset -= ArcAngle(_slats.Last(), Radius) / 2;

            switch (Alignment)
            {
                case Fan.Position.Middle: return -offset / 2;
                case Fan.Position.End: return -offset;
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
