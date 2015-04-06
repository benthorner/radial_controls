using Windows.Foundation;

namespace RadialControls.Utilities
{
    public static class PointExtensions
    {
        public static Vector RelativeTo(this Point point, Point centre)
        {
            return new Vector(point.X - centre.X, point.Y - centre.Y);
        }
    }
}
