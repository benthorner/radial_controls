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
using Windows.Foundation;

namespace Thorner.RadialControls.Utilities.Extensions
{
    public class Circle
    {
        public Circle(Size size)
        {
            Radius = Math.Min(size.Width, size.Height) / 2;
            Center = new Point(size.Width/2, size.Height/2);
        }

        public double Radius { get; set; }
        public Point Center { get; set; }

        public Point PointAt(double angle)
        {
            return new Point(
                Center.X + Radius * Math.Sin(angle.ToRadians()),
                Center.Y - Radius * Math.Cos(angle.ToRadians())
            );
        }

        public Size Size()
        {
            return new Size(Radius, Radius);
        }
    }
}
