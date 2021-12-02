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
    public partial interface IRxThumb : IRxTemplatedControl
    {

        Action? DragStartedAction { get; set; }
        Action<VectorEventArgs>? DragStartedActionWithArgs { get; set; }
        Action? DragDeltaAction { get; set; }
        Action<VectorEventArgs>? DragDeltaActionWithArgs { get; set; }
        Action? DragCompletedAction { get; set; }
        Action<VectorEventArgs>? DragCompletedActionWithArgs { get; set; }
    }

    public partial class RxThumb<T> : RxTemplatedControl<T>, IRxThumb where T : Thumb, new()
    {
        public RxThumb()
        {

        }

        public RxThumb(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }


        Action? IRxThumb.DragStartedAction { get; set; }
        Action<VectorEventArgs>? IRxThumb.DragStartedActionWithArgs { get; set; }
        Action? IRxThumb.DragDeltaAction { get; set; }
        Action<VectorEventArgs>? IRxThumb.DragDeltaActionWithArgs { get; set; }
        Action? IRxThumb.DragCompletedAction { get; set; }
        Action<VectorEventArgs>? IRxThumb.DragCompletedActionWithArgs { get; set; }

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

            var thisAsIRxThumb = (IRxThumb)this;
            if (thisAsIRxThumb.DragStartedAction != null || thisAsIRxThumb.DragStartedActionWithArgs != null)
            {
                NativeControl.DragStarted += NativeControl_DragStarted;
            }
            if (thisAsIRxThumb.DragDeltaAction != null || thisAsIRxThumb.DragDeltaActionWithArgs != null)
            {
                NativeControl.DragDelta += NativeControl_DragDelta;
            }
            if (thisAsIRxThumb.DragCompletedAction != null || thisAsIRxThumb.DragCompletedActionWithArgs != null)
            {
                NativeControl.DragCompleted += NativeControl_DragCompleted;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_DragStarted(object? sender, VectorEventArgs e)
        {
            var thisAsIRxThumb = (IRxThumb)this;
            thisAsIRxThumb.DragStartedAction?.Invoke();
            thisAsIRxThumb.DragStartedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DragDelta(object? sender, VectorEventArgs e)
        {
            var thisAsIRxThumb = (IRxThumb)this;
            thisAsIRxThumb.DragDeltaAction?.Invoke();
            thisAsIRxThumb.DragDeltaActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DragCompleted(object? sender, VectorEventArgs e)
        {
            var thisAsIRxThumb = (IRxThumb)this;
            thisAsIRxThumb.DragCompletedAction?.Invoke();
            thisAsIRxThumb.DragCompletedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.DragStarted -= NativeControl_DragStarted;
                NativeControl.DragDelta -= NativeControl_DragDelta;
                NativeControl.DragCompleted -= NativeControl_DragCompleted;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxThumb : RxThumb<Thumb>
    {
        public RxThumb()
        {

        }

        public RxThumb(Action<Thumb?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxThumbExtensions
    {
        public static T OnDragStarted<T>(this T thumb, Action dragstartedAction) where T : IRxThumb
        {
            thumb.DragStartedAction = dragstartedAction;
            return thumb;
        }

        public static T OnDragStarted<T>(this T thumb, Action<VectorEventArgs> dragstartedActionWithArgs) where T : IRxThumb
        {
            thumb.DragStartedActionWithArgs = dragstartedActionWithArgs;
            return thumb;
        }
        public static T OnDragDelta<T>(this T thumb, Action dragdeltaAction) where T : IRxThumb
        {
            thumb.DragDeltaAction = dragdeltaAction;
            return thumb;
        }

        public static T OnDragDelta<T>(this T thumb, Action<VectorEventArgs> dragdeltaActionWithArgs) where T : IRxThumb
        {
            thumb.DragDeltaActionWithArgs = dragdeltaActionWithArgs;
            return thumb;
        }
        public static T OnDragCompleted<T>(this T thumb, Action dragcompletedAction) where T : IRxThumb
        {
            thumb.DragCompletedAction = dragcompletedAction;
            return thumb;
        }

        public static T OnDragCompleted<T>(this T thumb, Action<VectorEventArgs> dragcompletedActionWithArgs) where T : IRxThumb
        {
            thumb.DragCompletedActionWithArgs = dragcompletedActionWithArgs;
            return thumb;
        }
    }
}
