using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

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

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public partial interface IRxInputElement : IRxInteractive
    {
        PropertyValue<bool> Focusable { get; set; }
        PropertyValue<bool> IsEnabled { get; set; }
        PropertyValue<Cursor> Cursor { get; set; }
        PropertyValue<bool> IsHitTestVisible { get; set; }

        Action GotFocusAction { get; set; }
        Action<GotFocusEventArgs> GotFocusActionWithArgs { get; set; }
        Action LostFocusAction { get; set; }
        Action<RoutedEventArgs> LostFocusActionWithArgs { get; set; }
        Action KeyDownAction { get; set; }
        Action<KeyEventArgs> KeyDownActionWithArgs { get; set; }
        Action KeyUpAction { get; set; }
        Action<KeyEventArgs> KeyUpActionWithArgs { get; set; }
        Action TextInputAction { get; set; }
        Action<TextInputEventArgs> TextInputActionWithArgs { get; set; }
        Action PointerEnterAction { get; set; }
        Action<PointerEventArgs> PointerEnterActionWithArgs { get; set; }
        Action PointerLeaveAction { get; set; }
        Action<PointerEventArgs> PointerLeaveActionWithArgs { get; set; }
        Action PointerMovedAction { get; set; }
        Action<PointerEventArgs> PointerMovedActionWithArgs { get; set; }
        Action PointerPressedAction { get; set; }
        Action<PointerPressedEventArgs> PointerPressedActionWithArgs { get; set; }
        Action PointerReleasedAction { get; set; }
        Action<PointerReleasedEventArgs> PointerReleasedActionWithArgs { get; set; }
        Action PointerCaptureLostAction { get; set; }
        Action<PointerCaptureLostEventArgs> PointerCaptureLostActionWithArgs { get; set; }
        Action PointerWheelChangedAction { get; set; }
        Action<PointerWheelEventArgs> PointerWheelChangedActionWithArgs { get; set; }
        Action TappedAction { get; set; }
        Action<RoutedEventArgs> TappedActionWithArgs { get; set; }
        Action DoubleTappedAction { get; set; }
        Action<RoutedEventArgs> DoubleTappedActionWithArgs { get; set; }
    }

    public partial class RxInputElement<T> : RxInteractive<T>, IRxInputElement where T : InputElement, new()
    {
        public RxInputElement()
        {

        }

        public RxInputElement(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool> IRxInputElement.Focusable { get; set; }
        PropertyValue<bool> IRxInputElement.IsEnabled { get; set; }
        PropertyValue<Cursor> IRxInputElement.Cursor { get; set; }
        PropertyValue<bool> IRxInputElement.IsHitTestVisible { get; set; }

        Action IRxInputElement.GotFocusAction { get; set; }
        Action<GotFocusEventArgs> IRxInputElement.GotFocusActionWithArgs { get; set; }
        Action IRxInputElement.LostFocusAction { get; set; }
        Action<RoutedEventArgs> IRxInputElement.LostFocusActionWithArgs { get; set; }
        Action IRxInputElement.KeyDownAction { get; set; }
        Action<KeyEventArgs> IRxInputElement.KeyDownActionWithArgs { get; set; }
        Action IRxInputElement.KeyUpAction { get; set; }
        Action<KeyEventArgs> IRxInputElement.KeyUpActionWithArgs { get; set; }
        Action IRxInputElement.TextInputAction { get; set; }
        Action<TextInputEventArgs> IRxInputElement.TextInputActionWithArgs { get; set; }
        Action IRxInputElement.PointerEnterAction { get; set; }
        Action<PointerEventArgs> IRxInputElement.PointerEnterActionWithArgs { get; set; }
        Action IRxInputElement.PointerLeaveAction { get; set; }
        Action<PointerEventArgs> IRxInputElement.PointerLeaveActionWithArgs { get; set; }
        Action IRxInputElement.PointerMovedAction { get; set; }
        Action<PointerEventArgs> IRxInputElement.PointerMovedActionWithArgs { get; set; }
        Action IRxInputElement.PointerPressedAction { get; set; }
        Action<PointerPressedEventArgs> IRxInputElement.PointerPressedActionWithArgs { get; set; }
        Action IRxInputElement.PointerReleasedAction { get; set; }
        Action<PointerReleasedEventArgs> IRxInputElement.PointerReleasedActionWithArgs { get; set; }
        Action IRxInputElement.PointerCaptureLostAction { get; set; }
        Action<PointerCaptureLostEventArgs> IRxInputElement.PointerCaptureLostActionWithArgs { get; set; }
        Action IRxInputElement.PointerWheelChangedAction { get; set; }
        Action<PointerWheelEventArgs> IRxInputElement.PointerWheelChangedActionWithArgs { get; set; }
        Action IRxInputElement.TappedAction { get; set; }
        Action<RoutedEventArgs> IRxInputElement.TappedActionWithArgs { get; set; }
        Action IRxInputElement.DoubleTappedAction { get; set; }
        Action<RoutedEventArgs> IRxInputElement.DoubleTappedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxInputElement = (IRxInputElement)this;
            NativeControl.Set(InputElement.FocusableProperty, thisAsIRxInputElement.Focusable);
            NativeControl.Set(InputElement.IsEnabledProperty, thisAsIRxInputElement.IsEnabled);
            NativeControl.Set(InputElement.CursorProperty, thisAsIRxInputElement.Cursor);
            NativeControl.Set(InputElement.IsHitTestVisibleProperty, thisAsIRxInputElement.IsHitTestVisible);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            if (thisAsIRxInputElement.GotFocusAction != null || thisAsIRxInputElement.GotFocusActionWithArgs != null)
            {
                NativeControl.GotFocus += NativeControl_GotFocus;
            }
            if (thisAsIRxInputElement.LostFocusAction != null || thisAsIRxInputElement.LostFocusActionWithArgs != null)
            {
                NativeControl.LostFocus += NativeControl_LostFocus;
            }
            if (thisAsIRxInputElement.KeyDownAction != null || thisAsIRxInputElement.KeyDownActionWithArgs != null)
            {
                NativeControl.KeyDown += NativeControl_KeyDown;
            }
            if (thisAsIRxInputElement.KeyUpAction != null || thisAsIRxInputElement.KeyUpActionWithArgs != null)
            {
                NativeControl.KeyUp += NativeControl_KeyUp;
            }
            if (thisAsIRxInputElement.TextInputAction != null || thisAsIRxInputElement.TextInputActionWithArgs != null)
            {
                NativeControl.TextInput += NativeControl_TextInput;
            }
            if (thisAsIRxInputElement.PointerEnterAction != null || thisAsIRxInputElement.PointerEnterActionWithArgs != null)
            {
                NativeControl.PointerEnter += NativeControl_PointerEnter;
            }
            if (thisAsIRxInputElement.PointerLeaveAction != null || thisAsIRxInputElement.PointerLeaveActionWithArgs != null)
            {
                NativeControl.PointerLeave += NativeControl_PointerLeave;
            }
            if (thisAsIRxInputElement.PointerMovedAction != null || thisAsIRxInputElement.PointerMovedActionWithArgs != null)
            {
                NativeControl.PointerMoved += NativeControl_PointerMoved;
            }
            if (thisAsIRxInputElement.PointerPressedAction != null || thisAsIRxInputElement.PointerPressedActionWithArgs != null)
            {
                NativeControl.PointerPressed += NativeControl_PointerPressed;
            }
            if (thisAsIRxInputElement.PointerReleasedAction != null || thisAsIRxInputElement.PointerReleasedActionWithArgs != null)
            {
                NativeControl.PointerReleased += NativeControl_PointerReleased;
            }
            if (thisAsIRxInputElement.PointerCaptureLostAction != null || thisAsIRxInputElement.PointerCaptureLostActionWithArgs != null)
            {
                NativeControl.PointerCaptureLost += NativeControl_PointerCaptureLost;
            }
            if (thisAsIRxInputElement.PointerWheelChangedAction != null || thisAsIRxInputElement.PointerWheelChangedActionWithArgs != null)
            {
                NativeControl.PointerWheelChanged += NativeControl_PointerWheelChanged;
            }
            if (thisAsIRxInputElement.TappedAction != null || thisAsIRxInputElement.TappedActionWithArgs != null)
            {
                NativeControl.Tapped += NativeControl_Tapped;
            }
            if (thisAsIRxInputElement.DoubleTappedAction != null || thisAsIRxInputElement.DoubleTappedActionWithArgs != null)
            {
                NativeControl.DoubleTapped += NativeControl_DoubleTapped;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_GotFocus(object sender, GotFocusEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.GotFocusAction?.Invoke();
            thisAsIRxInputElement.GotFocusActionWithArgs?.Invoke(e);
        }
        private void NativeControl_LostFocus(object sender, RoutedEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.LostFocusAction?.Invoke();
            thisAsIRxInputElement.LostFocusActionWithArgs?.Invoke(e);
        }
        private void NativeControl_KeyDown(object sender, KeyEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.KeyDownAction?.Invoke();
            thisAsIRxInputElement.KeyDownActionWithArgs?.Invoke(e);
        }
        private void NativeControl_KeyUp(object sender, KeyEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.KeyUpAction?.Invoke();
            thisAsIRxInputElement.KeyUpActionWithArgs?.Invoke(e);
        }
        private void NativeControl_TextInput(object sender, TextInputEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.TextInputAction?.Invoke();
            thisAsIRxInputElement.TextInputActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerEnter(object sender, PointerEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerEnterAction?.Invoke();
            thisAsIRxInputElement.PointerEnterActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerLeave(object sender, PointerEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerLeaveAction?.Invoke();
            thisAsIRxInputElement.PointerLeaveActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerMoved(object sender, PointerEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerMovedAction?.Invoke();
            thisAsIRxInputElement.PointerMovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerPressedAction?.Invoke();
            thisAsIRxInputElement.PointerPressedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerReleasedAction?.Invoke();
            thisAsIRxInputElement.PointerReleasedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerCaptureLost(object sender, PointerCaptureLostEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerCaptureLostAction?.Invoke();
            thisAsIRxInputElement.PointerCaptureLostActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.PointerWheelChangedAction?.Invoke();
            thisAsIRxInputElement.PointerWheelChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Tapped(object sender, RoutedEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.TappedAction?.Invoke();
            thisAsIRxInputElement.TappedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DoubleTapped(object sender, RoutedEventArgs e)
        {
            var thisAsIRxInputElement = (IRxInputElement)this;
            thisAsIRxInputElement.DoubleTappedAction?.Invoke();
            thisAsIRxInputElement.DoubleTappedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.GotFocus -= NativeControl_GotFocus;
                NativeControl.LostFocus -= NativeControl_LostFocus;
                NativeControl.KeyDown -= NativeControl_KeyDown;
                NativeControl.KeyUp -= NativeControl_KeyUp;
                NativeControl.TextInput -= NativeControl_TextInput;
                NativeControl.PointerEnter -= NativeControl_PointerEnter;
                NativeControl.PointerLeave -= NativeControl_PointerLeave;
                NativeControl.PointerMoved -= NativeControl_PointerMoved;
                NativeControl.PointerPressed -= NativeControl_PointerPressed;
                NativeControl.PointerReleased -= NativeControl_PointerReleased;
                NativeControl.PointerCaptureLost -= NativeControl_PointerCaptureLost;
                NativeControl.PointerWheelChanged -= NativeControl_PointerWheelChanged;
                NativeControl.Tapped -= NativeControl_Tapped;
                NativeControl.DoubleTapped -= NativeControl_DoubleTapped;
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxInputElement : RxInputElement<InputElement>
    {
        public RxInputElement()
        {

        }

        public RxInputElement(Action<InputElement> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxInputElementExtensions
    {
        public static T Focusable<T>(this T inputelement, bool focusable) where T : IRxInputElement
        {
            inputelement.Focusable = new PropertyValue<bool>(focusable);
            return inputelement;
        }
        public static T IsEnabled<T>(this T inputelement, bool isEnabled) where T : IRxInputElement
        {
            inputelement.IsEnabled = new PropertyValue<bool>(isEnabled);
            return inputelement;
        }
        public static T Cursor<T>(this T inputelement, Cursor cursor) where T : IRxInputElement
        {
            inputelement.Cursor = new PropertyValue<Cursor>(cursor);
            return inputelement;
        }
        public static T IsHitTestVisible<T>(this T inputelement, bool isHitTestVisible) where T : IRxInputElement
        {
            inputelement.IsHitTestVisible = new PropertyValue<bool>(isHitTestVisible);
            return inputelement;
        }
        public static T OnGotFocus<T>(this T inputelement, Action gotfocusAction) where T : IRxInputElement
        {
            inputelement.GotFocusAction = gotfocusAction;
            return inputelement;
        }

        public static T OnGotFocus<T>(this T inputelement, Action<GotFocusEventArgs> gotfocusActionWithArgs) where T : IRxInputElement
        {
            inputelement.GotFocusActionWithArgs = gotfocusActionWithArgs;
            return inputelement;
        }
        public static T OnLostFocus<T>(this T inputelement, Action lostfocusAction) where T : IRxInputElement
        {
            inputelement.LostFocusAction = lostfocusAction;
            return inputelement;
        }

        public static T OnLostFocus<T>(this T inputelement, Action<RoutedEventArgs> lostfocusActionWithArgs) where T : IRxInputElement
        {
            inputelement.LostFocusActionWithArgs = lostfocusActionWithArgs;
            return inputelement;
        }
        public static T OnKeyDown<T>(this T inputelement, Action keydownAction) where T : IRxInputElement
        {
            inputelement.KeyDownAction = keydownAction;
            return inputelement;
        }

        public static T OnKeyDown<T>(this T inputelement, Action<KeyEventArgs> keydownActionWithArgs) where T : IRxInputElement
        {
            inputelement.KeyDownActionWithArgs = keydownActionWithArgs;
            return inputelement;
        }
        public static T OnKeyUp<T>(this T inputelement, Action keyupAction) where T : IRxInputElement
        {
            inputelement.KeyUpAction = keyupAction;
            return inputelement;
        }

        public static T OnKeyUp<T>(this T inputelement, Action<KeyEventArgs> keyupActionWithArgs) where T : IRxInputElement
        {
            inputelement.KeyUpActionWithArgs = keyupActionWithArgs;
            return inputelement;
        }
        public static T OnTextInput<T>(this T inputelement, Action textinputAction) where T : IRxInputElement
        {
            inputelement.TextInputAction = textinputAction;
            return inputelement;
        }

        public static T OnTextInput<T>(this T inputelement, Action<TextInputEventArgs> textinputActionWithArgs) where T : IRxInputElement
        {
            inputelement.TextInputActionWithArgs = textinputActionWithArgs;
            return inputelement;
        }
        public static T OnPointerEnter<T>(this T inputelement, Action pointerenterAction) where T : IRxInputElement
        {
            inputelement.PointerEnterAction = pointerenterAction;
            return inputelement;
        }

        public static T OnPointerEnter<T>(this T inputelement, Action<PointerEventArgs> pointerenterActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerEnterActionWithArgs = pointerenterActionWithArgs;
            return inputelement;
        }
        public static T OnPointerLeave<T>(this T inputelement, Action pointerleaveAction) where T : IRxInputElement
        {
            inputelement.PointerLeaveAction = pointerleaveAction;
            return inputelement;
        }

        public static T OnPointerLeave<T>(this T inputelement, Action<PointerEventArgs> pointerleaveActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerLeaveActionWithArgs = pointerleaveActionWithArgs;
            return inputelement;
        }
        public static T OnPointerMoved<T>(this T inputelement, Action pointermovedAction) where T : IRxInputElement
        {
            inputelement.PointerMovedAction = pointermovedAction;
            return inputelement;
        }

        public static T OnPointerMoved<T>(this T inputelement, Action<PointerEventArgs> pointermovedActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerMovedActionWithArgs = pointermovedActionWithArgs;
            return inputelement;
        }
        public static T OnPointerPressed<T>(this T inputelement, Action pointerpressedAction) where T : IRxInputElement
        {
            inputelement.PointerPressedAction = pointerpressedAction;
            return inputelement;
        }

        public static T OnPointerPressed<T>(this T inputelement, Action<PointerPressedEventArgs> pointerpressedActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerPressedActionWithArgs = pointerpressedActionWithArgs;
            return inputelement;
        }
        public static T OnPointerReleased<T>(this T inputelement, Action pointerreleasedAction) where T : IRxInputElement
        {
            inputelement.PointerReleasedAction = pointerreleasedAction;
            return inputelement;
        }

        public static T OnPointerReleased<T>(this T inputelement, Action<PointerReleasedEventArgs> pointerreleasedActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerReleasedActionWithArgs = pointerreleasedActionWithArgs;
            return inputelement;
        }
        public static T OnPointerCaptureLost<T>(this T inputelement, Action pointercapturelostAction) where T : IRxInputElement
        {
            inputelement.PointerCaptureLostAction = pointercapturelostAction;
            return inputelement;
        }

        public static T OnPointerCaptureLost<T>(this T inputelement, Action<PointerCaptureLostEventArgs> pointercapturelostActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerCaptureLostActionWithArgs = pointercapturelostActionWithArgs;
            return inputelement;
        }
        public static T OnPointerWheelChanged<T>(this T inputelement, Action pointerwheelchangedAction) where T : IRxInputElement
        {
            inputelement.PointerWheelChangedAction = pointerwheelchangedAction;
            return inputelement;
        }

        public static T OnPointerWheelChanged<T>(this T inputelement, Action<PointerWheelEventArgs> pointerwheelchangedActionWithArgs) where T : IRxInputElement
        {
            inputelement.PointerWheelChangedActionWithArgs = pointerwheelchangedActionWithArgs;
            return inputelement;
        }
        public static T OnTapped<T>(this T inputelement, Action tappedAction) where T : IRxInputElement
        {
            inputelement.TappedAction = tappedAction;
            return inputelement;
        }

        public static T OnTapped<T>(this T inputelement, Action<RoutedEventArgs> tappedActionWithArgs) where T : IRxInputElement
        {
            inputelement.TappedActionWithArgs = tappedActionWithArgs;
            return inputelement;
        }
        public static T OnDoubleTapped<T>(this T inputelement, Action doubletappedAction) where T : IRxInputElement
        {
            inputelement.DoubleTappedAction = doubletappedAction;
            return inputelement;
        }

        public static T OnDoubleTapped<T>(this T inputelement, Action<RoutedEventArgs> doubletappedActionWithArgs) where T : IRxInputElement
        {
            inputelement.DoubleTappedActionWithArgs = doubletappedActionWithArgs;
            return inputelement;
        }
    }
}
