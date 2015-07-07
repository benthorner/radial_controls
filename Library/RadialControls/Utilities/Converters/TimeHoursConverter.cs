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

            var newValue = AngleFor((double)value);
            var newHours = newValue / 360 * 12;

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
                if ((oldHours < 4) ^ (newHours < 4))
                {
                    return (offset + 12) % 24;
                }
            }

            return offset;
        }

        private double AngleFor(double value)
        {
            return ((value % 360) + 360) % 360;
        }

        #endregion
    }
}