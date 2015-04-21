using System;
using System.Collections.Generic;
using System.Linq;
using Thorner.RadialControls.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public enum RadialAlignment { Middle, Start, End };

    public class RoundTextBlock : Control
    {
        private Grid _grid;

        #region Dependency Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(RoundTextBlock), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(RoundTextBlock), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
            "Alignment", typeof(RadialAlignment), typeof(RoundTextBlock), new PropertyMetadata(default(RadialAlignment)));

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
        
        public RadialAlignment Alignment
        {
            get { return (RadialAlignment)GetValue(AlignmentProperty); }
            set { SetValue(AlignmentProperty, value); }
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

            var blocks = _grid.Children.OfType<TextBlock>();
            FanOut(blocks, AlignRotate(blocks));

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

        private double AlignRotate(IEnumerable<TextBlock> blocks)
        {
            double offset = 0.0;

            for (var i = 0; i < blocks.Count(); i++)
            {
                var child = blocks.ElementAt(i);
                var arc = child.ArcAngle(Radius);

                offset += arc / 2;

                if ((i > 0) && (i < blocks.Count() - 1))
                {
                    offset += arc / 2;
                }
            }

            switch (Alignment)
            {
                case RadialAlignment.Middle:
                    return -offset / 2;
                case RadialAlignment.End:
                    return -offset;
                default:
                    return 0.0;
            }
        }

        private void FanOut(IEnumerable<TextBlock> blocks, double offset)
        {
            for (var i = 0; i < blocks.Count(); i++)
            {
                var block = blocks.ElementAt(i);
                var arc = block.ArcAngle(Radius);

                if (i > 0)
                {
                    offset += arc / 2;
                }

                block.FanOut(Radius, offset);
                offset += arc / 2;
            }
        }

        #endregion
    }
}
