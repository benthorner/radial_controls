using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Thorner.RadialControls.ViewModels;
using Windows.UI.Xaml;
using Windows.Foundation;

namespace Thorner.RadialControls.TemplateControls
{
    public sealed class HaloRingSlider : Button
    {
        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(HaloRingSlider), new PropertyMetadata(0.0, (o, e) =>
                {
                    var parent = ((HaloRingSlider)o).Parent as HaloRing;
                    if (parent == null) return;

                    o.SetValue(HaloRing.AngleProperty, e.NewValue);
                    parent.InvalidateMeasure(); parent.UpdateLayout();
                })
        );

        #endregion

        public HaloRingSlider()
        {
            this.DefaultStyleKey = typeof(HaloRingSlider);
        }

        #region Properties

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
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
            VisualStateManager.GoToState(this, "Sliding", false);
            CapturePointer(e.Pointer);
        }

        private void UpdateValue(object sender, PointerRoutedEventArgs e)
        {
            if (PointerCaptures == null || PointerCaptures.Count != 1)
            {
                return;
            }

            Value = SliderAngle(e) 
                - (double)GetValue(HaloRing.OriginProperty);
        }

        private void ReleasePointer(object sender, PointerRoutedEventArgs e)
        {
            ReleasePointerCapture(e.Pointer);
            VisualStateManager.GoToState(this, "Resting", false);
        }

        #endregion

        #region Private Members

        private double SliderAngle(PointerRoutedEventArgs e)
        {
            var parent = Parent as HaloRing;
            if (parent == null) return 0.0;

            var point = e.GetCurrentPoint(parent).Position;

            var centre = new Point(
                parent.ActualWidth / 2, parent.ActualHeight / 2
            );

            var hand = point.RelativeTo(centre);
            return new Vector(0, -1).AngleTo(hand);
        }

        #endregion
    }
}