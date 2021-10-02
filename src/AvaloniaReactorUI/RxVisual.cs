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
    public partial interface IRxVisual : IRxStyledElement
    {
        PropertyValue<bool>? ClipToBounds { get; set; }
        PropertyValue<Geometry?>? Clip { get; set; }
        PropertyValue<bool>? IsVisible { get; set; }
        PropertyValue<double>? Opacity { get; set; }
        PropertyValue<IBrush?>? OpacityMask { get; set; }
        PropertyValue<ITransform?>? RenderTransform { get; set; }
        PropertyValue<RelativePoint>? RenderTransformOrigin { get; set; }
        PropertyValue<int>? ZIndex { get; set; }

    }

    public partial class RxVisual<T> : RxStyledElement<T>, IRxVisual where T : Visual, new()
    {
        public RxVisual()
        {

        }

        public RxVisual(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxVisual.ClipToBounds { get; set; }
        PropertyValue<Geometry?>? IRxVisual.Clip { get; set; }
        PropertyValue<bool>? IRxVisual.IsVisible { get; set; }
        PropertyValue<double>? IRxVisual.Opacity { get; set; }
        PropertyValue<IBrush?>? IRxVisual.OpacityMask { get; set; }
        PropertyValue<ITransform?>? IRxVisual.RenderTransform { get; set; }
        PropertyValue<RelativePoint>? IRxVisual.RenderTransformOrigin { get; set; }
        PropertyValue<int>? IRxVisual.ZIndex { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            OnBeginUpdate();

            var thisAsIRxVisual = (IRxVisual)this;
            NativeControl.Set(Visual.ClipToBoundsProperty, thisAsIRxVisual.ClipToBounds);
            NativeControl.SetNullable(Visual.ClipProperty, thisAsIRxVisual.Clip);
            NativeControl.Set(Visual.IsVisibleProperty, thisAsIRxVisual.IsVisible);
            NativeControl.Set(Visual.OpacityProperty, thisAsIRxVisual.Opacity);
            NativeControl.SetNullable(Visual.OpacityMaskProperty, thisAsIRxVisual.OpacityMask);
            NativeControl.SetNullable(Visual.RenderTransformProperty, thisAsIRxVisual.RenderTransform);
            NativeControl.Set(Visual.RenderTransformOriginProperty, thisAsIRxVisual.RenderTransformOrigin);
            NativeControl.Set(Visual.ZIndexProperty, thisAsIRxVisual.ZIndex);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxVisual : RxVisual<Visual>
    {
        public RxVisual()
        {

        }

        public RxVisual(Action<Visual?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxVisualExtensions
    {
        public static T ClipToBounds<T>(this T visual, bool clipToBounds) where T : IRxVisual
        {
            visual.ClipToBounds = new PropertyValue<bool>(clipToBounds);
            return visual;
        }
        public static T Clip<T>(this T visual, Geometry? clip) where T : IRxVisual
        {
            visual.Clip = new PropertyValue<Geometry?>(clip);
            return visual;
        }
        public static T IsVisible<T>(this T visual, bool isVisible) where T : IRxVisual
        {
            visual.IsVisible = new PropertyValue<bool>(isVisible);
            return visual;
        }
        public static T Opacity<T>(this T visual, double opacity) where T : IRxVisual
        {
            visual.Opacity = new PropertyValue<double>(opacity);
            return visual;
        }
        public static T OpacityMask<T>(this T visual, IBrush? opacityMask) where T : IRxVisual
        {
            visual.OpacityMask = new PropertyValue<IBrush?>(opacityMask);
            return visual;
        }
        public static T RenderTransform<T>(this T visual, ITransform? renderTransform) where T : IRxVisual
        {
            visual.RenderTransform = new PropertyValue<ITransform?>(renderTransform);
            return visual;
        }
        public static T RenderTransformOrigin<T>(this T visual, RelativePoint renderTransformOrigin) where T : IRxVisual
        {
            visual.RenderTransformOrigin = new PropertyValue<RelativePoint>(renderTransformOrigin);
            return visual;
        }
        public static T ZIndex<T>(this T visual, int zIndex) where T : IRxVisual
        {
            visual.ZIndex = new PropertyValue<int>(zIndex);
            return visual;
        }
    }
}
