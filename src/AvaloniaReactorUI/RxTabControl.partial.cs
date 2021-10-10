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
    }

    public partial class RxTabControl<T> : RxSelectingItemsControl<T>, IRxTabControl, IEnumerable<RxTabItem> where T : TabControl, new()
    {
        private readonly List<RxTabItem> _contents = new();

        public void Add(RxTabItem child)
        {
            _contents.Add(child);
        }

        public IEnumerator<RxTabItem> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is TabItem tabItem)
            {
                NativeControl.Items ??= new ObservableCollection<TabItem>();
                Validate.EnsureNotNull(NativeControl.Items as ObservableCollection<TabItem>).Add(tabItem);
            }


            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is TabItem tabItem)
            {
                Validate.EnsureNotNull(NativeControl.Items as ObservableCollection<TabItem>).Remove(tabItem);
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<RxTabItem> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


    }
    public static partial class RxTabControlExtensions
    {

    }
}
