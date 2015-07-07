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
