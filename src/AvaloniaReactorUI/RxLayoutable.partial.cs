using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI
{
    public static partial class RxLayoutableExtensions
    {
        public static T HLeft<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.HorizontalAlignment = new PropertyValue<Avalonia.Layout.HorizontalAlignment>(Avalonia.Layout.HorizontalAlignment.Left);
            return layoutable;
        }

        public static T HCenter<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.HorizontalAlignment = new PropertyValue<Avalonia.Layout.HorizontalAlignment>(Avalonia.Layout.HorizontalAlignment.Center);
            return layoutable;
        }

        public static T HRight<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.HorizontalAlignment = new PropertyValue<Avalonia.Layout.HorizontalAlignment>(Avalonia.Layout.HorizontalAlignment.Right);
            return layoutable;
        }

        public static T HStretch<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.HorizontalAlignment = new PropertyValue<Avalonia.Layout.HorizontalAlignment>(Avalonia.Layout.HorizontalAlignment.Stretch);
            return layoutable;
        }

        public static T VTop<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.VerticalAlignment = new PropertyValue<Avalonia.Layout.VerticalAlignment>(Avalonia.Layout.VerticalAlignment.Top);
            return layoutable;
        }

        public static T VCenter<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.VerticalAlignment = new PropertyValue<Avalonia.Layout.VerticalAlignment>(Avalonia.Layout.VerticalAlignment.Center);
            return layoutable;
        }

        public static T VBottom<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.VerticalAlignment = new PropertyValue<Avalonia.Layout.VerticalAlignment>(Avalonia.Layout.VerticalAlignment.Bottom);
            return layoutable;
        }

        public static T VStretch<T>(this T layoutable) where T : IRxLayoutable
        {
            layoutable.VerticalAlignment = new PropertyValue<Avalonia.Layout.VerticalAlignment>(Avalonia.Layout.VerticalAlignment.Stretch);
            return layoutable;
        }
    }
}
