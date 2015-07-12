/**
 *  RadialControls - A circular controls library for Windows 8 Apps
 *  Copyright (C) Ben Thorner 2015
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 **/

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using Thorner.RadialControls.Utilities.Converters;

namespace RadialControls.Unit.Utilities.Converters
{
    [TestClass]
    public class TimeMinutesConverterTest
    {
        private TimeMinutesConverter _subject;
        private Picker _picker = new Picker();

        public class Picker
        {
            public TimeSpan Time { get; set; }
        }

        [TestInitialize]
        public void Initialize()
        {
            _subject = new TimeMinutesConverter(_picker);
        }

        [TestMethod]
        public void ConvertTest()
        {
            // Normal cases
            _picker.Time = new TimeSpan(0, 50, 0);
            Assert.AreEqual(300, Convert());

            _picker.Time = new TimeSpan(0, 0, 30);
            Assert.AreEqual(3, Convert());

            // Extreme cases (bounds)
            _picker.Time = new TimeSpan(0, 60, 0);
            Assert.AreEqual(0, Convert());
        }

        [TestMethod]
        public void ConvertBackTest()
        {
            // Normal cases (late)
            _picker.Time = new TimeSpan(23, 50, 2);

            Assert.AreEqual(new TimeSpan(23, 55, 0), ConvertBack(330));
            Assert.AreEqual(new TimeSpan(23, 45, 0), ConvertBack(270));

            Assert.AreEqual(new TimeSpan(0, 5, 0), ConvertBack(30));
            Assert.AreEqual(new TimeSpan(0, 15, 50), ConvertBack(95));

            Assert.AreEqual(new TimeSpan(23, 20, 0), ConvertBack(120));

            // Normal cases (early)
            _picker.Time = new TimeSpan(0, 10, 30);

            Assert.AreEqual(new TimeSpan(0, 1, 0), ConvertBack(6));
            Assert.AreEqual(new TimeSpan(0, 15, 0), ConvertBack(90));

            Assert.AreEqual(new TimeSpan(23, 50, 0), ConvertBack(300));
            Assert.AreEqual(new TimeSpan(23, 45, 10), ConvertBack(271));

            Assert.AreEqual(new TimeSpan(0, 44, 0), ConvertBack(264));

            // Extreme cases (bounds)
            _picker.Time = new TimeSpan(12, 46, 30);

            Assert.AreEqual(new TimeSpan(13, 6, 0), ConvertBack(396));
            Assert.AreEqual(new TimeSpan(12, 45, 0), ConvertBack(-90));
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
