using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Controls = Thorner.RadialControls.Controls;

namespace Thorner.RadialControls.Examples
{
    public sealed partial class ArcSlider : Controls.Slider
    {
        public ArcSlider()
        {
            this.InitializeComponent();

            VisualStateManager.GoToState(this, "Resting", false);

            SlideStart += (sender, args) =>
                VisualStateManager.GoToState(this, "Sliding", false);

            SlideStop += (sender, args) =>
                VisualStateManager.GoToState(this, "Resting", false);

            SetValue(Controls.Halo.ThicknessProperty, 30.0);
        }
    }
}
