using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace RadialControls.MarkupExtensions
{
    [ContentProperty(Name="Children")]
    public class MultiBinding : BindingBase
    {
        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(
            "Children", typeof (ICollection<BindingBase>), typeof (MultiBinding), 
                new PropertyMetadata(new List<BindingBase>()));
            
        public ICollection<BindingBase> Children
        {
            get { return (ICollection<BindingBase>) GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }
    }
}
