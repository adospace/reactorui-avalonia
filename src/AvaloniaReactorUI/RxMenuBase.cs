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
    public partial interface IRxMenuBase : IRxSelectingItemsControl
    {

        Action? MenuOpenedAction { get; set; }
        Action<RoutedEventArgs>? MenuOpenedActionWithArgs { get; set; }
        Action? MenuClosedAction { get; set; }
        Action<RoutedEventArgs>? MenuClosedActionWithArgs { get; set; }
    }

    public partial class RxMenuBase<T> : RxSelectingItemsControl<T>, IRxMenuBase where T : MenuBase, new()
    {
        public RxMenuBase()
        {

        }

        public RxMenuBase(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }


        Action? IRxMenuBase.MenuOpenedAction { get; set; }
        Action<RoutedEventArgs>? IRxMenuBase.MenuOpenedActionWithArgs { get; set; }
        Action? IRxMenuBase.MenuClosedAction { get; set; }
        Action<RoutedEventArgs>? IRxMenuBase.MenuClosedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxMenuBase = (IRxMenuBase)this;
            if (thisAsIRxMenuBase.MenuOpenedAction != null || thisAsIRxMenuBase.MenuOpenedActionWithArgs != null)
            {
                NativeControl.MenuOpened += NativeControl_MenuOpened;
            }
            if (thisAsIRxMenuBase.MenuClosedAction != null || thisAsIRxMenuBase.MenuClosedActionWithArgs != null)
            {
                NativeControl.MenuClosed += NativeControl_MenuClosed;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_MenuOpened(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxMenuBase = (IRxMenuBase)this;
            thisAsIRxMenuBase.MenuOpenedAction?.Invoke();
            thisAsIRxMenuBase.MenuOpenedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_MenuClosed(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxMenuBase = (IRxMenuBase)this;
            thisAsIRxMenuBase.MenuClosedAction?.Invoke();
            thisAsIRxMenuBase.MenuClosedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.MenuOpened -= NativeControl_MenuOpened;
                NativeControl.MenuClosed -= NativeControl_MenuClosed;
            }

            base.OnDetachNativeEvents();
        }
    }
    public static partial class RxMenuBaseExtensions
    {
        public static T OnMenuOpened<T>(this T menubase, Action menuopenedAction) where T : IRxMenuBase
        {
            menubase.MenuOpenedAction = menuopenedAction;
            return menubase;
        }

        public static T OnMenuOpened<T>(this T menubase, Action<RoutedEventArgs> menuopenedActionWithArgs) where T : IRxMenuBase
        {
            menubase.MenuOpenedActionWithArgs = menuopenedActionWithArgs;
            return menubase;
        }
        public static T OnMenuClosed<T>(this T menubase, Action menuclosedAction) where T : IRxMenuBase
        {
            menubase.MenuClosedAction = menuclosedAction;
            return menubase;
        }

        public static T OnMenuClosed<T>(this T menubase, Action<RoutedEventArgs> menuclosedActionWithArgs) where T : IRxMenuBase
        {
            menubase.MenuClosedActionWithArgs = menuclosedActionWithArgs;
            return menubase;
        }
    }
}
