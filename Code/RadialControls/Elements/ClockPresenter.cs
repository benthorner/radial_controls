using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RadialControls.Elements
{
    public class ClockPresenter : ContentControl
    {
        private Clock _picker;

        public ClockPresenter()
        {
            DefaultStyleKey = typeof (ClockPresenter);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DependencyObject current = this;

            while (!(current is Clock))
            {
                current = VisualTreeHelper.GetParent(current);
            }

            _picker = current as Clock;

            if (_picker != null)
            {
                
            }
        }
    }
}
