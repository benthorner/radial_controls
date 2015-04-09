using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RadialControls.Annotations;

namespace RadialControls
{
    public sealed partial class RadialTimePicker : UserControl
    {
        // TODO Expose control template for hand

        public static readonly DependencyProperty HandSizeProperty =
            DependencyProperty.Register("HandSize", typeof (double), typeof (RadialTimePicker), 
                new PropertyMetadata(40.0));

        public static readonly DependencyProperty RimThicknessProperty =
            DependencyProperty.Register("RimThickness", typeof (Thickness), typeof (RadialTimePicker), 
                new PropertyMetadata(new Thickness(40.0)));

        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof (int), typeof (RadialTimePicker), 
                new PropertyMetadata(default(int), OnHoursChanged));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof (int), typeof (RadialTimePicker),
                new PropertyMetadata(default(int), OnMinutesChanged));

        public static readonly DependencyProperty PeriodProperty =
            DependencyProperty.Register("Period", typeof (string), typeof (RadialTimePicker), 
                new PropertyMetadata("AM"));

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof (RadialTimePicker), typeof (RadialTimePicker), 
                new PropertyMetadata(default(RadialTimePicker)));

        public RadialTimePicker()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties

        public double HandSize
        {
            get { return (double)GetValue(HandSizeProperty); }
            set { SetValue(HandSizeProperty, value); }
        }

        public Thickness RimThickness
        {
            get { return (Thickness)GetValue(RimThicknessProperty); }
            set { SetValue(RimThicknessProperty, value); }
        }

        public string Period
        {
            get { return (string)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        public int Minutes
        {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }

        public int Hours
        {
            get { return (int)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }

        public RadialTimePicker Time
        {
            get { return (RadialTimePicker)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        #endregion

        #region Event Handlers

        private static void OnHoursChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (int) e.OldValue;
            var newValue = (int) e.NewValue;

            if (Math.Abs(newValue - oldValue) > 9)
            {
                if ((newValue < 3) || (newValue > 9))
                {
                    switch ((string)o.GetValue(PeriodProperty))
                    {
                        case "AM":
                            o.SetValue(PeriodProperty, "PM");
                            break;
                        case "PM":
                            o.SetValue(PeriodProperty, "AM");
                            break;
                    }
                }
            }

            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        private static void OnMinutesChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (int) e.OldValue;
            var newValue = (int) e.NewValue;

            if (Math.Abs(newValue - oldValue) > 45)
            {
                var hours = (int)o.GetValue(HoursProperty);

                if (newValue < 15)
                {
                    o.SetValue(HoursProperty, (hours + 1) % 12);
                }

                if (newValue > 45)
                {
                    o.SetValue(HoursProperty, (12 + hours - 1) % 12);
                }   
            }

            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        #endregion
    }
}
