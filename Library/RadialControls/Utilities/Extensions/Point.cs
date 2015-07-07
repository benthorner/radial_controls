using Windows.Foundation;

namespace Thorner.RadialControls.Utilities.Extensions
{
    public static class PointExtensions
    {
        public static Vector RelativeTo(this Point point, Point centre)
        {
            return new Vector(point.X - centre.X, point.Y - centre.Y);
        }
    }
}