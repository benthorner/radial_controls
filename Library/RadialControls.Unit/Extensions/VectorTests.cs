using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Extensions;

namespace RadialControls.Unit.Utilities.Extensions
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void LengthTest()
        {
            // Normal cases
            Assert.AreEqual(2.0, new Vector(0, 2.0).Length);
            Assert.AreEqual(3.0, new Vector(-3.0, 0).Length);
            Assert.AreEqual(0.0, new Vector(0.0, 0.0).Length);

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN, new Vector(Double.NaN, 0.0).Length);
        }

        [TestMethod]
        public void AngleToTest()
        {
            // Normal cases (vertical origin)
            Assert.AreEqual(0.0,
                new Vector(0, -1).AngleTo(new Vector(0, -0.5))
            );

            Assert.AreEqual(90.0,
                new Vector(100, 0).AngleTo(new Vector(0, -1))
            );

            Assert.AreEqual(270.0,
                new Vector(-0.1, 0).AngleTo(new Vector(0, -100))
            );

            // Normal case (horizontal origin)
            Assert.AreEqual(90.0,
                new Vector(0, 1).AngleTo(new Vector(1, 0))
            );

            // Normal case (slanted origin)
            var angle = new Vector(0, 100).AngleTo(new Vector(1, 1));
            Assert.AreEqual(45.0, Math.Round(angle, 10));

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN,
                new Vector(Double.NaN, 0).AngleTo(new Vector(0, -1))
            );
        }

        [TestMethod]
        public void DotProductTest()
        {
            // Normal cases
            Assert.AreEqual(0.0,
                new Vector(2.0, 0.5).DotProduct(new Vector(-1.0, 4.0))
            );

            Assert.AreEqual(1.5,
                new Vector(0.5, 1.0).DotProduct(new Vector(1.0, 1.0))
            );

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN,
                new Vector(Double.NaN, 0.0).DotProduct(new Vector(0.0, 0.0))
            );
        }

        [TestMethod]
        public void CrossProductTest()
        {
            // Normal cases
            Assert.AreEqual(-4.0,
                new Vector(2.0, -0.5).CrossProduct(new Vector(4.0, 1.0))
            );

            Assert.AreEqual(1.0,
                new Vector(1.0, 2.0).CrossProduct(new Vector(1.0, 1.0))
            );

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN,
                new Vector(Double.NaN, 0).CrossProduct(new Vector(0, 0))
            );
        }
    }
}
