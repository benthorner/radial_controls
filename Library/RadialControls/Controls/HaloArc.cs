using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Thorner.RadialControls.Utilities;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.Controls
{
    public class HaloArc : Path
    {
        #region Dependency Properties

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty SpreadProperty = DependencyProperty.Register(
            "Spread", typeof(double), typeof(HaloArc), new PropertyMetadata(360.0, Refresh));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty TensionProperty = DependencyProperty.Register(
            "Tension", typeof(double), typeof(HaloArc), new PropertyMetadata(0.0, Refresh));

        #endregion

        private PathFigure figure = new PathFigure();
        private ArcSegment segment = new ArcSegment();
        private PathGeometry path = new PathGeometry();

        public HaloArc()
        {
            segment.SweepDirection = SweepDirection.Clockwise;
            figure.Segments = new PathSegmentCollection { segment };
            path.Figures = new PathFigureCollection { figure };

            Data = path;

            BindingOperations.SetBinding(this, Halo.ThicknessProperty,
                new Binding
                {
                    Source = this, Mode = BindingMode.TwoWay,
                    Path = new PropertyPath("StrokeThickness"),
                });
        }

        #region Properties

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public double Spread
        {
            get { return (double)GetValue(SpreadProperty); }
            set { SetValue(SpreadProperty, value); }
        }

        public double Tension
        {
            get { return (double)GetValue(TensionProperty); }
            set { SetValue(TensionProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var circle = new Circle(finalSize);
            circle.Radius -= StrokeThickness / 2;

            ArrangePath(circle);

            return finalSize;
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            ((HaloArc)o).InvalidateMeasure();
            ((HaloArc)o).UpdateLayout();
        }

        #endregion

        #region Private Members

        private void ArrangePath(Circle circle)
        {
            var tension = Tension % 1;
            var angle = Angle + Offset;

            var startAngle = angle - tension * Spread;
            var endAngle = angle + (1 - tension) * Spread;

            figure.StartPoint = circle.PointAt(startAngle);
            segment.Point = circle.PointAt(endAngle);

            segment.Size = circle.Size();
        }

        #endregion
    }
}
