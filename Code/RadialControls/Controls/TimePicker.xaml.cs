using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RadialControls.Utilities;

namespace RadialControls
{
    public sealed partial class TimePicker : UserControl
    {
        // TODO Expose control template for hands
        // TODO Expose dial colours
        // TODO Expose content
        // TODO Support editable/non-editable states

        #region Dependency Properties

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof (Time), typeof (TimePicker), 
                new PropertyMetadata(new Time()));

        #endregion

        public TimePicker()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties

        public Time Time
        {
            get { return (Time)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        #endregion
    }
}
