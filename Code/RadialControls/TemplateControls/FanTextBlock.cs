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
            "Radius", typeof(double), typeof(FanTextBlock), new PropertyMetadata(default(double), UpdateFan));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
            "Alignment", typeof(Fan.Position), typeof(FanTextBlock), new PropertyMetadata(default(Fan.Position), UpdateFan));

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

            var offset = blocks.Any() ? blocks.Max((block) =>
            {
                block.Measure(availableSize);

                return Math.Max(
                    block.DesiredSize.Width,
                    block.DesiredSize.Height
                );
            }) : 0;

            return new Size(
                2 * Radius + offset, 2 * Radius + offset
            );
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_grid == null) return new Size(0, 0);
            var blocks = _grid.Children.OfType<TextBlock>();

            var offset = blocks.Any() ? blocks.Max((block) =>
            {
                return Math.Max(
                    block.ActualWidth, block.ActualHeight
                );
            }) : 0;

            if (_fan != null) _fan.Flourish();

            return base.ArrangeOverride(
                new Size(2 * Radius + offset, 2 * Radius + offset)
            );
        }

        #endregion

        #region Event Handlers

        private static void ResetFan(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((FanTextBlock)sender).FanOut();
        }

        private static void UpdateFan(object sender, DependencyPropertyChangedEventArgs e)
        {
            var fan = sender as FanTextBlock;
            if (fan._fan == null) return;

            fan._fan.Radius = fan.Radius;
            fan._fan.Alignment = fan.Alignment;
        }

        #endregion

        #region Private Members

        private void FanOut()
        {
            if (_grid == null || Text == null) return;
            
            _grid.Children.Clear();

            foreach(var letter in Text) 
            {
                _grid.Children.Add(
                    new TextBlock { Text = letter.ToString() }
                );
            }

            _fan = new Fan(_grid.Children.OfType<TextBlock>())
            {
                Radius = Radius, Alignment = Alignment
            };
        }

        #endregion
    }
}
