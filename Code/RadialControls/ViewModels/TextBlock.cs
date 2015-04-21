using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Thorner.RadialControls.ViewModels
{
    public static class TextBlockExtensions
    {
        public static void FanOut(this TextBlock block, double radius, double angle)
        {
            var origin = new Point { X = 0.5, Y = 0.5 };
            block.RenderTransformOrigin = origin;

            block.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform { Y = - radius },
                        new RotateTransform { Angle = angle }
                    }
            };

            block.VerticalAlignment = VerticalAlignment.Center;
            block.HorizontalAlignment = HorizontalAlignment.Center;
        }

        public static double ArcAngle(this TextBlock block, double radius)
        {
            return Math.Atan2(block.ActualWidth / 2, radius) * 360 / Math.PI;
        }
    }
}
