using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Thorner.RadialControls.Utilities;
using System;

namespace Thorner.RadialControls.Controls
{
    public class Slider : Control
    {
        #region Events

        public event Action<object, SliderEventArgs> SlideStart, SlideStop;

        #endregion

        #region DependencyProperties

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(Slider), new PropertyMetadata(0.0));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(Slider), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ThumbProperty = DependencyProperty.Register(
            "Thumb", typeof(ControlTemplate), typeof(Slider), new PropertyMetadata(null));

        #endregion

        public Slider()
        {
            DefaultStyleKey = typeof(Slider);
        }

        #region Properties

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public ControlTemplate Thumb
        {
            get { return (ControlTemplate)GetValue(ThumbProperty); }
            set { SetValue(ThumbProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            AddHandler(
                PointerPressedEvent, new PointerEventHandler(StealPointer), true
            );

            AddHandler(
                PointerReleasedEvent, new PointerEventHandler(ReleasePointer), true
            );

            AddHandler(
                PointerCanceledEvent, new PointerEventHandler(ReleasePointer), true
            );

            AddHandler(
                PointerCaptureLostEvent, new PointerEventHandler(ReleasePointer), true
            );

            AddHandler(
                PointerMovedEvent, new PointerEventHandler(UpdateValue), true
            );
        }

        #endregion

        #region Event Handlers

        private void StealPointer(object sender, PointerRoutedEventArgs e)
        {
            SlideStart(this, new SliderEventArgs(Angle));
            CapturePointer(e.Pointer);
        }

        private void UpdateValue(object sender, PointerRoutedEventArgs e)
        {
            if (PointerCaptures == null || PointerCaptures.Count != 1)
            {
                return;
            }

            SetValue(AngleProperty,
                SliderAngle(e) - (double)GetValue(OffsetProperty)
            );
        }

        private void ReleasePointer(object sender, PointerRoutedEventArgs e)
        {
            ReleasePointerCapture(e.Pointer);
            SlideStop(this, new SliderEventArgs(Angle));
        }

        #endregion

        #region Private Members

        private double SliderAngle(PointerRoutedEventArgs e)
        {
            var parent = Parent as FrameworkElement;
            if (parent == null) return 0.0;

            var point = e.GetCurrentPoint(parent).Position;

            var centre = new Point(
                parent.ActualWidth / 2, parent.ActualHeight / 2
            );

            var thumb = point.RelativeTo(centre);
            var vertical = new Vector(0, -1);

            return thumb.AngleTo(vertical);
        }

        #endregion
    }

    public sealed class SliderEventArgs
    {
        public SliderEventArgs(double angle)
        {
            Angle = angle;
        }

        public double Angle { get; private set; }
    }
}
