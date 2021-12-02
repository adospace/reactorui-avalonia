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
    public partial interface IRxTextBox : IRxTemplatedControl
    {
        PropertyValue<bool>? AcceptsReturn { get; set; }
        PropertyValue<bool>? AcceptsTab { get; set; }
        PropertyValue<int>? CaretIndex { get; set; }
        PropertyValue<bool>? IsReadOnly { get; set; }
        PropertyValue<char>? PasswordChar { get; set; }
        PropertyValue<IBrush>? SelectionBrush { get; set; }
        PropertyValue<IBrush>? SelectionForegroundBrush { get; set; }
        PropertyValue<IBrush>? CaretBrush { get; set; }
        PropertyValue<int>? SelectionStart { get; set; }
        PropertyValue<int>? SelectionEnd { get; set; }
        PropertyValue<int>? MaxLength { get; set; }
        PropertyValue<string>? Text { get; set; }
        PropertyValue<TextAlignment>? TextAlignment { get; set; }
        PropertyValue<HorizontalAlignment>? HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? VerticalContentAlignment { get; set; }
        PropertyValue<TextWrapping>? TextWrapping { get; set; }
        PropertyValue<string>? Watermark { get; set; }
        PropertyValue<bool>? UseFloatingWatermark { get; set; }
        PropertyValue<string>? NewLine { get; set; }
        PropertyValue<object>? InnerLeftContent { get; set; }
        PropertyValue<object>? InnerRightContent { get; set; }
        PropertyValue<bool>? RevealPassword { get; set; }
        PropertyValue<bool>? IsUndoEnabled { get; set; }
        PropertyValue<int>? UndoLimit { get; set; }

        Action? CopyingToClipboardAction { get; set; }
        Action<RoutedEventArgs>? CopyingToClipboardActionWithArgs { get; set; }
        Action? CuttingToClipboardAction { get; set; }
        Action<RoutedEventArgs>? CuttingToClipboardActionWithArgs { get; set; }
        Action? PastingFromClipboardAction { get; set; }
        Action<RoutedEventArgs>? PastingFromClipboardActionWithArgs { get; set; }
    }

    public partial class RxTextBox<T> : RxTemplatedControl<T>, IRxTextBox where T : TextBox, new()
    {
        public RxTextBox()
        {

        }

        public RxTextBox(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxTextBox.AcceptsReturn { get; set; }
        PropertyValue<bool>? IRxTextBox.AcceptsTab { get; set; }
        PropertyValue<int>? IRxTextBox.CaretIndex { get; set; }
        PropertyValue<bool>? IRxTextBox.IsReadOnly { get; set; }
        PropertyValue<char>? IRxTextBox.PasswordChar { get; set; }
        PropertyValue<IBrush>? IRxTextBox.SelectionBrush { get; set; }
        PropertyValue<IBrush>? IRxTextBox.SelectionForegroundBrush { get; set; }
        PropertyValue<IBrush>? IRxTextBox.CaretBrush { get; set; }
        PropertyValue<int>? IRxTextBox.SelectionStart { get; set; }
        PropertyValue<int>? IRxTextBox.SelectionEnd { get; set; }
        PropertyValue<int>? IRxTextBox.MaxLength { get; set; }
        PropertyValue<string>? IRxTextBox.Text { get; set; }
        PropertyValue<TextAlignment>? IRxTextBox.TextAlignment { get; set; }
        PropertyValue<HorizontalAlignment>? IRxTextBox.HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? IRxTextBox.VerticalContentAlignment { get; set; }
        PropertyValue<TextWrapping>? IRxTextBox.TextWrapping { get; set; }
        PropertyValue<string>? IRxTextBox.Watermark { get; set; }
        PropertyValue<bool>? IRxTextBox.UseFloatingWatermark { get; set; }
        PropertyValue<string>? IRxTextBox.NewLine { get; set; }
        PropertyValue<object>? IRxTextBox.InnerLeftContent { get; set; }
        PropertyValue<object>? IRxTextBox.InnerRightContent { get; set; }
        PropertyValue<bool>? IRxTextBox.RevealPassword { get; set; }
        PropertyValue<bool>? IRxTextBox.IsUndoEnabled { get; set; }
        PropertyValue<int>? IRxTextBox.UndoLimit { get; set; }

        Action? IRxTextBox.CopyingToClipboardAction { get; set; }
        Action<RoutedEventArgs>? IRxTextBox.CopyingToClipboardActionWithArgs { get; set; }
        Action? IRxTextBox.CuttingToClipboardAction { get; set; }
        Action<RoutedEventArgs>? IRxTextBox.CuttingToClipboardActionWithArgs { get; set; }
        Action? IRxTextBox.PastingFromClipboardAction { get; set; }
        Action<RoutedEventArgs>? IRxTextBox.PastingFromClipboardActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxTextBox = (IRxTextBox)this;
            NativeControl.Set(TextBox.AcceptsReturnProperty, thisAsIRxTextBox.AcceptsReturn);
            NativeControl.Set(TextBox.AcceptsTabProperty, thisAsIRxTextBox.AcceptsTab);
            NativeControl.Set(TextBox.CaretIndexProperty, thisAsIRxTextBox.CaretIndex);
            NativeControl.Set(TextBox.IsReadOnlyProperty, thisAsIRxTextBox.IsReadOnly);
            NativeControl.Set(TextBox.PasswordCharProperty, thisAsIRxTextBox.PasswordChar);
            NativeControl.Set(TextBox.SelectionBrushProperty, thisAsIRxTextBox.SelectionBrush);
            NativeControl.Set(TextBox.SelectionForegroundBrushProperty, thisAsIRxTextBox.SelectionForegroundBrush);
            NativeControl.Set(TextBox.CaretBrushProperty, thisAsIRxTextBox.CaretBrush);
            NativeControl.Set(TextBox.SelectionStartProperty, thisAsIRxTextBox.SelectionStart);
            NativeControl.Set(TextBox.SelectionEndProperty, thisAsIRxTextBox.SelectionEnd);
            NativeControl.Set(TextBox.MaxLengthProperty, thisAsIRxTextBox.MaxLength);
            NativeControl.Set(TextBox.TextProperty, thisAsIRxTextBox.Text);
            NativeControl.Set(TextBox.TextAlignmentProperty, thisAsIRxTextBox.TextAlignment);
            NativeControl.Set(TextBox.HorizontalContentAlignmentProperty, thisAsIRxTextBox.HorizontalContentAlignment);
            NativeControl.Set(TextBox.VerticalContentAlignmentProperty, thisAsIRxTextBox.VerticalContentAlignment);
            NativeControl.Set(TextBox.TextWrappingProperty, thisAsIRxTextBox.TextWrapping);
            NativeControl.Set(TextBox.WatermarkProperty, thisAsIRxTextBox.Watermark);
            NativeControl.Set(TextBox.UseFloatingWatermarkProperty, thisAsIRxTextBox.UseFloatingWatermark);
            NativeControl.Set(TextBox.NewLineProperty, thisAsIRxTextBox.NewLine);
            NativeControl.Set(TextBox.InnerLeftContentProperty, thisAsIRxTextBox.InnerLeftContent);
            NativeControl.Set(TextBox.InnerRightContentProperty, thisAsIRxTextBox.InnerRightContent);
            NativeControl.Set(TextBox.RevealPasswordProperty, thisAsIRxTextBox.RevealPassword);
            NativeControl.Set(TextBox.IsUndoEnabledProperty, thisAsIRxTextBox.IsUndoEnabled);
            NativeControl.Set(TextBox.UndoLimitProperty, thisAsIRxTextBox.UndoLimit);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxTextBox = (IRxTextBox)this;
            if (thisAsIRxTextBox.CopyingToClipboardAction != null || thisAsIRxTextBox.CopyingToClipboardActionWithArgs != null)
            {
                NativeControl.CopyingToClipboard += NativeControl_CopyingToClipboard;
            }
            if (thisAsIRxTextBox.CuttingToClipboardAction != null || thisAsIRxTextBox.CuttingToClipboardActionWithArgs != null)
            {
                NativeControl.CuttingToClipboard += NativeControl_CuttingToClipboard;
            }
            if (thisAsIRxTextBox.PastingFromClipboardAction != null || thisAsIRxTextBox.PastingFromClipboardActionWithArgs != null)
            {
                NativeControl.PastingFromClipboard += NativeControl_PastingFromClipboard;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_CopyingToClipboard(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxTextBox = (IRxTextBox)this;
            thisAsIRxTextBox.CopyingToClipboardAction?.Invoke();
            thisAsIRxTextBox.CopyingToClipboardActionWithArgs?.Invoke(e);
        }
        private void NativeControl_CuttingToClipboard(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxTextBox = (IRxTextBox)this;
            thisAsIRxTextBox.CuttingToClipboardAction?.Invoke();
            thisAsIRxTextBox.CuttingToClipboardActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PastingFromClipboard(object? sender, RoutedEventArgs e)
        {
            var thisAsIRxTextBox = (IRxTextBox)this;
            thisAsIRxTextBox.PastingFromClipboardAction?.Invoke();
            thisAsIRxTextBox.PastingFromClipboardActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.CopyingToClipboard -= NativeControl_CopyingToClipboard;
                NativeControl.CuttingToClipboard -= NativeControl_CuttingToClipboard;
                NativeControl.PastingFromClipboard -= NativeControl_PastingFromClipboard;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxTextBox : RxTextBox<TextBox>
    {
        public RxTextBox()
        {

        }

        public RxTextBox(Action<TextBox?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxTextBoxExtensions
    {
        public static T AcceptsReturn<T>(this T textbox, bool acceptsReturn) where T : IRxTextBox
        {
            textbox.AcceptsReturn = new PropertyValue<bool>(acceptsReturn);
            return textbox;
        }
        public static T AcceptsTab<T>(this T textbox, bool acceptsTab) where T : IRxTextBox
        {
            textbox.AcceptsTab = new PropertyValue<bool>(acceptsTab);
            return textbox;
        }
        public static T CaretIndex<T>(this T textbox, int caretIndex) where T : IRxTextBox
        {
            textbox.CaretIndex = new PropertyValue<int>(caretIndex);
            return textbox;
        }
        public static T IsReadOnly<T>(this T textbox, bool isReadOnly) where T : IRxTextBox
        {
            textbox.IsReadOnly = new PropertyValue<bool>(isReadOnly);
            return textbox;
        }
        public static T PasswordChar<T>(this T textbox, char passwordChar) where T : IRxTextBox
        {
            textbox.PasswordChar = new PropertyValue<char>(passwordChar);
            return textbox;
        }
        public static T SelectionBrush<T>(this T textbox, IBrush selectionBrush) where T : IRxTextBox
        {
            textbox.SelectionBrush = new PropertyValue<IBrush>(selectionBrush);
            return textbox;
        }
        public static T SelectionForegroundBrush<T>(this T textbox, IBrush selectionForegroundBrush) where T : IRxTextBox
        {
            textbox.SelectionForegroundBrush = new PropertyValue<IBrush>(selectionForegroundBrush);
            return textbox;
        }
        public static T CaretBrush<T>(this T textbox, IBrush caretBrush) where T : IRxTextBox
        {
            textbox.CaretBrush = new PropertyValue<IBrush>(caretBrush);
            return textbox;
        }
        public static T SelectionStart<T>(this T textbox, int selectionStart) where T : IRxTextBox
        {
            textbox.SelectionStart = new PropertyValue<int>(selectionStart);
            return textbox;
        }
        public static T SelectionEnd<T>(this T textbox, int selectionEnd) where T : IRxTextBox
        {
            textbox.SelectionEnd = new PropertyValue<int>(selectionEnd);
            return textbox;
        }
        public static T MaxLength<T>(this T textbox, int maxLength) where T : IRxTextBox
        {
            textbox.MaxLength = new PropertyValue<int>(maxLength);
            return textbox;
        }
        public static T Text<T>(this T textbox, string text) where T : IRxTextBox
        {
            textbox.Text = new PropertyValue<string>(text);
            return textbox;
        }
        public static T TextAlignment<T>(this T textbox, TextAlignment textAlignment) where T : IRxTextBox
        {
            textbox.TextAlignment = new PropertyValue<TextAlignment>(textAlignment);
            return textbox;
        }
        public static T HorizontalContentAlignment<T>(this T textbox, HorizontalAlignment horizontalContentAlignment) where T : IRxTextBox
        {
            textbox.HorizontalContentAlignment = new PropertyValue<HorizontalAlignment>(horizontalContentAlignment);
            return textbox;
        }
        public static T VerticalContentAlignment<T>(this T textbox, VerticalAlignment verticalContentAlignment) where T : IRxTextBox
        {
            textbox.VerticalContentAlignment = new PropertyValue<VerticalAlignment>(verticalContentAlignment);
            return textbox;
        }
        public static T TextWrapping<T>(this T textbox, TextWrapping textWrapping) where T : IRxTextBox
        {
            textbox.TextWrapping = new PropertyValue<TextWrapping>(textWrapping);
            return textbox;
        }
        public static T Watermark<T>(this T textbox, string watermark) where T : IRxTextBox
        {
            textbox.Watermark = new PropertyValue<string>(watermark);
            return textbox;
        }
        public static T UseFloatingWatermark<T>(this T textbox, bool useFloatingWatermark) where T : IRxTextBox
        {
            textbox.UseFloatingWatermark = new PropertyValue<bool>(useFloatingWatermark);
            return textbox;
        }
        public static T NewLine<T>(this T textbox, string newLine) where T : IRxTextBox
        {
            textbox.NewLine = new PropertyValue<string>(newLine);
            return textbox;
        }
        public static T InnerLeftContent<T>(this T textbox, object innerLeftContent) where T : IRxTextBox
        {
            textbox.InnerLeftContent = new PropertyValue<object>(innerLeftContent);
            return textbox;
        }
        public static T InnerRightContent<T>(this T textbox, object innerRightContent) where T : IRxTextBox
        {
            textbox.InnerRightContent = new PropertyValue<object>(innerRightContent);
            return textbox;
        }
        public static T RevealPassword<T>(this T textbox, bool revealPassword) where T : IRxTextBox
        {
            textbox.RevealPassword = new PropertyValue<bool>(revealPassword);
            return textbox;
        }
        public static T IsUndoEnabled<T>(this T textbox, bool isUndoEnabled) where T : IRxTextBox
        {
            textbox.IsUndoEnabled = new PropertyValue<bool>(isUndoEnabled);
            return textbox;
        }
        public static T UndoLimit<T>(this T textbox, int undoLimit) where T : IRxTextBox
        {
            textbox.UndoLimit = new PropertyValue<int>(undoLimit);
            return textbox;
        }
        public static T OnCopyingToClipboard<T>(this T textbox, Action copyingtoclipboardAction) where T : IRxTextBox
        {
            textbox.CopyingToClipboardAction = copyingtoclipboardAction;
            return textbox;
        }

        public static T OnCopyingToClipboard<T>(this T textbox, Action<RoutedEventArgs> copyingtoclipboardActionWithArgs) where T : IRxTextBox
        {
            textbox.CopyingToClipboardActionWithArgs = copyingtoclipboardActionWithArgs;
            return textbox;
        }
        public static T OnCuttingToClipboard<T>(this T textbox, Action cuttingtoclipboardAction) where T : IRxTextBox
        {
            textbox.CuttingToClipboardAction = cuttingtoclipboardAction;
            return textbox;
        }

        public static T OnCuttingToClipboard<T>(this T textbox, Action<RoutedEventArgs> cuttingtoclipboardActionWithArgs) where T : IRxTextBox
        {
            textbox.CuttingToClipboardActionWithArgs = cuttingtoclipboardActionWithArgs;
            return textbox;
        }
        public static T OnPastingFromClipboard<T>(this T textbox, Action pastingfromclipboardAction) where T : IRxTextBox
        {
            textbox.PastingFromClipboardAction = pastingfromclipboardAction;
            return textbox;
        }

        public static T OnPastingFromClipboard<T>(this T textbox, Action<RoutedEventArgs> pastingfromclipboardActionWithArgs) where T : IRxTextBox
        {
            textbox.PastingFromClipboardActionWithArgs = pastingfromclipboardActionWithArgs;
            return textbox;
        }
    }
}
