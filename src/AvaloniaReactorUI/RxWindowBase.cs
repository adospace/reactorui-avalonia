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
    public partial interface IRxWindowBase : IRxTopLevel
    {
        PropertyValue<bool> Topmost { get; set; }

    }

    public partial class RxWindowBase<T> : RxTopLevel<T>, IRxWindowBase where T : WindowBase, new()
    {
        public RxWindowBase()
        {

        }

        public RxWindowBase(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool> IRxWindowBase.Topmost { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxWindowBase = (IRxWindowBase)this;
            NativeControl.Set(WindowBase.TopmostProperty, thisAsIRxWindowBase.Topmost);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxWindowBase = (IRxWindowBase)this;

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
    public static partial class RxWindowBaseExtensions
    {
        public static T Topmost<T>(this T windowbase, bool topmost) where T : IRxWindowBase
        {
            windowbase.Topmost = new PropertyValue<bool>(topmost);
            return windowbase;
        }
    }
}
