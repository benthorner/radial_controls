using System;
using Windows.UI.Xaml.Data;

namespace RadialControls.Utilities
{
    public class HoursDegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (((double) value) / 12) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (((double) value) / 360) * 12;
        }
    }
}
