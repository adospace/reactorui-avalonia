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
    public partial interface IRxItemsRepeater : IRxPanel
    {
        PropertyValue<double>? HorizontalCacheLength { get; set; }
        PropertyValue<IEnumerable>? Items { get; set; }
        PropertyValue<AttachedLayout>? Layout { get; set; }
        PropertyValue<double>? VerticalCacheLength { get; set; }

    }

    public partial class RxItemsRepeater<T> : RxPanel<T>, IRxItemsRepeater where T : ItemsRepeater, new()
    {
        public RxItemsRepeater()
        {

        }

        public RxItemsRepeater(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double>? IRxItemsRepeater.HorizontalCacheLength { get; set; }
        PropertyValue<IEnumerable>? IRxItemsRepeater.Items { get; set; }
        PropertyValue<AttachedLayout>? IRxItemsRepeater.Layout { get; set; }
        PropertyValue<double>? IRxItemsRepeater.VerticalCacheLength { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxItemsRepeater = (IRxItemsRepeater)this;
            NativeControl.Set(ItemsRepeater.HorizontalCacheLengthProperty, thisAsIRxItemsRepeater.HorizontalCacheLength);
            NativeControl.Set(ItemsRepeater.ItemsProperty, thisAsIRxItemsRepeater.Items);
            NativeControl.Set(ItemsRepeater.LayoutProperty, thisAsIRxItemsRepeater.Layout);
            NativeControl.Set(ItemsRepeater.VerticalCacheLengthProperty, thisAsIRxItemsRepeater.VerticalCacheLength);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxItemsRepeater : RxItemsRepeater<ItemsRepeater>
    {
        public RxItemsRepeater()
        {

        }

        public RxItemsRepeater(Action<ItemsRepeater?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxItemsRepeaterExtensions
    {
        public static T HorizontalCacheLength<T>(this T itemsrepeater, double horizontalCacheLength) where T : IRxItemsRepeater
        {
            itemsrepeater.HorizontalCacheLength = new PropertyValue<double>(horizontalCacheLength);
            return itemsrepeater;
        }
        public static T Items<T>(this T itemsrepeater, IEnumerable items) where T : IRxItemsRepeater
        {
            itemsrepeater.Items = new PropertyValue<IEnumerable>(items);
            return itemsrepeater;
        }
        public static T Layout<T>(this T itemsrepeater, AttachedLayout layout) where T : IRxItemsRepeater
        {
            itemsrepeater.Layout = new PropertyValue<AttachedLayout>(layout);
            return itemsrepeater;
        }
        public static T VerticalCacheLength<T>(this T itemsrepeater, double verticalCacheLength) where T : IRxItemsRepeater
        {
            itemsrepeater.VerticalCacheLength = new PropertyValue<double>(verticalCacheLength);
            return itemsrepeater;
        }
    }
}
