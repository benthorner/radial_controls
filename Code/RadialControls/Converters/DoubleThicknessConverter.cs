using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Converters
{
    public class DoubleThicknessConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            return new Thickness((double) (int) value);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            var thickness = (Thickness) value;
            var bottom = thickness.Bottom;
            var top = thickness.Top;
            var left = thickness.Left;
            var right = thickness.Right;
            return (bottom + top + left + right)/4;
        }
    }
}
