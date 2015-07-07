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
 *  along with this program.If not, see<http://www.gnu.org/licenses/>.
 **/

using Thorner.RadialControls.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Shapes;

namespace Thorner.RadialControls.Examples
{
    public sealed partial class RingLabel : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(RingLabel), new PropertyMetadata("", RefreshText));

        public static readonly DependencyProperty FlipProperty = DependencyProperty.Register(
            "Flip", typeof(bool), typeof(RingLabel), new PropertyMetadata(false, RefreshFlip));

        #endregion

        public RingLabel()
        {
            this.InitializeComponent();
        }

        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool Flip
        {
            get { return (bool)GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }

        #endregion

        #region Event Handlers

        private static void RefreshFlip(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = (RingLabel)o;
            var chain = (HaloChain)label.Chain;

            if (label.Flip)
            {
                chain.Offset = 180;
                chain.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                chain.Offset = 0;
                chain.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private static void RefreshText(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = (RingLabel)o;
            var chain = (HaloChain)label.Chain;

            chain.Children.Clear();

            foreach(var letter in label.Text)
            {
                if (letter == ' ')
                {
                    chain.Children.Add(MakeSpace(label));
                }
                else
                {
                    chain.Children.Add(new TextBlock
                    {
                        Text = letter.ToString()
                    });
                }
            }
        }

        private static UIElement MakeSpace(RingLabel label)
        {
            var space = new Rectangle();

            BindingOperations.SetBinding(space, FrameworkElement.WidthProperty, new Binding
            {
                Source = label, Path = new PropertyPath("FontSize"), Mode = BindingMode.TwoWay
            });

            BindingOperations.SetBinding(space, FrameworkElement.HeightProperty, new Binding
            {
                Source = label, Path = new PropertyPath("FontSize"), Mode = BindingMode.TwoWay
            });

            return space;
        }

        #endregion
    }
}
