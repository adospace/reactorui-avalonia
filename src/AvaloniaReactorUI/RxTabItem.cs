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
    public partial interface IRxTabItem : IRxHeaderedContentControl
    {
        PropertyValue<bool>? IsSelected { get; set; }

    }

    public partial class RxTabItem<T> : RxHeaderedContentControl<T>, IRxTabItem where T : TabItem, new()
    {
        public RxTabItem()
        {

        }

        public RxTabItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxTabItem.IsSelected { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxTabItem = (IRxTabItem)this;
            NativeControl.Set(TabItem.IsSelectedProperty, thisAsIRxTabItem.IsSelected);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxTabItem : RxTabItem<TabItem>
    {
        public RxTabItem()
        {

        }

        public RxTabItem(Action<TabItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTabItemExtensions
    {
        public static T IsSelected<T>(this T tabitem, bool isSelected) where T : IRxTabItem
        {
            tabitem.IsSelected = new PropertyValue<bool>(isSelected);
            return tabitem;
        }
    }
}
