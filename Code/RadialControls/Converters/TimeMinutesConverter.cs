using System;
using Windows.UI.Xaml.Data;
using Thorner.RadialControls.UserControls;

namespace Thorner.RadialControls.Converters
{
    public class TimeMinutesConverter : IValueConverter
    {
        private readonly TimePicker _picker;

        public TimeMinutesConverter(TimePicker picker)
        {
            _picker = picker;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var minutes = _picker.Time.TotalMinutes;
            return (minutes / 60) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var newMinutes = (((double) value % 360) / 360) * 60;
            var seconds = (newMinutes * 60) % 60;
            var hours = WrapHours(_picker.Time, newMinutes);

            return new TimeSpan(
                hours, (int) newMinutes, (int) seconds
            );
        }

        #region Private Members

        private int WrapHours(TimeSpan span, double newMinutes)
        {
            var oldMinutes = span.Minutes;

            if ((oldMinutes > 45) && (newMinutes < 15))
            {
                return (span.Hours + 1) % 24;
            }

            if ((oldMinutes < 15) && (newMinutes > 45))
            {
                return (24 + span.Hours - 1) % 24;
            }

            return span.Hours;
        }

        #endregion
    }
}
