using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Converters;

namespace RadialControls.Unit.Utilities.Converters
{
    [TestClass]
    public class TimeHoursConverterTest
    {
        private TimeHoursConverter _subject;
        private Picker _picker = new Picker();

        public class Picker
        {
            public TimeSpan Time { get; set; }
        }

        [TestInitialize]
        public void Initialize()
        {
            _subject = new TimeHoursConverter(_picker);
        }

        [TestMethod]
        public void ConvertTest()
        {
            // Normal cases
            _picker.Time = new TimeSpan(11, 30, 0);
            Assert.AreEqual(345.0, Convert());

            _picker.Time = new TimeSpan(13, 0, 0);
            Assert.AreEqual(30.0, Convert());

            // Extreme case (bounds)
            _picker.Time = new TimeSpan(24, 45, 0);
            Assert.AreEqual(22.5, Convert());
        }

        [TestMethod]
        public void ConvertBackTest()
        {
            // Normal cases (late)
            _picker.Time = new TimeSpan(10, 30, 0);

            Assert.AreEqual(new TimeSpan(9, 30, 0), ConvertBack(272));
            Assert.AreEqual(new TimeSpan(11, 30, 0), ConvertBack(359));

            Assert.AreEqual(new TimeSpan(12, 30, 0), ConvertBack(0));
            Assert.AreEqual(new TimeSpan(15, 30, 0), ConvertBack(119));

            Assert.AreEqual(new TimeSpan(4, 30, 0), ConvertBack(120));

            // Normal cases (early)
            _picker.Time = new TimeSpan(2, 15, 0);

            Assert.AreEqual(new TimeSpan(23, 15, 0), ConvertBack(333));
            Assert.AreEqual(new TimeSpan(21, 15, 0), ConvertBack(271));

            Assert.AreEqual(new TimeSpan(9, 15, 0), ConvertBack(270));

            // Extreme cases (bounds)
            _picker.Time = new TimeSpan(2, 45, 0);

            Assert.AreEqual(new TimeSpan(3, 45, 0), ConvertBack(-270));
            Assert.AreEqual(new TimeSpan(3, 45, 0), ConvertBack(479));
        }

        #region Private Members

        private double Convert()
        {
            return (double) _subject.Convert(null, null, null, null);
        }

        private TimeSpan ConvertBack(double angle)
        {
            return (TimeSpan) _subject.ConvertBack(angle, null, null, null);
        }

        #endregion
    }
}
