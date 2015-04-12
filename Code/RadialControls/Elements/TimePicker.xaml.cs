using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using RadialControls.ViewModels;

namespace RadialControls.Elements
{
    public sealed partial class TimePicker : UserControl
    {
        // TODO Expose templates for rings and content
        // TODO Use background/foreground for ring colours
        // TODO Support editable/non-editable states

        #region Dependency Properties

        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(
            "Hours", typeof(int), typeof(TimePicker), new PropertyMetadata(default(int), UpdatePeriod));

        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
            "Minutes", typeof(int), typeof(TimePicker), new PropertyMetadata(default(int), UpdateSelf));

        public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register(
            "Period", typeof(string), typeof(TimePicker), new PropertyMetadata("AM", UpdateSelf));

        public static readonly DependencyProperty SelfProperty = DependencyProperty.Register(
            "Self", typeof(Time), typeof(TimePicker), new PropertyMetadata(default(TimePicker)));

        #endregion

        public TimePicker()
        {
            InitializeComponent();
            DataContext = this;
            Self = this;
        }

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
    }
}
