using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Thorner.RadialControls.Converters;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Thorner.RadialControls.TemplateControls
{
    [TemplatePart(Name = "PART_HoursSlider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_MinutesSlider", Type = typeof(Slider))]
    public class Clock : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(TimeSpan), typeof(Clock), new PropertyMetadata(default(TimeSpan)));

        public static readonly DependencyProperty HandSizeProperty = DependencyProperty.Register(
            "HandSize", typeof(double), typeof(Clock), new PropertyMetadata(50));

        public static readonly DependencyProperty HandStrokeProperty = DependencyProperty.Register(
            "HandStroke", typeof(Brush), typeof(Clock), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty HandFillProperty = DependencyProperty.Register(
            "HandFill", typeof(Brush), typeof(Clock), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode", typeof(string), typeof(Clock), new PropertyMetadata("WithHands", SetHandsState));

        #endregion

        public Clock()
        {
            DefaultStyleKey = typeof(Clock);
        }

        #region Properties

        public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double HandSize
        {
            get { return (double)GetValue(HandSizeProperty); }
            set { SetValue(HandSizeProperty, value); }
        }

        public Brush HandStroke
        {
            get { return (Brush)GetValue(HandStrokeProperty); }
            set { SetValue(HandStrokeProperty, value); }
        }

        public Brush HandFill
        {
            get { return (Brush)GetValue(HandFillProperty); }
            set { SetValue(HandFillProperty, value); }
        }

        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            VisualStateManager.GoToState(
                this, Mode, false
            );

            BindControlParts();
        }

        #endregion

        #region Private Members

        private void BindControlParts()
        {
            BindingOperations.SetBinding(GetTemplateChild("PART_HoursSlider"), Slider.ValueProperty,
                new Binding
                {
                    Source = this,
                    Path = new PropertyPath("Value"),
                    Converter = new TimeHoursConverter(this),
                    Mode = BindingMode.TwoWay
                });

            BindingOperations.SetBinding(GetTemplateChild("PART_MinutesSlider"), Slider.ValueProperty,
                new Binding
                {
                    Source = this,
                    Path = new PropertyPath("Value"),
                    Converter = new TimeMinutesConverter(this),
                    Mode = BindingMode.TwoWay
                });
        }

        private static void SetHandsState(object o, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((Clock) o, (string) e.NewValue, false);
        }

        #endregion
    }
}
