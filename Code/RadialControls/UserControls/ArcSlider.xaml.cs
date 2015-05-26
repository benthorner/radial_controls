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

            SetValue(Templates.Halo.ThicknessProperty, 50.0);
        }
    }
}
