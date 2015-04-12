﻿using System;
using Windows.UI.Xaml.Data;

namespace RadialControls.Converters
{
    public class MinutesDegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (((int) value) / 60) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (((double) value) / 360) * 60;
        }
    }
}