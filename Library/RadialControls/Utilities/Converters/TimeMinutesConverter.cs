/**
 *  RadialControls - A circular controls library for Windows 8 Apps
 *  Copyright (C) Ben Thorner 2015
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.If not, see <http://www.gnu.org/licenses/>.
 **/

using System;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Utilities.Converters
{
    public class TimeMinutesConverter : IValueConverter
    {
        private readonly dynamic _picker;

        public TimeMinutesConverter(dynamic picker)
        {
            _picker = picker;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var minutes = _picker.Time.TotalMinutes;
            return (minutes % 60 / 60) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var newValue = AngleFor((double)value);

            var newMinutes = (newValue / 360) * 60;
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

            if ((oldMinutes > 45) && (newMinutes < 16))
            {
                return (span.Hours + 1) % 24;
            }

            if ((oldMinutes < 16) && (newMinutes > 45))
            {
                return (24 + span.Hours - 1) % 24;
            }

            return span.Hours;
        }

        private double AngleFor(double value)
        {
            return ((value % 360) + 360) % 360;
        }

        #endregion
    }
}
