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
    public partial interface IRxTopLevel : IRxContentControl
    {
        PropertyValue<WindowTransparencyLevel> TransparencyLevelHint { get; set; }
        PropertyValue<IBrush> TransparencyBackgroundFallback { get; set; }

    }

    public partial class RxTopLevel<T> : RxContentControl<T>, IRxTopLevel where T : TopLevel, new()
    {
        public RxTopLevel()
        {

        }

        public RxTopLevel(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<WindowTransparencyLevel> IRxTopLevel.TransparencyLevelHint { get; set; }
        PropertyValue<IBrush> IRxTopLevel.TransparencyBackgroundFallback { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxTopLevel = (IRxTopLevel)this;
            NativeControl.Set(TopLevel.TransparencyLevelHintProperty, thisAsIRxTopLevel.TransparencyLevelHint);
            NativeControl.Set(TopLevel.TransparencyBackgroundFallbackProperty, thisAsIRxTopLevel.TransparencyBackgroundFallback);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxTopLevel = (IRxTopLevel)this;

            base.OnAttachNativeEvents();
        }


        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
            }

            base.OnDetachNativeEvents();
        }

    }
    public static partial class RxTopLevelExtensions
    {
        public static T TransparencyLevelHint<T>(this T toplevel, WindowTransparencyLevel transparencyLevelHint) where T : IRxTopLevel
        {
            toplevel.TransparencyLevelHint = new PropertyValue<WindowTransparencyLevel>(transparencyLevelHint);
            return toplevel;
        }
        public static T TransparencyBackgroundFallback<T>(this T toplevel, IBrush transparencyBackgroundFallback) where T : IRxTopLevel
        {
            toplevel.TransparencyBackgroundFallback = new PropertyValue<IBrush>(transparencyBackgroundFallback);
            return toplevel;
        }
    }
}
