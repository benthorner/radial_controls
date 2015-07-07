using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Extensions;

namespace RadialControls.Unit.Utilities.Extensions
{
    [TestClass]
    public class DoubleTests
    {
        [TestMethod]
        public void ToRadiansTest()
        {
            // Normal cases
            Assert.AreEqual(3.14, Math.Round(180.0.ToRadians(), 2));
            Assert.AreEqual(0, 0.0.ToRadians());
            Assert.AreEqual(-6.28, Math.Round(-360.0.ToRadians(), 2));

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN, Double.NaN.ToRadians());
        }

        [TestMethod]
        public void ToDegreesTest()
        {
            // Normal cases
            Assert.AreEqual(180, Math.PI.ToDegrees());
            Assert.AreEqual(0, 0.0.ToDegrees());
            Assert.AreEqual(-360, (-2 * Math.PI).ToDegrees());

            // Extreme case (NaN)
            Assert.AreEqual(Double.NaN, Double.NaN.ToDegrees());
        }
    }
}
