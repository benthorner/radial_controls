using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Thorner.RadialControls.Converters;

namespace Thorner.RadialControls.TemplateControls
{
    [TemplatePart(Name = "PART_Display", Type = typeof(TextBlock))]
    public class ClockPresenter : ContentControl
    {
        public ClockPresenter()
        {
            DefaultStyleKey = typeof (ClockPresenter);
        }

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            var display = GetTemplateChild("PART_Display");
            if (display == null) return;

            BindingOperations.SetBinding(display, TextBlock.TextProperty, new Binding
            {
                Source = FindParentClock(), Path = new PropertyPath("Value"),
                Converter = new TimeDisplayConverter()
            });
        }

        #endregion

        #region Private Members

        private Clock FindParentClock()
        {
            DependencyObject current = this;

            while (!(current is Clock))
            {
                current = VisualTreeHelper.GetParent(current);
            }

            return current as Clock;
        }

        #endregion
    }
}
