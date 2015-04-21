using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Thorner.RadialControls.ViewModels
{
    public static class TextBlockExtensions
    {
        public static double FanOut(this TextBlock block, double radius, double offset)
        {
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Center;

            var origin = new Point { X = 0.5, Y = 0.5 };
            block.RenderTransformOrigin = origin;

            var angle = (
                Math.Atan2(block.ActualWidth / 2, radius)
            ) * 180 / Math.PI;

            block.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        new TranslateTransform { Y = - radius },
                        new RotateTransform { Angle = offset }
                    }
            };

            return offset + angle;
        }
    }
}
