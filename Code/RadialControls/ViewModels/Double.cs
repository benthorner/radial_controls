using System;

namespace Thorner.RadialControls.ViewModels
{
    public static class DoubleExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return degrees / 180 * Math.PI;
        }
    }
}
