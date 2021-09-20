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
    public partial interface IRxControl : IRxInputElement
    {
        PropertyValue<object> Tag { get; set; }
        PropertyValue<ContextMenu> ContextMenu { get; set; }
        PropertyValue<FlyoutBase> ContextFlyout { get; set; }

        Action ContextRequestedAction { get; set; }
        Action<ContextRequestedEventArgs> ContextRequestedActionWithArgs { get; set; }
    }

    public partial class RxControl<T> : RxInputElement<T>, IRxControl where T : Control, new()
    {
        public RxControl()
        {

        }

        public RxControl(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<object> IRxControl.Tag { get; set; }
        PropertyValue<ContextMenu> IRxControl.ContextMenu { get; set; }
        PropertyValue<FlyoutBase> IRxControl.ContextFlyout { get; set; }

        Action IRxControl.ContextRequestedAction { get; set; }
        Action<ContextRequestedEventArgs> IRxControl.ContextRequestedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxControl = (IRxControl)this;
            NativeControl.Set(Control.TagProperty, thisAsIRxControl.Tag);
            NativeControl.Set(Control.ContextMenuProperty, thisAsIRxControl.ContextMenu);
            NativeControl.Set(Control.ContextFlyoutProperty, thisAsIRxControl.ContextFlyout);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxControl = (IRxControl)this;
            if (thisAsIRxControl.ContextRequestedAction != null || thisAsIRxControl.ContextRequestedActionWithArgs != null)
            {
                NativeControl.ContextRequested += NativeControl_ContextRequested;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ContextRequested(object sender, ContextRequestedEventArgs e)
        {
            var thisAsIRxControl = (IRxControl)this;
            thisAsIRxControl.ContextRequestedAction?.Invoke();
            thisAsIRxControl.ContextRequestedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.ContextRequested -= NativeControl_ContextRequested;
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxControl : RxControl<Control>
    {
        public RxControl()
        {

        }

        public RxControl(Action<Control> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxControlExtensions
    {
        public static T Tag<T>(this T control, object tag) where T : IRxControl
        {
            control.Tag = new PropertyValue<object>(tag);
            return control;
        }
        public static T ContextMenu<T>(this T control, ContextMenu contextMenu) where T : IRxControl
        {
            control.ContextMenu = new PropertyValue<ContextMenu>(contextMenu);
            return control;
        }
        public static T ContextFlyout<T>(this T control, FlyoutBase contextFlyout) where T : IRxControl
        {
            control.ContextFlyout = new PropertyValue<FlyoutBase>(contextFlyout);
            return control;
        }
        public static T OnContextRequested<T>(this T control, Action contextrequestedAction) where T : IRxControl
        {
            control.ContextRequestedAction = contextrequestedAction;
            return control;
        }

        public static T OnContextRequested<T>(this T control, Action<ContextRequestedEventArgs> contextrequestedActionWithArgs) where T : IRxControl
        {
            control.ContextRequestedActionWithArgs = contextrequestedActionWithArgs;
            return control;
        }
    }
}
