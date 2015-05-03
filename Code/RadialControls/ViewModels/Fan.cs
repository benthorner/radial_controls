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
        private IEnumerable<FrameworkElement> _slats;

        public Fan(IEnumerable<FrameworkElement> slats)
        {
            _slats = slats ?? new List<FrameworkElement>();
        }

        #region Properties

        public double Alignment { get; set; }

        public double Radius { get; set; }

        public double Offset { get; set; }

        #endregion

        public void Flourish()
        {
            var offset = Offset + AlignmentFactor();

            for (var i = 0; i < _slats.Count(); i++)
            {
                var slat = _slats.ElementAt(i);
                var arc = ArcAngle(slat);

                if (i > 0)
                {
                    offset += arc / 2;
                }

                FanOut(slat, offset);
                offset += arc / 2;
            }
        }

        #region Private Members

        private double AlignmentFactor()
        {
            if (_slats.Count() == 0) return 0.0;

            double offset = _slats.Sum(
                (slat) => ArcAngle(slat)
            );

            offset -= ArcAngle(_slats.First()) / 2;
            offset -= ArcAngle(_slats.Last()) / 2;

            return -(offset * Alignment);
        }

        private void FanOut(FrameworkElement element, double angle)
        {
            var origin = new Point { X = 0.5, Y = 0.5 };
            element.RenderTransformOrigin = origin;

            element.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform { Y = -Radius },
                        new RotateTransform { Angle = angle }
                    }
            };

            element.VerticalAlignment = VerticalAlignment.Center;
            element.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private double ArcAngle(FrameworkElement element)
        {
            return Math.Atan2(
                element.ActualWidth / 2, Math.Abs(Radius)
            ) * 360 / Math.PI;
        }

        #endregion
    }
}
