using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Templates = Thorner.RadialControls.TemplateControls;

namespace Thorner.RadialControls.UserControls
{
    public sealed partial class ArcSlider : Templates.Slider
    {
        public ArcSlider()
        {
            this.InitializeComponent();

            VisualStateManager.GoToState(this, "Resting", false);

            SlideStart += (sender, args) =>
                VisualStateManager.GoToState(this, "Sliding", false);

            SlideStop += (sender, args) =>
                VisualStateManager.GoToState(this, "Resting", false);

            SetValue(Templates.Halo.ThicknessProperty, 50.0);
        }
    }
}
