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
    public partial interface IRxHeaderedContentControl : IRxContentControl
    {
        PropertyValue<object?>? Header { get; set; }

    }

    public partial class RxHeaderedContentControl<T> : RxContentControl<T>, IRxHeaderedContentControl where T : HeaderedContentControl, new()
    {
        public RxHeaderedContentControl()
        {

        }

        public RxHeaderedContentControl(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<object?>? IRxHeaderedContentControl.Header { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxHeaderedContentControl = (IRxHeaderedContentControl)this;
            NativeControl.SetNullable(HeaderedContentControl.HeaderProperty, thisAsIRxHeaderedContentControl.Header);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxHeaderedContentControl : RxHeaderedContentControl<HeaderedContentControl>
    {
        public RxHeaderedContentControl()
        {

        }

        public RxHeaderedContentControl(Action<HeaderedContentControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxHeaderedContentControlExtensions
    {
        public static T Header<T>(this T headeredcontentcontrol, object? header) where T : IRxHeaderedContentControl
        {
            headeredcontentcontrol.Header = new PropertyValue<object?>(header);
            return headeredcontentcontrol;
        }
    }
}
