
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Extensions;
using Windows.Foundation;

namespace RadialControls.Unit.Utilities.Extensions
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void SizeTest()
        {
            // Normal case
            var circle = new Circle(new Size(3.0, 2.0));
            Assert.AreEqual(new Size(1.0, 1.0), circle.Size());

            // Extreme case (Inf)
            var infiniteCircle = new Circle(new Size(Double.PositiveInfinity, 0));
            Assert.AreEqual(new Size(0, 0), infiniteCircle.Size());

            // Extreme case (NaN)
            var nanCircle = new Circle(new Size(Double.NaN, 1));
            Assert.AreEqual(new Size(Double.NaN, Double.NaN), nanCircle.Size());
        }

        [TestMethod]
        public void PointAtTest()
        {
            // Normal cases
            var circle = new Circle(new Size(10.0, 2.0))
            {
                Center = new Point(1, 1)
            };

            Assert.AreEqual(new Point(1, 0), circle.PointAt(0));
            Assert.AreEqual(new Point(0, 1), circle.PointAt(270));
            Assert.AreEqual(new Point(2, 1), circle.PointAt(-270));

            // Extreme case (Inf)
            var infPoint = circle.PointAt(Double.PositiveInfinity);
            Assert.AreEqual(Double.NaN, infPoint.X);
            Assert.AreEqual(Double.NaN, infPoint.Y);

            // Extreme case (NaN)
            var nanPoint = circle.PointAt(Double.PositiveInfinity);
            Assert.AreEqual(Double.NaN, nanPoint.X);
            Assert.AreEqual(Double.NaN, nanPoint.Y);
        }
    }
}
