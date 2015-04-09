using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RadialControls
{
    public sealed partial class RadialTimePicker : UserControl
    {
        // TODO Expose control template for hand

        #region Dependency Properties

        public static readonly DependencyProperty HandSizeProperty =
            DependencyProperty.Register("HandSize", typeof (double), typeof (RadialTimePicker), 
                new PropertyMetadata(40.0));

        public static readonly DependencyProperty RimThicknessProperty =
            DependencyProperty.Register("RimThickness", typeof (Thickness), typeof (RadialTimePicker), 
                new PropertyMetadata(new Thickness(40.0)));

        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof (double), typeof (RadialTimePicker), 
                new PropertyMetadata(default(double), OnHoursChanged));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof (double), typeof (RadialTimePicker),
                new PropertyMetadata(default(double), OnMinutesChanged));

        public static readonly DependencyProperty PeriodProperty =
            DependencyProperty.Register("Period", typeof (string), typeof (RadialTimePicker), 
                new PropertyMetadata("AM"));

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof (RadialTimePicker), typeof (RadialTimePicker), 
                new PropertyMetadata(default(RadialTimePicker)));

        #endregion

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

        public double Minutes
        {
            get { return (double)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }

        public double Hours
        {
            get { return (double)GetValue(HoursProperty); }
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
            var oldValue = (double) e.OldValue;
            var newValue = (double) e.NewValue;

            var period = (string) o.GetValue(PeriodProperty);
            var minutes = (double) o.GetValue(MinutesProperty);

            if (Math.Abs(newValue - oldValue) > 9)
            {
                if ((newValue < 3) || (newValue > 9))
                {
                    switch (period)
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

            o.SetValue(MinutesProperty, (newValue % 1) * 60);

            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        private static void OnMinutesChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (double) e.OldValue;
            var newValue = (double) e.NewValue;

            var hours = (double) o.GetValue(HoursProperty);

            if (Math.Abs(newValue - oldValue) > 45)
            {
                if (newValue < 15)
                {
                    hours = (hours + 1) % 12;
                }

                if (newValue > 45)
                {
                    hours = (12 + hours - 1) % 12;
                }   
            }

            hours = Math.Floor(hours) + newValue / 60;
            o.SetValue(HoursProperty, hours);

            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        #endregion

        #region Private Members

        #endregion
    }
}
