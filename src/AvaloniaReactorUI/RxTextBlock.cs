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
    public partial interface IRxTextBlock : IRxControl
    {
        PropertyValue<IBrush> Background { get; set; }
        PropertyValue<Thickness> Padding { get; set; }
        PropertyValue<FontFamily> FontFamily { get; set; }
        PropertyValue<double> FontSize { get; set; }
        PropertyValue<FontStyle> FontStyle { get; set; }
        PropertyValue<FontWeight> FontWeight { get; set; }
        PropertyValue<IBrush> Foreground { get; set; }
        PropertyValue<double> LineHeight { get; set; }
        PropertyValue<int> MaxLines { get; set; }
        PropertyValue<string> Text { get; set; }
        PropertyValue<TextAlignment> TextAlignment { get; set; }
        PropertyValue<TextWrapping> TextWrapping { get; set; }
        PropertyValue<TextTrimming> TextTrimming { get; set; }
        PropertyValue<TextDecorationCollection> TextDecorations { get; set; }

    }

    public partial class RxTextBlock<T> : RxControl<T>, IRxTextBlock where T : TextBlock, new()
    {
        public RxTextBlock()
        {

        }

        public RxTextBlock(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IBrush> IRxTextBlock.Background { get; set; }
        PropertyValue<Thickness> IRxTextBlock.Padding { get; set; }
        PropertyValue<FontFamily> IRxTextBlock.FontFamily { get; set; }
        PropertyValue<double> IRxTextBlock.FontSize { get; set; }
        PropertyValue<FontStyle> IRxTextBlock.FontStyle { get; set; }
        PropertyValue<FontWeight> IRxTextBlock.FontWeight { get; set; }
        PropertyValue<IBrush> IRxTextBlock.Foreground { get; set; }
        PropertyValue<double> IRxTextBlock.LineHeight { get; set; }
        PropertyValue<int> IRxTextBlock.MaxLines { get; set; }
        PropertyValue<string> IRxTextBlock.Text { get; set; }
        PropertyValue<TextAlignment> IRxTextBlock.TextAlignment { get; set; }
        PropertyValue<TextWrapping> IRxTextBlock.TextWrapping { get; set; }
        PropertyValue<TextTrimming> IRxTextBlock.TextTrimming { get; set; }
        PropertyValue<TextDecorationCollection> IRxTextBlock.TextDecorations { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxTextBlock = (IRxTextBlock)this;
            NativeControl.Set(TextBlock.BackgroundProperty, thisAsIRxTextBlock.Background);
            NativeControl.Set(TextBlock.PaddingProperty, thisAsIRxTextBlock.Padding);
            NativeControl.Set(TextBlock.FontFamilyProperty, thisAsIRxTextBlock.FontFamily);
            NativeControl.Set(TextBlock.FontSizeProperty, thisAsIRxTextBlock.FontSize);
            NativeControl.Set(TextBlock.FontStyleProperty, thisAsIRxTextBlock.FontStyle);
            NativeControl.Set(TextBlock.FontWeightProperty, thisAsIRxTextBlock.FontWeight);
            NativeControl.Set(TextBlock.ForegroundProperty, thisAsIRxTextBlock.Foreground);
            NativeControl.Set(TextBlock.LineHeightProperty, thisAsIRxTextBlock.LineHeight);
            NativeControl.Set(TextBlock.MaxLinesProperty, thisAsIRxTextBlock.MaxLines);
            NativeControl.Set(TextBlock.TextProperty, thisAsIRxTextBlock.Text);
            NativeControl.Set(TextBlock.TextAlignmentProperty, thisAsIRxTextBlock.TextAlignment);
            NativeControl.Set(TextBlock.TextWrappingProperty, thisAsIRxTextBlock.TextWrapping);
            NativeControl.Set(TextBlock.TextTrimmingProperty, thisAsIRxTextBlock.TextTrimming);
            NativeControl.Set(TextBlock.TextDecorationsProperty, thisAsIRxTextBlock.TextDecorations);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxTextBlock = (IRxTextBlock)this;

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
    public partial class RxTextBlock : RxTextBlock<TextBlock>
    {
        public RxTextBlock()
        {

        }

        public RxTextBlock(Action<TextBlock> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTextBlockExtensions
    {
        public static T Background<T>(this T textblock, IBrush background) where T : IRxTextBlock
        {
            textblock.Background = new PropertyValue<IBrush>(background);
            return textblock;
        }
        public static T Padding<T>(this T textblock, Thickness padding) where T : IRxTextBlock
        {
            textblock.Padding = new PropertyValue<Thickness>(padding);
            return textblock;
        }
        public static T Padding<T>(this T textblock, double leftRight, double topBottom) where T : IRxTextBlock
        {
            textblock.Padding = new PropertyValue<Thickness>(new Thickness(leftRight, topBottom));
            return textblock;
        }
        public static T Padding<T>(this T textblock, double uniformSize) where T : IRxTextBlock
        {
            textblock.Padding = new PropertyValue<Thickness>(new Thickness(uniformSize));
            return textblock;
        }
        public static T FontFamily<T>(this T textblock, FontFamily fontFamily) where T : IRxTextBlock
        {
            textblock.FontFamily = new PropertyValue<FontFamily>(fontFamily);
            return textblock;
        }
        public static T FontSize<T>(this T textblock, double fontSize) where T : IRxTextBlock
        {
            textblock.FontSize = new PropertyValue<double>(fontSize);
            return textblock;
        }
        public static T FontStyle<T>(this T textblock, FontStyle fontStyle) where T : IRxTextBlock
        {
            textblock.FontStyle = new PropertyValue<FontStyle>(fontStyle);
            return textblock;
        }
        public static T FontWeight<T>(this T textblock, FontWeight fontWeight) where T : IRxTextBlock
        {
            textblock.FontWeight = new PropertyValue<FontWeight>(fontWeight);
            return textblock;
        }
        public static T Foreground<T>(this T textblock, IBrush foreground) where T : IRxTextBlock
        {
            textblock.Foreground = new PropertyValue<IBrush>(foreground);
            return textblock;
        }
        public static T LineHeight<T>(this T textblock, double lineHeight) where T : IRxTextBlock
        {
            textblock.LineHeight = new PropertyValue<double>(lineHeight);
            return textblock;
        }
        public static T MaxLines<T>(this T textblock, int maxLines) where T : IRxTextBlock
        {
            textblock.MaxLines = new PropertyValue<int>(maxLines);
            return textblock;
        }
        public static T Text<T>(this T textblock, string text) where T : IRxTextBlock
        {
            textblock.Text = new PropertyValue<string>(text);
            return textblock;
        }
        public static T TextAlignment<T>(this T textblock, TextAlignment textAlignment) where T : IRxTextBlock
        {
            textblock.TextAlignment = new PropertyValue<TextAlignment>(textAlignment);
            return textblock;
        }
        public static T TextWrapping<T>(this T textblock, TextWrapping textWrapping) where T : IRxTextBlock
        {
            textblock.TextWrapping = new PropertyValue<TextWrapping>(textWrapping);
            return textblock;
        }
        public static T TextTrimming<T>(this T textblock, TextTrimming textTrimming) where T : IRxTextBlock
        {
            textblock.TextTrimming = new PropertyValue<TextTrimming>(textTrimming);
            return textblock;
        }
        public static T TextDecorations<T>(this T textblock, TextDecorationCollection textDecorations) where T : IRxTextBlock
        {
            textblock.TextDecorations = new PropertyValue<TextDecorationCollection>(textDecorations);
            return textblock;
        }
    }
}
