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
 *  along with this program.If not, see<http://www.gnu.org/licenses/>.
 **/

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
