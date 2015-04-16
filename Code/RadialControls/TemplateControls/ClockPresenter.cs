using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Thorner.RadialControls.Converters;

namespace Thorner.RadialControls.TemplatedControls
{
    [TemplatePart(Name = "PART_Display", Type = typeof(TextBlock))]
    public class ClockPresenter : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            "Converter", typeof(IValueConverter), typeof(ClockPresenter), 
                new PropertyMetadata(default(IValueConverter)));

        #endregion

        public ClockPresenter()
        {
            DefaultStyleKey = typeof (ClockPresenter);
        }

        #region Properties

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BindClockToDisplay();
        }

        #endregion

        #region Private Members

        private void BindClockToDisplay()
        {
            DependencyObject current = this;

            while (!(current is Clock))
            {
                current = VisualTreeHelper.GetParent(current);
            }

            var clock = current as Clock;

            var display = GetTemplateChild("PART_Display");

            if ((clock != null) && (display != null))
            {
                BindingOperations.SetBinding(
                    display, TextBlock.TextProperty, new Binding
                    {
                        Source = clock, Path = new PropertyPath("Value"),
                        Converter = this.Converter ?? new TimeDisplayConverter()
                    }
                );
            }
        }

        #endregion
    }
}
