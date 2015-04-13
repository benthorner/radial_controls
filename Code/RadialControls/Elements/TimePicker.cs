using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using RadialControls.Converters;

namespace RadialControls.Elements
{
    [TemplatePart(Name = "PART_HoursSlider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_MinutesSlider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_TimeReadout", Type = typeof(TextBlock))]
    public class TimePicker : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(
            "Hours", typeof(int), typeof(TimePicker), new PropertyMetadata(default(int), UpdatePeriod));

        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
            "Minutes", typeof(int), typeof(TimePicker), new PropertyMetadata(default(int), UpdateSelf));

        public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register(
            "Period", typeof(string), typeof(TimePicker), new PropertyMetadata("AM", UpdateSelf));

        public static readonly DependencyProperty SelfProperty = DependencyProperty.Register(
            "Self", typeof(TimePicker), typeof(TimePicker), new PropertyMetadata(default(TimePicker)));

        #endregion

        public TimePicker()
        {
            DefaultStyleKey = typeof(TimePicker);
            SetValue(SelfProperty, this);
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

        public int Hours
        {
            get { return (int)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }

        public int Minutes
        {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }

        public string Period
        {
            get { return (string)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        public TimePicker Self
        {
            get { return (TimePicker)GetValue(SelfProperty); }
            set { SetValue(SelfProperty, value); }
        }

        #endregion

        #region Event Handlers

        public static void UpdatePeriod(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (int) e.OldValue;
            var newValue = (int) e.NewValue;

            UpdateSelf(o, e);

            if ((oldValue > 9) ^ (newValue > 9))
            {
                if ((oldValue < 3) ^ (newValue < 3))
                {
                    var period = o.GetValue(PeriodProperty);

                    switch ((string) period)
                    {
                        case "AM":
                            period = "PM"; break;
                        default:
                            period = "AM"; break;
                    }

                    o.SetValue(PeriodProperty, period);
                }
            }
        }

        private static void UpdateSelf(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            o.ClearValue(SelfProperty);
            o.SetValue(SelfProperty, o);
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            var offset = (Period == "PM") ? 12 : 0;

            return String.Format(
                "{0:00}:{1:00}", Hours + offset, Minutes
            );
        }

        #endregion

        #region Private Members

        private void BindControlParts()
        {
            BindingOperations.SetBinding(GetTemplateChild("PART_TimeReadout"), TextBlock.TextProperty,
              new Binding { Source = this, Path = new PropertyPath("Self") });

            BindingOperations.SetBinding(GetTemplateChild("PART_HoursSlider"), Slider.ValueProperty,
                new Binding
                {
                    Source = this,
                    Path = new PropertyPath("Hours"),
                    Converter = new HoursDegreesConverter(),
                    Mode = BindingMode.TwoWay
                });

            BindingOperations.SetBinding(GetTemplateChild("PART_MinutesSlider"), Slider.ValueProperty,
                new Binding
                {
                    Source = this,
                    Path = new PropertyPath("Minutes"),
                    Converter = new MinutesDegreesConverter(),
                    Mode = BindingMode.TwoWay
                });
        }

        #endregion
    }
}
