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
using Thorner.RadialControls.Utilities.Converters;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Examples
{
    public sealed partial class TimePicker : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(TimeSpan), typeof(TimePicker), new PropertyMetadata(new TimeSpan(7, 0, 0)));

        #endregion

        public TimePicker()
        {
            this.InitializeComponent();

            BindingOperations.SetBinding(Hours, ArcSlider.AngleProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeHoursConverter(this), Mode = BindingMode.TwoWay
            });

            BindingOperations.SetBinding(Minutes, ArcSlider.AngleProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeMinutesConverter(this), Mode = BindingMode.TwoWay
            });

            BindingOperations.SetBinding(Display, TextBlock.TextProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeDisplayConverter()
            });
        }

        #region Properties

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        #endregion
    }
}
