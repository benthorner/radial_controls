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

using Thorner.RadialControls.Utilities.Extensions;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Thorner.RadialControls.Controls
{
    public class HaloDisc : Path
    {
        private EllipseGeometry ellipse = new EllipseGeometry();

        public HaloDisc()
        {
            Data = ellipse;
        }

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var circle = new Circle(finalSize);
            circle.Radius -= StrokeThickness / 2;

            ArrangeEllipse(circle);
            return finalSize;
        }

        #endregion

        #region Private Members

        private void ArrangeEllipse(Circle circle)
        {
            ellipse.Center = circle.Center;
            ellipse.RadiusX = circle.Radius;
            ellipse.RadiusY = circle.Radius;
        }

        #endregion
    }
}
