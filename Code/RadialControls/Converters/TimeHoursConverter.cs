using System;
using Windows.UI.Xaml.Data;
using RadialControls.Elements;

namespace RadialControls.Converters
{
    public class TimeHoursConverter : IValueConverter
    {
        private readonly Clock _picker;

        public TimeHoursConverter(Clock picker)
        {
            _picker = picker;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var span = (TimeSpan) value;
            var hours = (double) span.TotalHours;
            return ((hours % 12) / 12) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var span = _picker.Value;

            var oldHours = span.Hours % 12;
            var newHours = (((double) value)/360)*12;

            var offset = (oldHours / 12) * 12;

            if ((oldHours > 9) ^ (newHours > 9))
            {
                if ((oldHours < 3) ^ (newHours < 3))
                {
                    offset = (offset + 12) % 24;
                }
            }

            return new TimeSpan(
                (int) newHours + offset, span.Minutes, span.Seconds
            );
        }
    }
}
