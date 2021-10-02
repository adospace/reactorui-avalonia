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
    public partial interface IRxContentControl : IRxTemplatedControl
    {
        PropertyValue<HorizontalAlignment>? HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? VerticalContentAlignment { get; set; }

    }

    public partial class RxContentControl<T> : RxTemplatedControl<T>, IRxContentControl where T : ContentControl, new()
    {
        public RxContentControl()
        {

        }

        public RxContentControl(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<HorizontalAlignment>? IRxContentControl.HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? IRxContentControl.VerticalContentAlignment { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            OnBeginUpdate();

            var thisAsIRxContentControl = (IRxContentControl)this;
            NativeControl.Set(ContentControl.HorizontalContentAlignmentProperty, thisAsIRxContentControl.HorizontalContentAlignment);
            NativeControl.Set(ContentControl.VerticalContentAlignmentProperty, thisAsIRxContentControl.VerticalContentAlignment);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxContentControl : RxContentControl<ContentControl>
    {
        public RxContentControl()
        {

        }

        public RxContentControl(Action<ContentControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxContentControlExtensions
    {
        public static T HorizontalContentAlignment<T>(this T contentcontrol, HorizontalAlignment horizontalContentAlignment) where T : IRxContentControl
        {
            contentcontrol.HorizontalContentAlignment = new PropertyValue<HorizontalAlignment>(horizontalContentAlignment);
            return contentcontrol;
        }
        public static T VerticalContentAlignment<T>(this T contentcontrol, VerticalAlignment verticalContentAlignment) where T : IRxContentControl
        {
            contentcontrol.VerticalContentAlignment = new PropertyValue<VerticalAlignment>(verticalContentAlignment);
            return contentcontrol;
        }
    }
}
