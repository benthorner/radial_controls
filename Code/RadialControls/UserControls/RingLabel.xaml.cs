using Thorner.RadialControls.TemplateControls;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Thorner.RadialControls.UserControls
{
    public sealed partial class RingLabel : UserControl
    {
        #region DependencyProperties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(RingLabel), new PropertyMetadata("", RefreshText));

        public static readonly DependencyProperty InvertedProperty = DependencyProperty.Register(
            "Role", typeof(bool), typeof(RingLabel), new PropertyMetadata(false, RefreshRole));

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            "Spacing", typeof(double), typeof(RingLabel), new PropertyMetadata(0.0));

        #endregion

        public RingLabel()
        {
            this.InitializeComponent();

            BindingOperations.SetBinding(Chain, HaloChain.SpacingProperty, new Binding
            {
                Source = this, Path = new PropertyPath("Spacing")
            });
        }

        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public bool Inverted
        {
            get { return (bool)GetValue(InvertedProperty); }
            set { SetValue(InvertedProperty, value); }
        }

        #endregion

        #region Event Handlers

        private static void RefreshRole(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = (RingLabel)o;
            var chain = (HaloChain)label.Chain;

            if (label.Inverted)
            {
                chain.Offset = 180;
                chain.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                chain.Offset = 0;
                chain.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private static void RefreshText(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = (RingLabel)o;

            var chain = (HaloChain)label.Chain;
            chain.Children.Clear();

            foreach(var letter in label.Text)
            {
                if (letter == ' ')
                {
                    chain.Children.Add(MakeSpace(label));
                }
                else
                {
                    chain.Children.Add(new TextBlock
                    {
                        Text = letter.ToString()
                    });
                }
            }
        }

        private static UIElement MakeSpace(RingLabel label)
        {
            var fontStretch = (int)label.FontStretch;
            var stretch = 1 - (fontStretch - 5) * 0.5;

            return new Rectangle
            {
                Width = label.FontSize * stretch,
                Height = label.FontSize * stretch
            };
        }

        #endregion
    }
}
