using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Foundation;
using Thorner.RadialControls.Utilities.Extensions;

namespace RadialControls.Unit.Utilities.Extensions
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void RelativeToTest()
        {
            var origin = new Point(1, -1);
            var vector = new Point(1, 1).RelativeTo(origin);

            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(2, vector.Y);
        }
    }
}
