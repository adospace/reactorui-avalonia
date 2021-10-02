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
    public partial interface IRxSelectingItemsControl : IRxItemsControl
    {
        PropertyValue<bool>? AutoScrollToSelectedItem { get; set; }
        PropertyValue<int>? SelectedIndex { get; set; }
        PropertyValue<object?>? SelectedItem { get; set; }
        PropertyValue<bool>? IsTextSearchEnabled { get; set; }

        Action? SelectionChangedAction { get; set; }
        Action<SelectionChangedEventArgs>? SelectionChangedActionWithArgs { get; set; }
    }

    public partial class RxSelectingItemsControl<T> : RxItemsControl<T>, IRxSelectingItemsControl where T : SelectingItemsControl, new()
    {
        public RxSelectingItemsControl()
        {

        }

        public RxSelectingItemsControl(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxSelectingItemsControl.AutoScrollToSelectedItem { get; set; }
        PropertyValue<int>? IRxSelectingItemsControl.SelectedIndex { get; set; }
        PropertyValue<object?>? IRxSelectingItemsControl.SelectedItem { get; set; }
        PropertyValue<bool>? IRxSelectingItemsControl.IsTextSearchEnabled { get; set; }

        Action? IRxSelectingItemsControl.SelectionChangedAction { get; set; }
        Action<SelectionChangedEventArgs>? IRxSelectingItemsControl.SelectionChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            OnBeginUpdate();

            var thisAsIRxSelectingItemsControl = (IRxSelectingItemsControl)this;
            NativeControl.Set(SelectingItemsControl.AutoScrollToSelectedItemProperty, thisAsIRxSelectingItemsControl.AutoScrollToSelectedItem);
            NativeControl.Set(SelectingItemsControl.SelectedIndexProperty, thisAsIRxSelectingItemsControl.SelectedIndex);
            NativeControl.SetNullable(SelectingItemsControl.SelectedItemProperty, thisAsIRxSelectingItemsControl.SelectedItem);
            NativeControl.Set(SelectingItemsControl.IsTextSearchEnabledProperty, thisAsIRxSelectingItemsControl.IsTextSearchEnabled);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxSelectingItemsControl = (IRxSelectingItemsControl)this;
            if (thisAsIRxSelectingItemsControl.SelectionChangedAction != null || thisAsIRxSelectingItemsControl.SelectionChangedActionWithArgs != null)
            {
                NativeControl.SelectionChanged += NativeControl_SelectionChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var thisAsIRxSelectingItemsControl = (IRxSelectingItemsControl)this;
            thisAsIRxSelectingItemsControl.SelectionChangedAction?.Invoke();
            thisAsIRxSelectingItemsControl.SelectionChangedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxSelectingItemsControl : RxSelectingItemsControl<SelectingItemsControl>
    {
        public RxSelectingItemsControl()
        {

        }

        public RxSelectingItemsControl(Action<SelectingItemsControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxSelectingItemsControlExtensions
    {
        public static T AutoScrollToSelectedItem<T>(this T selectingitemscontrol, bool autoScrollToSelectedItem) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.AutoScrollToSelectedItem = new PropertyValue<bool>(autoScrollToSelectedItem);
            return selectingitemscontrol;
        }
        public static T SelectedIndex<T>(this T selectingitemscontrol, int selectedIndex) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.SelectedIndex = new PropertyValue<int>(selectedIndex);
            return selectingitemscontrol;
        }
        public static T SelectedItem<T>(this T selectingitemscontrol, object? selectedItem) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.SelectedItem = new PropertyValue<object?>(selectedItem);
            return selectingitemscontrol;
        }
        public static T IsTextSearchEnabled<T>(this T selectingitemscontrol, bool isTextSearchEnabled) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.IsTextSearchEnabled = new PropertyValue<bool>(isTextSearchEnabled);
            return selectingitemscontrol;
        }
        public static T OnSelectionChanged<T>(this T selectingitemscontrol, Action selectionchangedAction) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.SelectionChangedAction = selectionchangedAction;
            return selectingitemscontrol;
        }

        public static T OnSelectionChanged<T>(this T selectingitemscontrol, Action<SelectionChangedEventArgs> selectionchangedActionWithArgs) where T : IRxSelectingItemsControl
        {
            selectingitemscontrol.SelectionChangedActionWithArgs = selectionchangedActionWithArgs;
            return selectingitemscontrol;
        }
    }
}
