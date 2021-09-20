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
    public partial interface IRxButton : IRxContentControl
    {
        PropertyValue<ClickMode> ClickMode { get; set; }
        PropertyValue<ICommand> Command { get; set; }
        PropertyValue<KeyGesture> HotKey { get; set; }
        PropertyValue<object> CommandParameter { get; set; }
        PropertyValue<bool> IsDefault { get; set; }
        PropertyValue<bool> IsCancel { get; set; }
        PropertyValue<FlyoutBase> Flyout { get; set; }

        Action ClickAction { get; set; }
        Action<RoutedEventArgs> ClickActionWithArgs { get; set; }
    }

    public partial class RxButton<T> : RxContentControl<T>, IRxButton where T : Button, new()
    {
        public RxButton()
        {

        }

        public RxButton(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<ClickMode> IRxButton.ClickMode { get; set; }
        PropertyValue<ICommand> IRxButton.Command { get; set; }
        PropertyValue<KeyGesture> IRxButton.HotKey { get; set; }
        PropertyValue<object> IRxButton.CommandParameter { get; set; }
        PropertyValue<bool> IRxButton.IsDefault { get; set; }
        PropertyValue<bool> IRxButton.IsCancel { get; set; }
        PropertyValue<FlyoutBase> IRxButton.Flyout { get; set; }

        Action IRxButton.ClickAction { get; set; }
        Action<RoutedEventArgs> IRxButton.ClickActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxButton = (IRxButton)this;
            NativeControl.Set(Button.ClickModeProperty, thisAsIRxButton.ClickMode);
            NativeControl.Set(Button.CommandProperty, thisAsIRxButton.Command);
            NativeControl.Set(Button.HotKeyProperty, thisAsIRxButton.HotKey);
            NativeControl.Set(Button.CommandParameterProperty, thisAsIRxButton.CommandParameter);
            NativeControl.Set(Button.IsDefaultProperty, thisAsIRxButton.IsDefault);
            NativeControl.Set(Button.IsCancelProperty, thisAsIRxButton.IsCancel);
            NativeControl.Set(Button.FlyoutProperty, thisAsIRxButton.Flyout);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxButton = (IRxButton)this;
            if (thisAsIRxButton.ClickAction != null || thisAsIRxButton.ClickActionWithArgs != null)
            {
                NativeControl.Click += NativeControl_Click;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Click(object sender, RoutedEventArgs e)
        {
            var thisAsIRxButton = (IRxButton)this;
            thisAsIRxButton.ClickAction?.Invoke();
            thisAsIRxButton.ClickActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Click -= NativeControl_Click;
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxButton : RxButton<Button>
    {
        public RxButton()
        {

        }

        public RxButton(Action<Button> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxButtonExtensions
    {
        public static T ClickMode<T>(this T button, ClickMode clickMode) where T : IRxButton
        {
            button.ClickMode = new PropertyValue<ClickMode>(clickMode);
            return button;
        }
        public static T Command<T>(this T button, ICommand command) where T : IRxButton
        {
            button.Command = new PropertyValue<ICommand>(command);
            return button;
        }
        public static T HotKey<T>(this T button, KeyGesture hotKey) where T : IRxButton
        {
            button.HotKey = new PropertyValue<KeyGesture>(hotKey);
            return button;
        }
        public static T CommandParameter<T>(this T button, object commandParameter) where T : IRxButton
        {
            button.CommandParameter = new PropertyValue<object>(commandParameter);
            return button;
        }
        public static T IsDefault<T>(this T button, bool isDefault) where T : IRxButton
        {
            button.IsDefault = new PropertyValue<bool>(isDefault);
            return button;
        }
        public static T IsCancel<T>(this T button, bool isCancel) where T : IRxButton
        {
            button.IsCancel = new PropertyValue<bool>(isCancel);
            return button;
        }
        public static T Flyout<T>(this T button, FlyoutBase flyout) where T : IRxButton
        {
            button.Flyout = new PropertyValue<FlyoutBase>(flyout);
            return button;
        }
        public static T OnClick<T>(this T button, Action clickAction) where T : IRxButton
        {
            button.ClickAction = clickAction;
            return button;
        }

        public static T OnClick<T>(this T button, Action<RoutedEventArgs> clickActionWithArgs) where T : IRxButton
        {
            button.ClickActionWithArgs = clickActionWithArgs;
            return button;
        }
    }
}
