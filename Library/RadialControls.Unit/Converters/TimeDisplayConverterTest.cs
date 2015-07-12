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
