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

namespace AvaloniaReactorUI
{
    public partial interface IRxTreeViewItem : IRxHeaderedItemsControl
    {
        PropertyValue<bool>? IsExpanded { get; set; }
        PropertyValue<bool>? IsSelected { get; set; }

    }

    public partial class RxTreeViewItem<T> : RxHeaderedItemsControl<T>, IRxTreeViewItem where T : TreeViewItem, new()
    {
        public RxTreeViewItem()
        {

        }

        public RxTreeViewItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxTreeViewItem.IsExpanded { get; set; }
        PropertyValue<bool>? IRxTreeViewItem.IsSelected { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxTreeViewItem = (IRxTreeViewItem)this;
            NativeControl.Set(TreeViewItem.IsExpandedProperty, thisAsIRxTreeViewItem.IsExpanded);
            NativeControl.Set(TreeViewItem.IsSelectedProperty, thisAsIRxTreeViewItem.IsSelected);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxTreeViewItem : RxTreeViewItem<TreeViewItem>
    {
        public RxTreeViewItem()
        {

        }

        public RxTreeViewItem(Action<TreeViewItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTreeViewItemExtensions
    {
        public static T IsExpanded<T>(this T treeviewitem, bool isExpanded) where T : IRxTreeViewItem
        {
            treeviewitem.IsExpanded = new PropertyValue<bool>(isExpanded);
            return treeviewitem;
        }
        public static T IsSelected<T>(this T treeviewitem, bool isSelected) where T : IRxTreeViewItem
        {
            treeviewitem.IsSelected = new PropertyValue<bool>(isSelected);
            return treeviewitem;
        }
    }
}
