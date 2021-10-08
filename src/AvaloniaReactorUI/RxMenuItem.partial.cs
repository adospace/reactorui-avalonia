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
    public partial interface IRxMenuItem : IRxHeaderedSelectingItemsControl
    {
    }

    public partial class RxMenuItem<T> : RxHeaderedSelectingItemsControl<T>, IRxMenuItem, IEnumerable<RxMenuItem> where T : MenuItem, new()
    {
        private readonly List<RxMenuItem> _contents = new();

        public RxMenuItem(string header)
        {
            ((IRxMenuItem)this).Header = new PropertyValue<object>(header);
        }

        public void Add(RxMenuItem child)
        {
            _contents.Add(child);
        }

        public IEnumerator<RxMenuItem> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MenuItem menuItem)
            {
                NativeControl.Items ??= new ObservableCollection<MenuItem>();
                Validate.EnsureNotNull(NativeControl.Items as ObservableCollection<MenuItem>).Add(menuItem);
            }


            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MenuItem menuItem)
            {
                Validate.EnsureNotNull(NativeControl.Items as ObservableCollection<MenuItem>).Remove(menuItem);
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<RxMenuItem> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class RxMenuItem : RxMenuItem<MenuItem>
    {
        public RxMenuItem(string header)
            :base(header)
        {

        }
    }


    public static partial class RxMenuItemExtensions
    {
    }
}
