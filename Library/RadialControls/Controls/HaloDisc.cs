using Thorner.RadialControls.Utilities.Extensions;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Thorner.RadialControls.Controls
{
    public class HaloDisc : Path
    {
        private EllipseGeometry ellipse = new EllipseGeometry();

        public HaloDisc()
        {
            Data = ellipse;
        }

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var circle = new Circle(finalSize);
            circle.Radius -= StrokeThickness / 2;

            ArrangeEllipse(circle);
            return finalSize;
        }

        #endregion

        #region Private Members

        private void ArrangeEllipse(Circle circle)
        {
            ellipse.Center = circle.Center;
            ellipse.RadiusX = circle.Radius;
            ellipse.RadiusY = circle.Radius;
        }

        #endregion
    }
}
