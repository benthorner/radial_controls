using System;

namespace Thorner.RadialControls.Utilities
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
