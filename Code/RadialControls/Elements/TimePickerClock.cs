using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RadialControls.Elements
{
    public class TimePickerClock : Control
    {
        private TimePicker _picker;

        public TimePickerClock()
        {
            DependencyObject current = this;

            while (!(current is TimePicker))
            {
                current = VisualTreeHelper.GetParent(current);
            }

            _picker = current as TimePicker;

            if (_picker != null)
            {
                throw new Exception();
            }
        }
    }
}
