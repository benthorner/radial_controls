using System;

namespace RadialControls.Utilities
{
    public class Vector
    {
        public Vector(double x, double y)
        {
            X = x; Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }

        public double AngleTo(Vector other)
        {
            var dotProduct = DotProduct(other);

            var angle = Math.Acos(
                dotProduct / (Length * other.Length)
            ) * 180 / Math.PI;

            return (other.X < X) ? (360 - angle) : angle;
        }

        public double DotProduct(Vector other)
        {
            return (X * other.X) + (Y * other.Y);
        }
    }
}
