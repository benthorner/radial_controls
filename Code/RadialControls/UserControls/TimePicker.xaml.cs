using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.UserControls
{
    public sealed partial class TimePicker : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status", typeof(string), typeof(TimePicker), new PropertyMetadata("On", SetOnOffState));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode", typeof(string), typeof(TimePicker), new PropertyMetadata("WithHands", SetHandsState));

        #endregion

        public TimePicker()
        {
            this.InitializeComponent();

            VisualStateManager.GoToState(
                this, "On", false
            );

            VisualStateManager.GoToState(
                this, "WithHands", false
            );
        }

        #region Properties

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        #endregion

        #region Event Handlers

        private static void SetOnOffState(object o, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((TimePicker)o, (string) e.NewValue, false);
        }

        private static void SetHandsState(object o, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((TimePicker)o, (string) e.NewValue, false);
        }

        #endregion
    }
}
