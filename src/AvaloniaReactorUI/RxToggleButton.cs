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
    public partial interface IRxToggleButton : IRxButton
    {
        PropertyValue<bool>? IsThreeState { get; set; }

        Action? CheckedAction { get; set; }
        Action<RoutedEventArgs>? CheckedActionWithArgs { get; set; }
        Action? UncheckedAction { get; set; }
        Action<RoutedEventArgs>? UncheckedActionWithArgs { get; set; }
        Action? IndeterminateAction { get; set; }
        Action<RoutedEventArgs>? IndeterminateActionWithArgs { get; set; }
    }

    public partial class RxToggleButton<T> : RxButton<T>, IRxToggleButton where T : ToggleButton, new()
    {
        public RxToggleButton()
        {

        }

        public RxToggleButton(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxToggleButton.IsThreeState { get; set; }

        Action? IRxToggleButton.CheckedAction { get; set; }
        Action<RoutedEventArgs>? IRxToggleButton.CheckedActionWithArgs { get; set; }
        Action? IRxToggleButton.UncheckedAction { get; set; }
        Action<RoutedEventArgs>? IRxToggleButton.UncheckedActionWithArgs { get; set; }
        Action? IRxToggleButton.IndeterminateAction { get; set; }
        Action<RoutedEventArgs>? IRxToggleButton.IndeterminateActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxToggleButton = (IRxToggleButton)this;
            NativeControl.Set(ToggleButton.IsThreeStateProperty, thisAsIRxToggleButton.IsThreeState);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxToggleButton = (IRxToggleButton)this;
            if (thisAsIRxToggleButton.CheckedAction != null || thisAsIRxToggleButton.CheckedActionWithArgs != null)
            {
                NativeControl.Checked += NativeControl_Checked;
            }
            if (thisAsIRxToggleButton.UncheckedAction != null || thisAsIRxToggleButton.UncheckedActionWithArgs != null)
            {
                NativeControl.Unchecked += NativeControl_Unchecked;
            }
            if (thisAsIRxToggleButton.IndeterminateAction != null || thisAsIRxToggleButton.IndeterminateActionWithArgs != null)
            {
                NativeControl.Indeterminate += NativeControl_Indeterminate;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Checked(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxToggleButton = (IRxToggleButton)this;
            thisAsIRxToggleButton.CheckedAction?.Invoke();
            thisAsIRxToggleButton.CheckedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Unchecked(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxToggleButton = (IRxToggleButton)this;
            thisAsIRxToggleButton.UncheckedAction?.Invoke();
            thisAsIRxToggleButton.UncheckedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Indeterminate(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxToggleButton = (IRxToggleButton)this;
            thisAsIRxToggleButton.IndeterminateAction?.Invoke();
            thisAsIRxToggleButton.IndeterminateActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Checked -= NativeControl_Checked;
                NativeControl.Unchecked -= NativeControl_Unchecked;
                NativeControl.Indeterminate -= NativeControl_Indeterminate;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxToggleButton : RxToggleButton<ToggleButton>
    {
        public RxToggleButton()
        {

        }

        public RxToggleButton(Action<ToggleButton?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxToggleButtonExtensions
    {
        public static T IsThreeState<T>(this T togglebutton, bool isThreeState) where T : IRxToggleButton
        {
            togglebutton.IsThreeState = new PropertyValue<bool>(isThreeState);
            return togglebutton;
        }
        public static T OnChecked<T>(this T togglebutton, Action checkedAction) where T : IRxToggleButton
        {
            togglebutton.CheckedAction = checkedAction;
            return togglebutton;
        }

        public static T OnChecked<T>(this T togglebutton, Action<RoutedEventArgs> checkedActionWithArgs) where T : IRxToggleButton
        {
            togglebutton.CheckedActionWithArgs = checkedActionWithArgs;
            return togglebutton;
        }
        public static T OnUnchecked<T>(this T togglebutton, Action uncheckedAction) where T : IRxToggleButton
        {
            togglebutton.UncheckedAction = uncheckedAction;
            return togglebutton;
        }

        public static T OnUnchecked<T>(this T togglebutton, Action<RoutedEventArgs> uncheckedActionWithArgs) where T : IRxToggleButton
        {
            togglebutton.UncheckedActionWithArgs = uncheckedActionWithArgs;
            return togglebutton;
        }
        public static T OnIndeterminate<T>(this T togglebutton, Action indeterminateAction) where T : IRxToggleButton
        {
            togglebutton.IndeterminateAction = indeterminateAction;
            return togglebutton;
        }

        public static T OnIndeterminate<T>(this T togglebutton, Action<RoutedEventArgs> indeterminateActionWithArgs) where T : IRxToggleButton
        {
            togglebutton.IndeterminateActionWithArgs = indeterminateActionWithArgs;
            return togglebutton;
        }
    }
}
