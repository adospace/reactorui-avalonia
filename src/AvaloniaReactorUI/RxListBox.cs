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
    public partial interface IRxListBox : IRxSelectingItemsControl
    {
        PropertyValue<IList> SelectedItems { get; set; }
        PropertyValue<ISelectionModel> Selection { get; set; }
        PropertyValue<SelectionMode> SelectionMode { get; set; }
        PropertyValue<ItemVirtualizationMode> VirtualizationMode { get; set; }

    }

    public partial class RxListBox<T> : RxSelectingItemsControl<T>, IRxListBox where T : ListBox, new()
    {
        public RxListBox()
        {

        }

        public RxListBox(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IList> IRxListBox.SelectedItems { get; set; }
        PropertyValue<ISelectionModel> IRxListBox.Selection { get; set; }
        PropertyValue<SelectionMode> IRxListBox.SelectionMode { get; set; }
        PropertyValue<ItemVirtualizationMode> IRxListBox.VirtualizationMode { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxListBox = (IRxListBox)this;
            NativeControl.Set(ListBox.SelectedItemsProperty, thisAsIRxListBox.SelectedItems);
            NativeControl.Set(ListBox.SelectionProperty, thisAsIRxListBox.Selection);
            NativeControl.Set(ListBox.SelectionModeProperty, thisAsIRxListBox.SelectionMode);
            NativeControl.Set(ListBox.VirtualizationModeProperty, thisAsIRxListBox.VirtualizationMode);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxListBox = (IRxListBox)this;

            base.OnAttachNativeEvents();
        }


        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxListBox : RxListBox<ListBox>
    {
        public RxListBox()
        {

        }

        public RxListBox(Action<ListBox> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxListBoxExtensions
    {
        public static T SelectedItems<T>(this T listbox, IList selectedItems) where T : IRxListBox
        {
            listbox.SelectedItems = new PropertyValue<IList>(selectedItems);
            return listbox;
        }
        public static T Selection<T>(this T listbox, ISelectionModel selection) where T : IRxListBox
        {
            listbox.Selection = new PropertyValue<ISelectionModel>(selection);
            return listbox;
        }
        public static T SelectionMode<T>(this T listbox, SelectionMode selectionMode) where T : IRxListBox
        {
            listbox.SelectionMode = new PropertyValue<SelectionMode>(selectionMode);
            return listbox;
        }
        public static T VirtualizationMode<T>(this T listbox, ItemVirtualizationMode virtualizationMode) where T : IRxListBox
        {
            listbox.VirtualizationMode = new PropertyValue<ItemVirtualizationMode>(virtualizationMode);
            return listbox;
        }
    }
}
