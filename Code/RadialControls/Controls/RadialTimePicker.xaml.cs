using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RadialControls
{
    public sealed partial class RadialTimePicker : UserControl
    {
        // TODO Expose control template for hands
        // TODO Expose dial colours
        // TODO Expose content
        // TODO Support editable/non-editable states

        #region Dependency Properties

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

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof (RadialTimePicker), typeof (RadialTimePicker), 
                new PropertyMetadata(default(RadialTimePicker)));

        #endregion

        public RadialTimePicker()
        {
            Period = "AM";
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

        public string Period { get; set; }

        #endregion

        #region Event Handlers

        private static void OnHoursChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (RadialTimePicker) o;

            var oldValue = (int) e.OldValue;
            var newValue = (int) e.NewValue;

            if (Math.Abs(newValue - oldValue) > 9)
            {
                if ((newValue < 3) || (newValue > 9))
                {
                    switch (picker.Period)
                    {
                        case "AM":
                            picker.Period = "PM";
                            break;
                        case "PM":
                            picker.Period = "AM";
                            break;
                    }
                }
            }

            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        private static void OnMinutesChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            o.ClearValue(TimeProperty);
            o.SetValue(TimeProperty, o);
        }

        #endregion
    }
}
