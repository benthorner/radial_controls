using System;
using Thorner.RadialControls.Converters;
using Thorner.RadialControls.TemplateControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.UserControls
{
    public sealed partial class TimePicker : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(TimeSpan), typeof(TimePicker), new PropertyMetadata(new TimeSpan()));

        #endregion

        public TimePicker()
        {
            this.InitializeComponent();

            BindingOperations.SetBinding(Hours, HaloRing.AngleProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeHoursConverter(this),
                Mode = BindingMode.TwoWay
            });

            BindingOperations.SetBinding(Minutes, HaloRing.AngleProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeMinutesConverter(this),
                Mode = BindingMode.TwoWay
            });

            BindingOperations.SetBinding(Display, TextBlock.TextProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Time"),
                Converter = new TimeDisplayConverter()
            });
        }

        #region Properties

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        #endregion
    }
}
