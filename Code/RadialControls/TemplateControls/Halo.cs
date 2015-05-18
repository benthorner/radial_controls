using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class Halo : Panel
    {
        #region Dependency Properties

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            "Thickness", typeof(Thickness), typeof(Halo), new PropertyMetadata(new Thickness(0.0)));

        #endregion

        #region Properties

        public Thickness GetThickness(DependencyObject o)
        {
            return (Thickness)o.GetValue(ThicknessProperty);
        }

        public void SetThickness(DependencyObject o, Thickness value)
        {
            o.SetValue(ThicknessProperty, value);
        }

        #endregion

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = CalculateThickness(availableSize);
            var area = new Rect(new Point(0, 0), size);

            foreach(var child in Children)
            {
                child.Measure(
                    new Size(area.Width, area.Height)
                );

                area = ReCalculateArea(area, child);
            }

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = CalculateThickness(finalSize);
            var area = new Rect(new Point(0, 0), size);

            foreach(var child in Children)
            {
                child.Arrange(area);
                area = ReCalculateArea(area, child);
            }

            return size;
        }

        #endregion

        #region Private Members

        private Size CalculateThickness(Size size)
        {
            var width = 0.0;
            var height = 0.0;

            foreach (var child in Children)
            {
                var thickness = GetThickness(child);

                width += thickness.Left + thickness.Right;
                height += thickness.Top + thickness.Bottom;
            }

            height = Math.Max(height, size.Height);
            width = Math.Max(width, size.Width);

            return new Size(width, height);
        }

        private Rect ReCalculateArea(Rect area, DependencyObject o)
        {
            var thickness = GetThickness(o);

            return new Rect(
                area.X + thickness.Left, area.Y + thickness.Top,
                area.Width - (thickness.Left + thickness.Right),
                area.Height - (thickness.Top + thickness.Bottom)
            );
        }

        #endregion
    }
}
