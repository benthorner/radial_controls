using System;
using System.Collections.Generic;
using System.Linq;
using Thorner.RadialControls.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thorner.RadialControls.TemplateControls
{
    public class FanTextBlock : Control
    {
        private Grid _grid;
        private Fan _fan;

        #region Dependency Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(FanTextBlock), new PropertyMetadata(default(string), ResetFan));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(FanTextBlock), new PropertyMetadata(default(double), ResetFan));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
            "Alignment", typeof(Fan.Position), typeof(FanTextBlock), new PropertyMetadata(default(Fan.Position), UpdateFan));

        public static readonly DependencyProperty FrillProperty = DependencyProperty.Register(
            "Frill", typeof(Fan.Extent), typeof(FanTextBlock), new PropertyMetadata(default(Fan.Extent), UpdateFan));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(FanTextBlock), new PropertyMetadata(default(double), UpdateFan));

        #endregion

        public FanTextBlock()
        {
            DefaultStyleKey = typeof(FanTextBlock);
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
        
        public Fan.Position Alignment
        {
            get { return (Fan.Position)GetValue(AlignmentProperty); }
            set { SetValue(AlignmentProperty, value); }
        }

        public Fan.Extent Frill
        {
            get { return (Fan.Extent)GetValue(FrillProperty); }
            set { SetValue(FrillProperty, value); }
        }

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        #endregion

        #region UIElement Overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _grid = GetTemplateChild("PART_Grid") as Grid;

            this.FanOut();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_grid == null) return new Size(0, 0);
            var blocks = _grid.Children.OfType<TextBlock>();

            var excess = blocks.Any() ? blocks.Max((block) =>
            {
                block.Measure(availableSize);

                return Math.Max(
                    block.DesiredSize.Width,
                    block.DesiredSize.Height
                );
            }) : 0;

            return new Size(
                2 * Math.Abs(Radius) + excess, 
                2 * Math.Abs(Radius) + excess
            );
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_grid == null) return new Size(0, 0);
            var blocks = _grid.Children.OfType<TextBlock>();

            var excess = blocks.Any() ? blocks.Max((block) =>
            {
                return Math.Max(
                    block.ActualWidth, block.ActualHeight
                );
            }) : 0;

            if (_fan != null) _fan.Flourish();

            return base.ArrangeOverride(new Size(
                2 * Math.Abs(Radius) + excess, 
                2 * Math.Abs(Radius) + excess
            ));
        }

        #endregion

        #region Event Handlers

        private static void ResetFan(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((FanTextBlock)sender).FanOut();
        }

        private static void UpdateFan(object sender, DependencyPropertyChangedEventArgs e)
        {
            var block = sender as FanTextBlock;
            if (block._fan == null) return;

            block._fan.Frill = block.Frill;
            block._fan.Offset = block.Offset;
            block._fan.Alignment = block.Alignment;
        }

        #endregion

        #region Private Members

        private void FanOut()
        {
            if (Text == null || _grid == null) return;

            var letters = Radius > 0 ? Text : Text.Reverse();

            _grid.Children.Clear(); foreach (var letter in letters)
            {
                _grid.Children.Add(new TextBlock { Text = letter.ToString() });
            }

            _fan = new Fan(_grid.Children.OfType<TextBlock>())
            {
                Radius = Radius, Alignment = Alignment, Frill = Frill, Offset = Offset
            };
        }

        #endregion
    }
}
