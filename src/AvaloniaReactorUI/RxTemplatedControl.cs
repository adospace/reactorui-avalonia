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
    public partial interface IRxTemplatedControl : IRxControl
    {
        PropertyValue<IBrush> Background { get; set; }
        PropertyValue<IBrush> BorderBrush { get; set; }
        PropertyValue<Thickness> BorderThickness { get; set; }
        PropertyValue<FontFamily> FontFamily { get; set; }
        PropertyValue<double> FontSize { get; set; }
        PropertyValue<FontStyle> FontStyle { get; set; }
        PropertyValue<FontWeight> FontWeight { get; set; }
        PropertyValue<IBrush> Foreground { get; set; }
        PropertyValue<Thickness> Padding { get; set; }

        Action TemplateAppliedAction { get; set; }
        Action<TemplateAppliedEventArgs> TemplateAppliedActionWithArgs { get; set; }
    }

    public partial class RxTemplatedControl<T> : RxControl<T>, IRxTemplatedControl where T : TemplatedControl, new()
    {
        public RxTemplatedControl()
        {

        }

        public RxTemplatedControl(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IBrush> IRxTemplatedControl.Background { get; set; }
        PropertyValue<IBrush> IRxTemplatedControl.BorderBrush { get; set; }
        PropertyValue<Thickness> IRxTemplatedControl.BorderThickness { get; set; }
        PropertyValue<FontFamily> IRxTemplatedControl.FontFamily { get; set; }
        PropertyValue<double> IRxTemplatedControl.FontSize { get; set; }
        PropertyValue<FontStyle> IRxTemplatedControl.FontStyle { get; set; }
        PropertyValue<FontWeight> IRxTemplatedControl.FontWeight { get; set; }
        PropertyValue<IBrush> IRxTemplatedControl.Foreground { get; set; }
        PropertyValue<Thickness> IRxTemplatedControl.Padding { get; set; }

        Action IRxTemplatedControl.TemplateAppliedAction { get; set; }
        Action<TemplateAppliedEventArgs> IRxTemplatedControl.TemplateAppliedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxTemplatedControl = (IRxTemplatedControl)this;
            NativeControl.Set(TemplatedControl.BackgroundProperty, thisAsIRxTemplatedControl.Background);
            NativeControl.Set(TemplatedControl.BorderBrushProperty, thisAsIRxTemplatedControl.BorderBrush);
            NativeControl.Set(TemplatedControl.BorderThicknessProperty, thisAsIRxTemplatedControl.BorderThickness);
            NativeControl.Set(TemplatedControl.FontFamilyProperty, thisAsIRxTemplatedControl.FontFamily);
            NativeControl.Set(TemplatedControl.FontSizeProperty, thisAsIRxTemplatedControl.FontSize);
            NativeControl.Set(TemplatedControl.FontStyleProperty, thisAsIRxTemplatedControl.FontStyle);
            NativeControl.Set(TemplatedControl.FontWeightProperty, thisAsIRxTemplatedControl.FontWeight);
            NativeControl.Set(TemplatedControl.ForegroundProperty, thisAsIRxTemplatedControl.Foreground);
            NativeControl.Set(TemplatedControl.PaddingProperty, thisAsIRxTemplatedControl.Padding);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxTemplatedControl = (IRxTemplatedControl)this;
            if (thisAsIRxTemplatedControl.TemplateAppliedAction != null || thisAsIRxTemplatedControl.TemplateAppliedActionWithArgs != null)
            {
                NativeControl.TemplateApplied += NativeControl_TemplateApplied;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_TemplateApplied(object sender, TemplateAppliedEventArgs e)
        {
            var thisAsIRxTemplatedControl = (IRxTemplatedControl)this;
            thisAsIRxTemplatedControl.TemplateAppliedAction?.Invoke();
            thisAsIRxTemplatedControl.TemplateAppliedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.TemplateApplied -= NativeControl_TemplateApplied;
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxTemplatedControl : RxTemplatedControl<TemplatedControl>
    {
        public RxTemplatedControl()
        {

        }

        public RxTemplatedControl(Action<TemplatedControl> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTemplatedControlExtensions
    {
        public static T Background<T>(this T templatedcontrol, IBrush background) where T : IRxTemplatedControl
        {
            templatedcontrol.Background = new PropertyValue<IBrush>(background);
            return templatedcontrol;
        }
        public static T BorderBrush<T>(this T templatedcontrol, IBrush borderBrush) where T : IRxTemplatedControl
        {
            templatedcontrol.BorderBrush = new PropertyValue<IBrush>(borderBrush);
            return templatedcontrol;
        }
        public static T BorderThickness<T>(this T templatedcontrol, Thickness borderThickness) where T : IRxTemplatedControl
        {
            templatedcontrol.BorderThickness = new PropertyValue<Thickness>(borderThickness);
            return templatedcontrol;
        }
        public static T BorderThickness<T>(this T templatedcontrol, double leftRight, double topBottom) where T : IRxTemplatedControl
        {
            templatedcontrol.BorderThickness = new PropertyValue<Thickness>(new Thickness(leftRight, topBottom));
            return templatedcontrol;
        }
        public static T BorderThickness<T>(this T templatedcontrol, double uniformSize) where T : IRxTemplatedControl
        {
            templatedcontrol.BorderThickness = new PropertyValue<Thickness>(new Thickness(uniformSize));
            return templatedcontrol;
        }
        public static T FontFamily<T>(this T templatedcontrol, FontFamily fontFamily) where T : IRxTemplatedControl
        {
            templatedcontrol.FontFamily = new PropertyValue<FontFamily>(fontFamily);
            return templatedcontrol;
        }
        public static T FontSize<T>(this T templatedcontrol, double fontSize) where T : IRxTemplatedControl
        {
            templatedcontrol.FontSize = new PropertyValue<double>(fontSize);
            return templatedcontrol;
        }
        public static T FontStyle<T>(this T templatedcontrol, FontStyle fontStyle) where T : IRxTemplatedControl
        {
            templatedcontrol.FontStyle = new PropertyValue<FontStyle>(fontStyle);
            return templatedcontrol;
        }
        public static T FontWeight<T>(this T templatedcontrol, FontWeight fontWeight) where T : IRxTemplatedControl
        {
            templatedcontrol.FontWeight = new PropertyValue<FontWeight>(fontWeight);
            return templatedcontrol;
        }
        public static T Foreground<T>(this T templatedcontrol, IBrush foreground) where T : IRxTemplatedControl
        {
            templatedcontrol.Foreground = new PropertyValue<IBrush>(foreground);
            return templatedcontrol;
        }
        public static T Padding<T>(this T templatedcontrol, Thickness padding) where T : IRxTemplatedControl
        {
            templatedcontrol.Padding = new PropertyValue<Thickness>(padding);
            return templatedcontrol;
        }
        public static T Padding<T>(this T templatedcontrol, double leftRight, double topBottom) where T : IRxTemplatedControl
        {
            templatedcontrol.Padding = new PropertyValue<Thickness>(new Thickness(leftRight, topBottom));
            return templatedcontrol;
        }
        public static T Padding<T>(this T templatedcontrol, double uniformSize) where T : IRxTemplatedControl
        {
            templatedcontrol.Padding = new PropertyValue<Thickness>(new Thickness(uniformSize));
            return templatedcontrol;
        }
        public static T OnTemplateApplied<T>(this T templatedcontrol, Action templateappliedAction) where T : IRxTemplatedControl
        {
            templatedcontrol.TemplateAppliedAction = templateappliedAction;
            return templatedcontrol;
        }

        public static T OnTemplateApplied<T>(this T templatedcontrol, Action<TemplateAppliedEventArgs> templateappliedActionWithArgs) where T : IRxTemplatedControl
        {
            templatedcontrol.TemplateAppliedActionWithArgs = templateappliedActionWithArgs;
            return templatedcontrol;
        }
    }
}
