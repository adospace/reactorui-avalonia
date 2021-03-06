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

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public partial interface IRxItemsControl : IRxTemplatedControl
    {
        PropertyValue<IEnumerable> Items { get; set; }

    }

    public partial class RxItemsControl<T> : RxTemplatedControl<T>, IRxItemsControl where T : ItemsControl, new()
    {
        public RxItemsControl()
        {

        }

        public RxItemsControl(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IEnumerable> IRxItemsControl.Items { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxItemsControl = (IRxItemsControl)this;
            NativeControl.Set(ItemsControl.ItemsProperty, thisAsIRxItemsControl.Items);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxItemsControl = (IRxItemsControl)this;

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
    public partial class RxItemsControl : RxItemsControl<ItemsControl>
    {
        public RxItemsControl()
        {

        }

        public RxItemsControl(Action<ItemsControl> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxItemsControlExtensions
    {
        public static T Items<T>(this T itemscontrol, IEnumerable items) where T : IRxItemsControl
        {
            itemscontrol.Items = new PropertyValue<IEnumerable>(items);
            return itemscontrol;
        }
    }
}
