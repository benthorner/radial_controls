using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class Pyramid : Panel
    {
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.RegisterAttached("Step", typeof (Thickness), typeof (Pyramid), 
                new PropertyMetadata(default(Thickness)));

        public static void SetStep(UIElement element, Thickness value)
        {
            element.SetValue(StepProperty, value);
        }

        public static Thickness GetStep(UIElement element)
        {
            return (Thickness) element.GetValue(StepProperty);
        }

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var element in Children)
            {
                if (element == null) continue;

                element.Measure(availableSize);

                availableSize = UpdateSize(
                    availableSize, GetStep(element)
                );
            }

            return Children.Count > 0 
                ? Children.First().DesiredSize 
                : new Size(0, 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = finalSize;
            var origin = new Point(0, 0);

            foreach (var element in Children)
            {
                if (element == null) continue;

                element.Arrange(
                    new Rect(origin, size)
                );

                var step = GetStep(element);

                size = UpdateSize(size, step);
                origin = UpdateOrigin(origin, step);
            }

            return finalSize;
        }

        #endregion

        #region Private Members

        private Size UpdateSize(Size size, Thickness step)
        {
            return new Size(size.Width - step.Left - step.Right,
                size.Height - step.Top - step.Bottom);
        }

        private Point UpdateOrigin(Point origin, Thickness step)
        {
            return new Point(origin.X + step.Left,
                origin.Y + step.Top);
        }

        #endregion
    }
}
