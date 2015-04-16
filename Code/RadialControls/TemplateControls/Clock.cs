using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Thorner.RadialControls.Converters;

namespace Thorner.RadialControls.TemplatedControls
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

        #endregion

        #region UIElement Overrides
        
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BindControlParts();

            VisualStateManager.GoToState(this, "Editing", false);
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

        #endregion
    }
}
