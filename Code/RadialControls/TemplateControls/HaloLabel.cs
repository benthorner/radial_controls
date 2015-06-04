using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Thorner.RadialControls.ViewModels;

namespace Thorner.RadialControls.TemplateControls
{
    public class HaloLabel : HaloRing
    {
        #region DependencyProperties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(HaloLabel), new PropertyMetadata("", Refresh));

        #endregion

        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override Size ArrangeOverride(Size finalSize)
        {
            var diameter = Math.Min(finalSize.Width, finalSize.Height);

            var totalAngle = 0.0;

            foreach(var letter in Children.OfType<TextBlock>())
            {
                var angle = HalfAngle(letter, diameter);
                totalAngle += angle;

                letter.SetValue(HaloRing.AngleProperty, totalAngle);
                totalAngle += angle;
            }

            return base.ArrangeOverride(new Size(diameter, diameter));
        }

        #endregion

        #region Event Handlers

        private static void Refresh(object o, DependencyPropertyChangedEventArgs e)
        {
            var label = (HaloLabel) o;
            label.Children.Clear();

            foreach(var letter in label.Text)
            {
                label.Children.Add(new TextBlock 
                { 
                    Text = letter.ToString(),
                    FontSize = 50
                });
            }
        }

        #endregion

        #region Private Members

        private double BlankWidth()
        {
            var letters = Children.OfType<TextBlock>();
            if (letters.Count() == 0) return 0.0;
            return letters.Max(letter => letter.ActualWidth);
        }

        private double LabelThickness()
        {
            return (double)GetValue(Halo.ThicknessProperty);
        }

        private double HalfAngle(TextBlock letter, double diameter)
        {
            var thickness = LabelThickness();

            var y = letter.ActualWidth;
            y = y == 0 ? BlankWidth() : y;

            var height = letter.ActualHeight;
            var x = diameter - thickness - height;

            return Math.Atan2(y / 2, x / 2).ToDegrees();
        }

        #endregion
    }
}
