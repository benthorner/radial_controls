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

using System;

namespace Thorner.RadialControls.Utilities.Extensions
{
    public static class DoubleExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        public static double ToDegrees(this double radians)
        {
            return radians * 180 / Math.PI;
        }
    }
}
