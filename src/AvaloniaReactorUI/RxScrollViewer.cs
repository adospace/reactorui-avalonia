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
    public partial interface IRxScrollViewer : IRxContentControl
    {
        PropertyValue<Vector>? Offset { get; set; }
        PropertyValue<ScrollBarVisibility>? HorizontalScrollBarVisibility { get; set; }
        PropertyValue<ScrollBarVisibility>? VerticalScrollBarVisibility { get; set; }
        PropertyValue<bool>? AllowAutoHide { get; set; }

        Action? ScrollChangedAction { get; set; }
        Action<ScrollChangedEventArgs>? ScrollChangedActionWithArgs { get; set; }
    }

    public partial class RxScrollViewer<T> : RxContentControl<T>, IRxScrollViewer where T : ScrollViewer, new()
    {
        public RxScrollViewer()
        {

        }

        public RxScrollViewer(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Vector>? IRxScrollViewer.Offset { get; set; }
        PropertyValue<ScrollBarVisibility>? IRxScrollViewer.HorizontalScrollBarVisibility { get; set; }
        PropertyValue<ScrollBarVisibility>? IRxScrollViewer.VerticalScrollBarVisibility { get; set; }
        PropertyValue<bool>? IRxScrollViewer.AllowAutoHide { get; set; }

        Action? IRxScrollViewer.ScrollChangedAction { get; set; }
        Action<ScrollChangedEventArgs>? IRxScrollViewer.ScrollChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxScrollViewer = (IRxScrollViewer)this;
            NativeControl.Set(ScrollViewer.OffsetProperty, thisAsIRxScrollViewer.Offset);
            NativeControl.Set(ScrollViewer.HorizontalScrollBarVisibilityProperty, thisAsIRxScrollViewer.HorizontalScrollBarVisibility);
            NativeControl.Set(ScrollViewer.VerticalScrollBarVisibilityProperty, thisAsIRxScrollViewer.VerticalScrollBarVisibility);
            NativeControl.Set(ScrollViewer.AllowAutoHideProperty, thisAsIRxScrollViewer.AllowAutoHide);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxScrollViewer = (IRxScrollViewer)this;
            if (thisAsIRxScrollViewer.ScrollChangedAction != null || thisAsIRxScrollViewer.ScrollChangedActionWithArgs != null)
            {
                NativeControl.ScrollChanged += NativeControl_ScrollChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            var thisAsIRxScrollViewer = (IRxScrollViewer)this;
            thisAsIRxScrollViewer.ScrollChangedAction?.Invoke();
            thisAsIRxScrollViewer.ScrollChangedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.ScrollChanged -= NativeControl_ScrollChanged;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxScrollViewer : RxScrollViewer<ScrollViewer>
    {
        public RxScrollViewer()
        {

        }

        public RxScrollViewer(Action<ScrollViewer?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxScrollViewerExtensions
    {
        public static T Offset<T>(this T scrollviewer, Vector offset) where T : IRxScrollViewer
        {
            scrollviewer.Offset = new PropertyValue<Vector>(offset);
            return scrollviewer;
        }
        public static T HorizontalScrollBarVisibility<T>(this T scrollviewer, ScrollBarVisibility horizontalScrollBarVisibility) where T : IRxScrollViewer
        {
            scrollviewer.HorizontalScrollBarVisibility = new PropertyValue<ScrollBarVisibility>(horizontalScrollBarVisibility);
            return scrollviewer;
        }
        public static T VerticalScrollBarVisibility<T>(this T scrollviewer, ScrollBarVisibility verticalScrollBarVisibility) where T : IRxScrollViewer
        {
            scrollviewer.VerticalScrollBarVisibility = new PropertyValue<ScrollBarVisibility>(verticalScrollBarVisibility);
            return scrollviewer;
        }
        public static T AllowAutoHide<T>(this T scrollviewer, bool allowAutoHide) where T : IRxScrollViewer
        {
            scrollviewer.AllowAutoHide = new PropertyValue<bool>(allowAutoHide);
            return scrollviewer;
        }
        public static T OnScrollChanged<T>(this T scrollviewer, Action scrollchangedAction) where T : IRxScrollViewer
        {
            scrollviewer.ScrollChangedAction = scrollchangedAction;
            return scrollviewer;
        }

        public static T OnScrollChanged<T>(this T scrollviewer, Action<ScrollChangedEventArgs> scrollchangedActionWithArgs) where T : IRxScrollViewer
        {
            scrollviewer.ScrollChangedActionWithArgs = scrollchangedActionWithArgs;
            return scrollviewer;
        }
    }
}
