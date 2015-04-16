using System;
using Windows.UI.Xaml.Data;
using Thorner.RadialControls.TemplatedControls;

namespace Thorner.RadialControls.Converters
{
    public class TimeMinutesConverter : IValueConverter
    {
        private readonly Clock _picker;

        public TimeMinutesConverter(Clock picker)
        {
            _picker = picker;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var span = (TimeSpan) value;
            return (((double) span.TotalMinutes) / 60) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var span = _picker.Value;

            var oldMinutes = span.Minutes;
            var newMinutes = (((double) value)/360)*60;

            var hours = span.Hours;
            var seconds = (newMinutes * 60) % 60;

            if ((oldMinutes > 45) && (newMinutes < 15))
            {
                hours = (hours + 1) % 24;
            }

            if ((oldMinutes < 15) && (newMinutes > 45))
            {
                hours = (24 + hours - 1) % 24;
            }

            return new TimeSpan(
                hours, (int) newMinutes, (int) seconds
            );
        }
    }
}
