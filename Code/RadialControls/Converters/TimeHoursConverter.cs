using System;
using Windows.UI.Xaml.Data;
using Thorner.RadialControls.UserControls;

namespace Thorner.RadialControls.Converters
{
    public class TimeHoursConverter : IValueConverter
    {
        private readonly dynamic _picker;

        public TimeHoursConverter(dynamic picker)
        {
            _picker = picker;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var hours = _picker.Time.TotalHours;
            return ((hours % 12) / 12) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var span = _picker.Time;

            var newHours = (((double) value % 360) / 360) * 12;
            var offset = WrapPeriod(span, newHours);

            return new TimeSpan(
                (int) newHours + offset, span.Minutes, span.Seconds
            );
        }

        #region Private Members

        private int WrapPeriod(TimeSpan span, double newHours)
        {
            var oldHours = span.Hours % 12;
            var offset = (span.Hours / 12) * 12;

            if ((oldHours > 9) ^ (newHours > 9))
            {
                if ((oldHours < 3) ^ (newHours < 3))
                {
                    return (offset + 12) % 24;
                }
            }

            return offset;
        }

        #endregion
    }
}