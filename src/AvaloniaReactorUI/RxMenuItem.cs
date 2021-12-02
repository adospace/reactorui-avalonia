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
    public partial interface IRxMenuItem : IRxHeaderedSelectingItemsControl
    {
        PropertyValue<ICommand?>? Command { get; set; }
        PropertyValue<KeyGesture?>? HotKey { get; set; }
        PropertyValue<object>? CommandParameter { get; set; }
        PropertyValue<KeyGesture>? InputGesture { get; set; }
        PropertyValue<bool>? IsSelected { get; set; }
        PropertyValue<bool>? IsSubMenuOpen { get; set; }

        Action? ClickAction { get; set; }
        Action<RoutedEventArgs>? ClickActionWithArgs { get; set; }
        Action? PointerEnterItemAction { get; set; }
        Action<PointerEventArgs>? PointerEnterItemActionWithArgs { get; set; }
        Action? PointerLeaveItemAction { get; set; }
        Action<PointerEventArgs>? PointerLeaveItemActionWithArgs { get; set; }
        Action? SubmenuOpenedAction { get; set; }
        Action<RoutedEventArgs>? SubmenuOpenedActionWithArgs { get; set; }
    }

    public partial class RxMenuItem<T> : RxHeaderedSelectingItemsControl<T>, IRxMenuItem where T : MenuItem, new()
    {
        public RxMenuItem()
        {

        }

        public RxMenuItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<ICommand?>? IRxMenuItem.Command { get; set; }
        PropertyValue<KeyGesture?>? IRxMenuItem.HotKey { get; set; }
        PropertyValue<object>? IRxMenuItem.CommandParameter { get; set; }
        PropertyValue<KeyGesture>? IRxMenuItem.InputGesture { get; set; }
        PropertyValue<bool>? IRxMenuItem.IsSelected { get; set; }
        PropertyValue<bool>? IRxMenuItem.IsSubMenuOpen { get; set; }

        Action? IRxMenuItem.ClickAction { get; set; }
        Action<RoutedEventArgs>? IRxMenuItem.ClickActionWithArgs { get; set; }
        Action? IRxMenuItem.PointerEnterItemAction { get; set; }
        Action<PointerEventArgs>? IRxMenuItem.PointerEnterItemActionWithArgs { get; set; }
        Action? IRxMenuItem.PointerLeaveItemAction { get; set; }
        Action<PointerEventArgs>? IRxMenuItem.PointerLeaveItemActionWithArgs { get; set; }
        Action? IRxMenuItem.SubmenuOpenedAction { get; set; }
        Action<RoutedEventArgs>? IRxMenuItem.SubmenuOpenedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxMenuItem = (IRxMenuItem)this;
            NativeControl.SetNullable(MenuItem.CommandProperty, thisAsIRxMenuItem.Command);
            NativeControl.SetNullable(MenuItem.HotKeyProperty, thisAsIRxMenuItem.HotKey);
            NativeControl.Set(MenuItem.CommandParameterProperty, thisAsIRxMenuItem.CommandParameter);
            NativeControl.Set(MenuItem.InputGestureProperty, thisAsIRxMenuItem.InputGesture);
            NativeControl.Set(MenuItem.IsSelectedProperty, thisAsIRxMenuItem.IsSelected);
            NativeControl.Set(MenuItem.IsSubMenuOpenProperty, thisAsIRxMenuItem.IsSubMenuOpen);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxMenuItem = (IRxMenuItem)this;
            if (thisAsIRxMenuItem.ClickAction != null || thisAsIRxMenuItem.ClickActionWithArgs != null)
            {
                NativeControl.Click += NativeControl_Click;
            }
            if (thisAsIRxMenuItem.PointerEnterItemAction != null || thisAsIRxMenuItem.PointerEnterItemActionWithArgs != null)
            {
                NativeControl.PointerEnterItem += NativeControl_PointerEnterItem;
            }
            if (thisAsIRxMenuItem.PointerLeaveItemAction != null || thisAsIRxMenuItem.PointerLeaveItemActionWithArgs != null)
            {
                NativeControl.PointerLeaveItem += NativeControl_PointerLeaveItem;
            }
            if (thisAsIRxMenuItem.SubmenuOpenedAction != null || thisAsIRxMenuItem.SubmenuOpenedActionWithArgs != null)
            {
                NativeControl.SubmenuOpened += NativeControl_SubmenuOpened;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Click(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxMenuItem = (IRxMenuItem)this;
            thisAsIRxMenuItem.ClickAction?.Invoke();
            thisAsIRxMenuItem.ClickActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerEnterItem(object? sender, PointerEventArgs e)
        {
            var thisAsIRxMenuItem = (IRxMenuItem)this;
            thisAsIRxMenuItem.PointerEnterItemAction?.Invoke();
            thisAsIRxMenuItem.PointerEnterItemActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerLeaveItem(object? sender, PointerEventArgs e)
        {
            var thisAsIRxMenuItem = (IRxMenuItem)this;
            thisAsIRxMenuItem.PointerLeaveItemAction?.Invoke();
            thisAsIRxMenuItem.PointerLeaveItemActionWithArgs?.Invoke(e);
        }
        private void NativeControl_SubmenuOpened(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxMenuItem = (IRxMenuItem)this;
            thisAsIRxMenuItem.SubmenuOpenedAction?.Invoke();
            thisAsIRxMenuItem.SubmenuOpenedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Click -= NativeControl_Click;
                NativeControl.PointerEnterItem -= NativeControl_PointerEnterItem;
                NativeControl.PointerLeaveItem -= NativeControl_PointerLeaveItem;
                NativeControl.SubmenuOpened -= NativeControl_SubmenuOpened;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxMenuItem : RxMenuItem<MenuItem>
    {
        public RxMenuItem()
        {

        }

        public RxMenuItem(Action<MenuItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxMenuItemExtensions
    {
        public static T Command<T>(this T menuitem, ICommand? command) where T : IRxMenuItem
        {
            menuitem.Command = new PropertyValue<ICommand?>(command);
            return menuitem;
        }
        public static T HotKey<T>(this T menuitem, KeyGesture? hotKey) where T : IRxMenuItem
        {
            menuitem.HotKey = new PropertyValue<KeyGesture?>(hotKey);
            return menuitem;
        }
        public static T CommandParameter<T>(this T menuitem, object commandParameter) where T : IRxMenuItem
        {
            menuitem.CommandParameter = new PropertyValue<object>(commandParameter);
            return menuitem;
        }
        public static T InputGesture<T>(this T menuitem, KeyGesture inputGesture) where T : IRxMenuItem
        {
            menuitem.InputGesture = new PropertyValue<KeyGesture>(inputGesture);
            return menuitem;
        }
        public static T IsSelected<T>(this T menuitem, bool isSelected) where T : IRxMenuItem
        {
            menuitem.IsSelected = new PropertyValue<bool>(isSelected);
            return menuitem;
        }
        public static T IsSubMenuOpen<T>(this T menuitem, bool isSubMenuOpen) where T : IRxMenuItem
        {
            menuitem.IsSubMenuOpen = new PropertyValue<bool>(isSubMenuOpen);
            return menuitem;
        }
        public static T OnClick<T>(this T menuitem, Action clickAction) where T : IRxMenuItem
        {
            menuitem.ClickAction = clickAction;
            return menuitem;
        }

        public static T OnClick<T>(this T menuitem, Action<RoutedEventArgs> clickActionWithArgs) where T : IRxMenuItem
        {
            menuitem.ClickActionWithArgs = clickActionWithArgs;
            return menuitem;
        }
        public static T OnPointerEnterItem<T>(this T menuitem, Action pointerenteritemAction) where T : IRxMenuItem
        {
            menuitem.PointerEnterItemAction = pointerenteritemAction;
            return menuitem;
        }

        public static T OnPointerEnterItem<T>(this T menuitem, Action<PointerEventArgs> pointerenteritemActionWithArgs) where T : IRxMenuItem
        {
            menuitem.PointerEnterItemActionWithArgs = pointerenteritemActionWithArgs;
            return menuitem;
        }
        public static T OnPointerLeaveItem<T>(this T menuitem, Action pointerleaveitemAction) where T : IRxMenuItem
        {
            menuitem.PointerLeaveItemAction = pointerleaveitemAction;
            return menuitem;
        }

        public static T OnPointerLeaveItem<T>(this T menuitem, Action<PointerEventArgs> pointerleaveitemActionWithArgs) where T : IRxMenuItem
        {
            menuitem.PointerLeaveItemActionWithArgs = pointerleaveitemActionWithArgs;
            return menuitem;
        }
        public static T OnSubmenuOpened<T>(this T menuitem, Action submenuopenedAction) where T : IRxMenuItem
        {
            menuitem.SubmenuOpenedAction = submenuopenedAction;
            return menuitem;
        }

        public static T OnSubmenuOpened<T>(this T menuitem, Action<RoutedEventArgs> submenuopenedActionWithArgs) where T : IRxMenuItem
        {
            menuitem.SubmenuOpenedActionWithArgs = submenuopenedActionWithArgs;
            return menuitem;
        }
    }
}
