using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using RadialControls.Utilities;

namespace RadialControls
{
    [TemplatePart(Name="PART_Slider", Type = typeof(FrameworkElement))]
    public sealed class RadialSlider : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof (double), typeof (RadialSlider), 
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (double), typeof (RadialSlider), 
                new PropertyMetadata(default(double)));

        #endregion

        private FrameworkElement _slider;

        private readonly TranslateTransform _translation = new TranslateTransform();
        private readonly RotateTransform _rotation = new RotateTransform();

        public RadialSlider()
        {
            DefaultStyleKey = typeof(RadialSlider);

            BindingOperations.SetBinding(
                _rotation, RotateTransform.AngleProperty,
                new Binding { Source = this, Path = new PropertyPath("Value") }
            );
        }

        #region Properties

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _slider = GetTemplateChild("PART_Slider") as FrameworkElement;
            if (_slider == null) return;

            MakeSlidable(_slider);
            MakeDragDrop(_slider);

            VisualStateManager.GoToState(this, "Resting", false);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var diameter = Math.Min(availableSize.Width, availableSize.Height);
            if (_slider != null) _slider.Measure(new Size(diameter, diameter));
            return base.MeasureOverride(new Size(diameter, diameter));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var diameter = Math.Min(finalSize.Width, finalSize.Height);

            if (_slider != null)
            {
                _rotation.CenterX = _slider.DesiredSize.Width / 2;
                _rotation.CenterY = _slider.DesiredSize.Height / 2;

                var size = Math.Max(
                    _slider.DesiredSize.Width, _slider.DesiredSize.Height
                );

                _translation.Y = -(diameter / 2) + (size / 2);
            }

            return base.ArrangeOverride(new Size(diameter, diameter));
        }

        #endregion

        #region Event Handlers

        private void StealPointer(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Sliding", false);
            ((UIElement) sender).CapturePointer(e.Pointer);
        }

        private void UpdateValue(object sender, PointerRoutedEventArgs e)
        {

            var element = sender as UIElement;
            if (element == null) return;

            if (element.PointerCaptures == null)
            {
                return;
            }

            if (element.PointerCaptures.Count != 1)
            {
                return;
            }

            var point = e.GetCurrentPoint(this).Position;
            var centre = new Point(ActualWidth/2, ActualHeight/2);

            var hand = point.RelativeTo(centre);
            Value = new Vector(0, -1).AngleTo(hand);
        }

        private void ReleasePointer(object sender, PointerRoutedEventArgs e)
        {
            ((UIElement) sender).ReleasePointerCapture(e.Pointer);
            VisualStateManager.GoToState(this, "Resting", false);
        }

        #endregion

        #region Private Members

        private void MakeSlidable(FrameworkElement element)
        {
            element.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                    {
                        _translation, _rotation
                    }
            };

            element.VerticalAlignment = VerticalAlignment.Center;
            element.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void MakeDragDrop(UIElement element)
        {
            element.AddHandler(
                PointerPressedEvent, new PointerEventHandler(StealPointer), true
            );

            element.AddHandler(
                PointerReleasedEvent, new PointerEventHandler(ReleasePointer), true
            );

            element.AddHandler(
                PointerCanceledEvent, new PointerEventHandler(ReleasePointer), true
            );

            element.AddHandler(
                PointerCaptureLostEvent, new PointerEventHandler(ReleasePointer), true
            );

            element.AddHandler(
                PointerMovedEvent, new PointerEventHandler(UpdateValue), true
            );
        }

        #endregion
    }
}
