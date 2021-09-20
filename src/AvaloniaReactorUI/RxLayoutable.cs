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
    public partial interface IRxLayoutable : IRxVisual
    {
        PropertyValue<double> Width { get; set; }
        PropertyValue<double> Height { get; set; }
        PropertyValue<double> MinWidth { get; set; }
        PropertyValue<double> MaxWidth { get; set; }
        PropertyValue<double> MinHeight { get; set; }
        PropertyValue<double> MaxHeight { get; set; }
        PropertyValue<Thickness> Margin { get; set; }
        PropertyValue<HorizontalAlignment> HorizontalAlignment { get; set; }
        PropertyValue<VerticalAlignment> VerticalAlignment { get; set; }
        PropertyValue<bool> UseLayoutRounding { get; set; }

    }

    public partial class RxLayoutable<T> : RxVisual<T>, IRxLayoutable where T : Layoutable, new()
    {
        public RxLayoutable()
        {

        }

        public RxLayoutable(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double> IRxLayoutable.Width { get; set; }
        PropertyValue<double> IRxLayoutable.Height { get; set; }
        PropertyValue<double> IRxLayoutable.MinWidth { get; set; }
        PropertyValue<double> IRxLayoutable.MaxWidth { get; set; }
        PropertyValue<double> IRxLayoutable.MinHeight { get; set; }
        PropertyValue<double> IRxLayoutable.MaxHeight { get; set; }
        PropertyValue<Thickness> IRxLayoutable.Margin { get; set; }
        PropertyValue<HorizontalAlignment> IRxLayoutable.HorizontalAlignment { get; set; }
        PropertyValue<VerticalAlignment> IRxLayoutable.VerticalAlignment { get; set; }
        PropertyValue<bool> IRxLayoutable.UseLayoutRounding { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxLayoutable = (IRxLayoutable)this;
            NativeControl.Set(Layoutable.WidthProperty, thisAsIRxLayoutable.Width);
            NativeControl.Set(Layoutable.HeightProperty, thisAsIRxLayoutable.Height);
            NativeControl.Set(Layoutable.MinWidthProperty, thisAsIRxLayoutable.MinWidth);
            NativeControl.Set(Layoutable.MaxWidthProperty, thisAsIRxLayoutable.MaxWidth);
            NativeControl.Set(Layoutable.MinHeightProperty, thisAsIRxLayoutable.MinHeight);
            NativeControl.Set(Layoutable.MaxHeightProperty, thisAsIRxLayoutable.MaxHeight);
            NativeControl.Set(Layoutable.MarginProperty, thisAsIRxLayoutable.Margin);
            NativeControl.Set(Layoutable.HorizontalAlignmentProperty, thisAsIRxLayoutable.HorizontalAlignment);
            NativeControl.Set(Layoutable.VerticalAlignmentProperty, thisAsIRxLayoutable.VerticalAlignment);
            NativeControl.Set(Layoutable.UseLayoutRoundingProperty, thisAsIRxLayoutable.UseLayoutRounding);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxLayoutable = (IRxLayoutable)this;

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
    public partial class RxLayoutable : RxLayoutable<Layoutable>
    {
        public RxLayoutable()
        {

        }

        public RxLayoutable(Action<Layoutable> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxLayoutableExtensions
    {
        public static T Width<T>(this T layoutable, double width) where T : IRxLayoutable
        {
            layoutable.Width = new PropertyValue<double>(width);
            return layoutable;
        }
        public static T Height<T>(this T layoutable, double height) where T : IRxLayoutable
        {
            layoutable.Height = new PropertyValue<double>(height);
            return layoutable;
        }
        public static T MinWidth<T>(this T layoutable, double minWidth) where T : IRxLayoutable
        {
            layoutable.MinWidth = new PropertyValue<double>(minWidth);
            return layoutable;
        }
        public static T MaxWidth<T>(this T layoutable, double maxWidth) where T : IRxLayoutable
        {
            layoutable.MaxWidth = new PropertyValue<double>(maxWidth);
            return layoutable;
        }
        public static T MinHeight<T>(this T layoutable, double minHeight) where T : IRxLayoutable
        {
            layoutable.MinHeight = new PropertyValue<double>(minHeight);
            return layoutable;
        }
        public static T MaxHeight<T>(this T layoutable, double maxHeight) where T : IRxLayoutable
        {
            layoutable.MaxHeight = new PropertyValue<double>(maxHeight);
            return layoutable;
        }
        public static T Margin<T>(this T layoutable, Thickness margin) where T : IRxLayoutable
        {
            layoutable.Margin = new PropertyValue<Thickness>(margin);
            return layoutable;
        }
        public static T Margin<T>(this T layoutable, double leftRight, double topBottom) where T : IRxLayoutable
        {
            layoutable.Margin = new PropertyValue<Thickness>(new Thickness(leftRight, topBottom));
            return layoutable;
        }
        public static T Margin<T>(this T layoutable, double uniformSize) where T : IRxLayoutable
        {
            layoutable.Margin = new PropertyValue<Thickness>(new Thickness(uniformSize));
            return layoutable;
        }
        public static T HorizontalAlignment<T>(this T layoutable, HorizontalAlignment horizontalAlignment) where T : IRxLayoutable
        {
            layoutable.HorizontalAlignment = new PropertyValue<HorizontalAlignment>(horizontalAlignment);
            return layoutable;
        }
        public static T VerticalAlignment<T>(this T layoutable, VerticalAlignment verticalAlignment) where T : IRxLayoutable
        {
            layoutable.VerticalAlignment = new PropertyValue<VerticalAlignment>(verticalAlignment);
            return layoutable;
        }
        public static T UseLayoutRounding<T>(this T layoutable, bool useLayoutRounding) where T : IRxLayoutable
        {
            layoutable.UseLayoutRounding = new PropertyValue<bool>(useLayoutRounding);
            return layoutable;
        }
    }
}
