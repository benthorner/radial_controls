using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.UserControls
{
    public sealed partial class TimePicker : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status", typeof(string), typeof(TimePicker), new PropertyMetadata("On", SetOnOffState));

        #endregion

        public TimePicker()
        {
            this.InitializeComponent();
        }

        #region Properties

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        #endregion

        #region Event Handlers

        private static void SetOnOffState(object o, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((TimePicker)o, (string) e.NewValue, false);
        }

        #endregion
    }
}
