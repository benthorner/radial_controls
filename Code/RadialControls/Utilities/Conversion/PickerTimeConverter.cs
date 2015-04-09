using System;
using Windows.UI.Xaml.Data;

namespace RadialControls.Utilities
{
    public class PickerTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var picker = (RadialTimePicker) value;
            if (picker == null) return "00:00";

            var offset = picker.Period == "AM" ? 0 : 12;
            var hours = Math.Floor(picker.Hours + offset);
            var minutes = Math.Floor(picker.Minutes);

            return String.Format("{0:00}:{1:00}", hours, minutes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
