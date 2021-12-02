using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;

using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Platform;
using Avalonia.Controls.Selection;
using Avalonia.Input.TextInput;

using AvaloniaReactorUI.Internals;
using System.Collections.ObjectModel;

namespace AvaloniaReactorUI
{
    public partial interface IRxTabControl : IRxSelectingItemsControl
    {
        PropertyValue<IDataTemplate>? ContentTemplate { get; set; }
    }

    public partial class RxTabControl<T> : RxSelectingItemsControl<T>, IRxTabControl, IEnumerable<RxTabItem> where T : TabControl, new()
    {
        private readonly List<RxTabItem> _tabItems = new();

        PropertyValue<IDataTemplate>? IRxTabControl.ContentTemplate { get; set; }

        public void Add(RxTabItem child)
        {
            _tabItems.Add(child);
        }

        public void Add(IEnumerable<RxTabItem> children)
        {
            _tabItems.AddRange(children);
        }

        public IEnumerator<RxTabItem> GetEnumerator()
        {
            return _tabItems.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is TabItem tabItem)
            {
                NativeControl.Items ??= new ObservableCollection<TabItem>();
                Validate.EnsureNotNull(NativeControl.Items as IList).Add(tabItem);
            }


            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is TabItem tabItem)
            {
                Validate.EnsureNotNull(NativeControl.Items as IList).Remove(tabItem);
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<RxTabItem> RenderChildren()
        {
            return _tabItems;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            
            var thisAsIRxLayoutable = (IRxTabControl)this;
            NativeControl.Set(TabControl.ContentTemplateProperty, thisAsIRxLayoutable.ContentTemplate);
        }
    }
    public static partial class RxTabControlExtensions
    {
        public static T OnRenderContent<T, I>(this T itemscontrol, Func<I, VisualNode> renderFunc) where T : IRxTabControl
        {
            itemscontrol.ContentTemplate = new PropertyValue<IDataTemplate>(
                new FuncDataTemplate<I>((item, ns) =>
                {
                    VisualNode newRoot = renderFunc(item);
                    var itemTemplateNode = new ItemTemplateNode(newRoot);
                    itemTemplateNode.Layout(itemscontrol);
                    return itemTemplateNode.RootControl;
                }));

            return itemscontrol;
        }
    }
}
