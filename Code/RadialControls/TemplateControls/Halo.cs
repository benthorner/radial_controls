using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class Halo : Panel
    {
        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            var length = Math.Min(
                availableSize.Width, availableSize.Height
            );

            var size = new Size(length, length);

            foreach(var child in Children)
            {
                child.Measure(
                    new Size(length, length)
                );

                var ring = child as HaloRing;
                if (ring == null) continue;

                length -= ring.Thickness * 2;
                length = length < 0 ? 0 : length;
            }

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var origin = 0.0;

            var length = Math.Min(
                finalSize.Width, finalSize.Height
            );

            var size = new Size(length, length);

            foreach(var child in Children)
            {
                child.Arrange(
                    new Rect(origin, origin, length, length)
                );

                var ring = child as HaloRing;
                if (ring == null) continue;

                length -= ring.Thickness * 2;
                length = length > 0 ? length : 0;

                if (length > 0)
                {
                    origin += ring.Thickness;
                }
            }

            return size;
        }

        #endregion
    }
}
