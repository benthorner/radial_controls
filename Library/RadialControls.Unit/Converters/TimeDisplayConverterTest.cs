using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Converters;

namespace RadialControls.Unit.Utilities.Converters
{
    [TestClass]
    public class TimeDisplayConverterTest
    {
        private TimeDisplayConverter _subject;

        [TestInitialize]
        public void Initialize()
        {
            _subject = new TimeDisplayConverter();
        }

        [TestMethod]
        public void ConvertTest()
        {
            // Normal cases
            Assert.AreEqual("03:02", Convert(new TimeSpan(3, 2, 1)));
            Assert.AreEqual("15:59", Convert(new TimeSpan(15, 59, 0)));

            // Extreme case (bounds)
            Assert.AreEqual("00:03", Convert(new TimeSpan(24, 3, 2)));
        }

        #region Private Members

        private string Convert(TimeSpan span)
        {
            return (string) _subject.Convert(span, null, null, null);
        }

        #endregion
    }
}
