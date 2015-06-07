using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Specialized;
using Windows.Foundation;

namespace Thorner.RadialControls.TemplateControls
{
    public class HaloRingCluster : Control
    {
        private ObservableCollection<UIElement> _children;
        private Queue<NotifyCollectionChangedEventArgs> _updates;

        public HaloRingCluster()
        {
            _updates = new Queue<NotifyCollectionChangedEventArgs>();
            _children = new ObservableCollection<UIElement>();
            _children.CollectionChanged += (o, e) => _updates.Enqueue(e);
        }

        #region Properties

        public ICollection<UIElement> Children
        {
            get { return _children; }
        }

        #endregion

        #region UIElement Overrides

        protected override Size ArrangeOverride(Size finalSize)
        {
            while (_updates.Count > 0)
            {
                Synchronise(_updates.Dequeue());
            }

            return new Size(0, 0);
        }

        #endregion

        #region Event Handlers
        #pragma warning disable 4014

        private void Synchronise(NotifyCollectionChangedEventArgs args)
        {
            Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var parent = Parent as HaloRing;
                if (parent == null) return;

                if (args.OldItems != null)
                {
                    var oldItems = args.OldItems.OfType<UIElement>();

                    foreach (var item in oldItems)
                    {
                        parent.Children.Remove(item);
                    }
                }

                if (args.NewItems != null)
                {
                    var newItems = args.NewItems.OfType<UIElement>();

                    foreach (var item in newItems)
                    {
                        parent.Children.Add(item);
                    }
                }
            });
        }

        #endregion
    }
}