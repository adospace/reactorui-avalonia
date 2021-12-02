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
    public partial interface IRxTabControl : IRxSelectingItemsControl
    {
        PropertyValue<Dock>? TabStripPlacement { get; set; }
        PropertyValue<HorizontalAlignment>? HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? VerticalContentAlignment { get; set; }

    }

    public partial class RxTabControl<T> : RxSelectingItemsControl<T>, IRxTabControl where T : TabControl, new()
    {
        public RxTabControl()
        {

        }

        public RxTabControl(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Dock>? IRxTabControl.TabStripPlacement { get; set; }
        PropertyValue<HorizontalAlignment>? IRxTabControl.HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? IRxTabControl.VerticalContentAlignment { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxTabControl = (IRxTabControl)this;
            NativeControl.Set(TabControl.TabStripPlacementProperty, thisAsIRxTabControl.TabStripPlacement);
            NativeControl.Set(TabControl.HorizontalContentAlignmentProperty, thisAsIRxTabControl.HorizontalContentAlignment);
            NativeControl.Set(TabControl.VerticalContentAlignmentProperty, thisAsIRxTabControl.VerticalContentAlignment);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxTabControl : RxTabControl<TabControl>
    {
        public RxTabControl()
        {

        }

        public RxTabControl(Action<TabControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTabControlExtensions
    {
        public static T TabStripPlacement<T>(this T tabcontrol, Dock tabStripPlacement) where T : IRxTabControl
        {
            tabcontrol.TabStripPlacement = new PropertyValue<Dock>(tabStripPlacement);
            return tabcontrol;
        }
        public static T HorizontalContentAlignment<T>(this T tabcontrol, HorizontalAlignment horizontalContentAlignment) where T : IRxTabControl
        {
            tabcontrol.HorizontalContentAlignment = new PropertyValue<HorizontalAlignment>(horizontalContentAlignment);
            return tabcontrol;
        }
        public static T VerticalContentAlignment<T>(this T tabcontrol, VerticalAlignment verticalContentAlignment) where T : IRxTabControl
        {
            tabcontrol.VerticalContentAlignment = new PropertyValue<VerticalAlignment>(verticalContentAlignment);
            return tabcontrol;
        }
    }
}
