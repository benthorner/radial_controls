using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

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

            FanOut(_grid.Children);

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

                    RenderTransform = new TransformGroup
                    {
                        Children = new TransformCollection 
                        {
                            new TranslateTransform { Y = -Radius },
                            new RotateTransform()
                        }
                    },

                    RenderTransformOrigin = new Point { X = 0.5, Y = 0.5 },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                });
            }
        }

        private void FanOut(IEnumerable<UIElement> children)
        {
            var index = 0;
            var angle = 0.0;

            foreach (var child in children)
            {
                var block = child as TextBlock;
                if (block == null) continue;
                //block.FanOut(previous, offset);

                var group = block.RenderTransform as TransformGroup;
                if (group == null) continue;
                var arc = Math.Atan2(block.ActualWidth / 2, Radius);

                if (index++ > 0)
                {
                    angle += arc * 180 / Math.PI;
                }

                group.Children
                    .OfType<RotateTransform>()
                    .Single().Angle = angle;

                if (index < _grid.Children.Count)
                {
                    angle += arc * 180 / Math.PI;
                }
            }
        }

        #endregion
    }
}
