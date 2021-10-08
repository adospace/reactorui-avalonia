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
    public partial interface IRxGridSplitter : IRxThumb
    {
        PropertyValue<GridResizeDirection>? ResizeDirection { get; set; }
        PropertyValue<GridResizeBehavior>? ResizeBehavior { get; set; }
        PropertyValue<bool>? ShowsPreview { get; set; }
        PropertyValue<double>? KeyboardIncrement { get; set; }
        PropertyValue<double>? DragIncrement { get; set; }

    }

    public partial class RxGridSplitter<T> : RxThumb<T>, IRxGridSplitter where T : GridSplitter, new()
    {
        public RxGridSplitter()
        {

        }

        public RxGridSplitter(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<GridResizeDirection>? IRxGridSplitter.ResizeDirection { get; set; }
        PropertyValue<GridResizeBehavior>? IRxGridSplitter.ResizeBehavior { get; set; }
        PropertyValue<bool>? IRxGridSplitter.ShowsPreview { get; set; }
        PropertyValue<double>? IRxGridSplitter.KeyboardIncrement { get; set; }
        PropertyValue<double>? IRxGridSplitter.DragIncrement { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxGridSplitter = (IRxGridSplitter)this;
            NativeControl.Set(GridSplitter.ResizeDirectionProperty, thisAsIRxGridSplitter.ResizeDirection);
            NativeControl.Set(GridSplitter.ResizeBehaviorProperty, thisAsIRxGridSplitter.ResizeBehavior);
            NativeControl.Set(GridSplitter.ShowsPreviewProperty, thisAsIRxGridSplitter.ShowsPreview);
            NativeControl.Set(GridSplitter.KeyboardIncrementProperty, thisAsIRxGridSplitter.KeyboardIncrement);
            NativeControl.Set(GridSplitter.DragIncrementProperty, thisAsIRxGridSplitter.DragIncrement);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxGridSplitter : RxGridSplitter<GridSplitter>
    {
        public RxGridSplitter()
        {

        }

        public RxGridSplitter(Action<GridSplitter?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxGridSplitterExtensions
    {
        public static T ResizeDirection<T>(this T gridsplitter, GridResizeDirection resizeDirection) where T : IRxGridSplitter
        {
            gridsplitter.ResizeDirection = new PropertyValue<GridResizeDirection>(resizeDirection);
            return gridsplitter;
        }
        public static T ResizeBehavior<T>(this T gridsplitter, GridResizeBehavior resizeBehavior) where T : IRxGridSplitter
        {
            gridsplitter.ResizeBehavior = new PropertyValue<GridResizeBehavior>(resizeBehavior);
            return gridsplitter;
        }
        public static T ShowsPreview<T>(this T gridsplitter, bool showsPreview) where T : IRxGridSplitter
        {
            gridsplitter.ShowsPreview = new PropertyValue<bool>(showsPreview);
            return gridsplitter;
        }
        public static T KeyboardIncrement<T>(this T gridsplitter, double keyboardIncrement) where T : IRxGridSplitter
        {
            gridsplitter.KeyboardIncrement = new PropertyValue<double>(keyboardIncrement);
            return gridsplitter;
        }
        public static T DragIncrement<T>(this T gridsplitter, double dragIncrement) where T : IRxGridSplitter
        {
            gridsplitter.DragIncrement = new PropertyValue<double>(dragIncrement);
            return gridsplitter;
        }
    }
}
