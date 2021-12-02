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
    public partial interface IRxTreeView : IRxItemsControl
    {
        PropertyValue<bool>? AutoScrollToSelectedItem { get; set; }
        PropertyValue<object>? SelectedItem { get; set; }
        PropertyValue<IList>? SelectedItems { get; set; }
        PropertyValue<SelectionMode>? SelectionMode { get; set; }

    }

    public partial class RxTreeView<T> : RxItemsControl<T>, IRxTreeView where T : TreeView, new()
    {
        public RxTreeView()
        {

        }

        public RxTreeView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxTreeView.AutoScrollToSelectedItem { get; set; }
        PropertyValue<object>? IRxTreeView.SelectedItem { get; set; }
        PropertyValue<IList>? IRxTreeView.SelectedItems { get; set; }
        PropertyValue<SelectionMode>? IRxTreeView.SelectionMode { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxTreeView = (IRxTreeView)this;
            NativeControl.Set(TreeView.AutoScrollToSelectedItemProperty, thisAsIRxTreeView.AutoScrollToSelectedItem);
            NativeControl.Set(TreeView.SelectedItemProperty, thisAsIRxTreeView.SelectedItem);
            NativeControl.Set(TreeView.SelectedItemsProperty, thisAsIRxTreeView.SelectedItems);
            NativeControl.Set(TreeView.SelectionModeProperty, thisAsIRxTreeView.SelectionMode);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxTreeView : RxTreeView<TreeView>
    {
        public RxTreeView()
        {

        }

        public RxTreeView(Action<TreeView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTreeViewExtensions
    {
        public static T AutoScrollToSelectedItem<T>(this T treeview, bool autoScrollToSelectedItem) where T : IRxTreeView
        {
            treeview.AutoScrollToSelectedItem = new PropertyValue<bool>(autoScrollToSelectedItem);
            return treeview;
        }
        public static T SelectedItem<T>(this T treeview, object selectedItem) where T : IRxTreeView
        {
            treeview.SelectedItem = new PropertyValue<object>(selectedItem);
            return treeview;
        }
        public static T SelectedItems<T>(this T treeview, IList selectedItems) where T : IRxTreeView
        {
            treeview.SelectedItems = new PropertyValue<IList>(selectedItems);
            return treeview;
        }
        public static T SelectionMode<T>(this T treeview, SelectionMode selectionMode) where T : IRxTreeView
        {
            treeview.SelectionMode = new PropertyValue<SelectionMode>(selectionMode);
            return treeview;
        }
    }
}
