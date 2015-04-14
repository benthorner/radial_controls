using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using RadialControls.Converters;

namespace RadialControls.Elements
{
    [TemplatePart(Name = "PART_HoursSlider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_MinutesSlider", Type = typeof(Slider))]
    public class Clock : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(TimeSpan), typeof(Clock), new PropertyMetadata(new TimeSpan(3,20,3), NotifyChanged));

        #endregion

        public Clock()
        {
            DefaultStyleKey = typeof(Clock);
        }

        #region UIElement Overrides
        
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BindControlParts();

            VisualStateManager.GoToState(this, "Editing", false);
        }

        #endregion

        #region Properties

        public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion

        #region Private Members

        private static void NotifyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var foo = e;
        }

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

        private void UpdatePeriod(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (int)e.OldValue;
            var newValue = (int)e.NewValue;

            if ((oldValue > 9) ^ (newValue > 9))
            {
                if ((oldValue < 3) ^ (newValue < 3))
                {
                    var period = o.GetValue(null);

                    switch ((string)period)
                    {
                        case "AM":
                            period = "PM"; break;
                        default:
                            period = "AM"; break;
                    }

                    //o.SetValue(PeriodProperty, period);
                }
            }
        }

        #endregion
    }
}
