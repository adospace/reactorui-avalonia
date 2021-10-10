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

    public partial class RxMenuItem<T> : RxHeaderedSelectingItemsControl<T>, IRxMenuItem, IEnumerable<VisualNode> where T : MenuItem, new()
    {
        private readonly List<VisualNode> _menuItems = new();

        public RxMenuItem(object header)
        {
            ((IRxMenuItem)this).Header = new PropertyValue<object>(header);
        }

        public void Add(VisualNode child)
        {
            _menuItems.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _menuItems.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MenuItem menuItem)
            {
                NativeControl.Items ??= new ObservableCollection<MenuItem>();
                Validate.EnsureNotNull(NativeControl.Items as ObservableCollection<MenuItem>).Add(menuItem);
            }
            else if (childControl is Image image)
            {
                NativeControl.Icon = image;
            }
            else if (childControl is CheckBox checkBox)
            {
                NativeControl.Icon = checkBox;
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
            else if (childControl is Image)
            {
                NativeControl.Icon = null!;
            }
            else if (childControl is CheckBox)
            {
                NativeControl.Icon = null!;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _menuItems;
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
