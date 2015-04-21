using System;
using System.Collections.Generic;
using System.Linq;
using Thorner.RadialControls.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class RoundTextBlock : Control
    {
        private Grid _grid;

        #region Dependency Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(RoundTextBlock), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(RoundTextBlock), new PropertyMetadata(default(double)));

        #endregion

        public RoundTextBlock()
        {
            DefaultStyleKey = typeof(RoundTextBlock);
        }

        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }           

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _grid = GetTemplateChild("PART_Grid") as Grid;

            SplitText();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_grid == null)
            {
                return new Size(0, 0);
            }

            var offset = _grid.Children.Max((child) =>
            {
                child.Measure(availableSize);

                return Math.Max(
                    child.DesiredSize.Width, child.DesiredSize.Height
                );
            });

            return new Size(Radius + offset, Radius + offset);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var offset = _grid.Children.Max((child) =>
            {
                var block = child as TextBlock;
                if (block == null) return 0;

                return Math.Max(
                    block.ActualWidth, block.ActualHeight
                );
            });

            FanOut(_grid.Children.OfType<TextBlock>());

            return base.ArrangeOverride(
                new Size(2 * Radius + offset, 2 * Radius + offset)
            );
        }

        #endregion

        #region Private Members

        private void SplitText()
        {
            if (_grid == null) return;
            _grid.Children.Clear();
            if (Text == null) return;

            foreach(var character in Text) 
            {
                _grid.Children.Add(new TextBlock 
                { 
                    Text = character.ToString(),

                });
            }
        }

        private void FanOut(IEnumerable<TextBlock> children)
        {
            var angle = 0.0;

            for (var i = 0; i < children.Count(); i++)
            {
                var block = children.ElementAt(i);
                angle = block.FanOut(Radius, angle);

                if (i > 0)
                {
                    angle = block.FanOut(Radius, angle);
                }
            }
        }

        #endregion
    }
}
