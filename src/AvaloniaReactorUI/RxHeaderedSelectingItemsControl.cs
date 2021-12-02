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
    public partial interface IRxHeaderedSelectingItemsControl : IRxSelectingItemsControl
    {
        PropertyValue<object>? Header { get; set; }

    }

    public partial class RxHeaderedSelectingItemsControl<T> : RxSelectingItemsControl<T>, IRxHeaderedSelectingItemsControl where T : HeaderedSelectingItemsControl, new()
    {
        public RxHeaderedSelectingItemsControl()
        {

        }

        public RxHeaderedSelectingItemsControl(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<object>? IRxHeaderedSelectingItemsControl.Header { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxHeaderedSelectingItemsControl = (IRxHeaderedSelectingItemsControl)this;
            NativeControl.Set(HeaderedSelectingItemsControl.HeaderProperty, thisAsIRxHeaderedSelectingItemsControl.Header);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxHeaderedSelectingItemsControl : RxHeaderedSelectingItemsControl<HeaderedSelectingItemsControl>
    {
        public RxHeaderedSelectingItemsControl()
        {

        }

        public RxHeaderedSelectingItemsControl(Action<HeaderedSelectingItemsControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxHeaderedSelectingItemsControlExtensions
    {
        public static T Header<T>(this T headeredselectingitemscontrol, object header) where T : IRxHeaderedSelectingItemsControl
        {
            headeredselectingitemscontrol.Header = new PropertyValue<object>(header);
            return headeredselectingitemscontrol;
        }
    }
}
