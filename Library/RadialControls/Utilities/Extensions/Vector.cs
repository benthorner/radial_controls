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
    public struct Vector
    {
        public double X, Y;

        public Vector(double x, double y)
        {
            X = x; Y = y;
        }

        #region Properties

        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }

        #endregion

        public double AngleTo(Vector other)
        {
            var dotProduct = DotProduct(other);
            var crossProduct = CrossProduct(other);

            var angle = Math.Acos(
                dotProduct / (Length * other.Length)
            ).ToDegrees();

            var otherWay = crossProduct < 0;
            return otherWay ? (360 - angle) : angle;
        }

        public double DotProduct(Vector other)
        {
            return (X * other.X) + (Y * other.Y);
        }

        public double CrossProduct(Vector other)
        {
            return (Y * other.X) - (X * other.Y);
        }
    }
}
