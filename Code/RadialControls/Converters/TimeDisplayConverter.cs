using System;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Converters
{
    class TimeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var span = (TimeSpan) value;

            return String.Format(
                "{0:00}:{1:00}", span.Hours, span.Minutes
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
