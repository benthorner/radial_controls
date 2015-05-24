using System;
using Thorner.RadialControls.Converters;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Thorner.RadialControls.TemplateControls
{
    [TemplatePart(Name = "PART_HoursSlider", Type = typeof(HaloRingSlider))]
    [TemplatePart(Name = "PART_MinutesSlider", Type = typeof(HaloRingSlider))]
    public class Clock : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(TimeSpan), typeof(Clock), new PropertyMetadata(new TimeSpan()));

        #endregion

        public Clock()
        {
            DefaultStyleKey = typeof(Clock);
        }

        #region Properties

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            BindingOperations.SetBinding(GetTemplateChild("PART_HoursSlider"), HaloRing.AngleProperty,
                new Binding
                {
                    Source = this, Path = new PropertyPath("Time"),
                    Converter = new TimeHoursConverter(this),
                    Mode = BindingMode.TwoWay
                });

            BindingOperations.SetBinding(GetTemplateChild("PART_MinutesSlider"), HaloRing.AngleProperty,
                new Binding
                {
                    Source = this, Path = new PropertyPath("Time"),
                    Converter = new TimeMinutesConverter(this),
                    Mode = BindingMode.TwoWay
                });
        }

        #endregion
    }
}
