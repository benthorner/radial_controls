using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Thorner.RadialControls.ViewModels;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;

namespace Thorner.RadialControls.TemplateControls
{
    public class HaloChain : HaloRing
    {
        #region Label DependencyProperties

        public static readonly DependencyProperty TensionProperty = DependencyProperty.Register(
            "Tension", typeof(double), typeof(HaloChain), new PropertyMetadata(0.5, Refresh));

        public static new readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(HaloChain), new PropertyMetadata(0.0, Refresh));

        public static new readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(HaloChain), new PropertyMetadata(0.0, Refresh));

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            "Spacing", typeof(double), typeof(HaloChain), new PropertyMetadata(0.0, Refresh));

        #endregion

        #region Properties

        public double Tension
        {
            get { return (double)GetValue(TensionProperty); }
            set { SetValue(TensionProperty, value); }
        }

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

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override Size ArrangeOverride(Size finalSize)
        {
            var radius = Math.Min(finalSize.Width, finalSize.Height) / 2;

            var angle = -(Tension % 1) * TotalAngle(radius) + Angle;

            foreach(var link in Children)
            {
                angle += EnterAngle(link, radius);
                link.SetValue(HaloRing.AngleProperty, angle);

                angle += ExitAngle(link, radius);
                link.SetValue(HaloRing.OffsetProperty, Offset);
            }

            return base.ArrangeOverride(new Size(radius * 2, radius * 2));
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            ((HaloChain)o).InvalidateMeasure();
            ((HaloChain)o).UpdateLayout();
        }

        #endregion

        #region Private Members

        private double TotalAngle(double radius)
        {
            return Children.Sum(link =>
            {
                return EnterAngle(link, radius) + ExitAngle(link, radius);
            });
        }

        private double EnterAngle(UIElement link, double radius)
        {
            if (Children.First() == link) return 0.0;
            return HalfAngle(link.DesiredSize, radius) + Spacing / 2;
        }

        private double ExitAngle(UIElement link, double radius)
        {
            if (Children.Last() == link) return 0.0;
            return HalfAngle(link.DesiredSize, radius) + Spacing / 2;
        }

        private double HalfAngle(Size size, double radius)
        {
            var thickness = (double)GetValue(Halo.ThicknessProperty);

            var width = new Vector(
                Math.Cos(Offset.ToRadians()) * size.Width, 
                Math.Sin(Offset.ToRadians()) * size.Height
            ).Length;

            var height = new Vector(
                Math.Sin(Offset.ToRadians()) * size.Width, 
                Math.Cos(Offset.ToRadians()) * size.Height
            ).Length;

            return Math.Atan2(
                width/2, radius - thickness/2 - height/2
            ).ToDegrees();
        }

        #endregion
    }
}
