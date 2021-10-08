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
    public partial interface IRxComboBox : IRxSelectingItemsControl
    {
        PropertyValue<bool>? IsDropDownOpen { get; set; }
        PropertyValue<double>? MaxDropDownHeight { get; set; }
        PropertyValue<ItemVirtualizationMode>? VirtualizationMode { get; set; }
        PropertyValue<string>? PlaceholderText { get; set; }
        PropertyValue<IBrush>? PlaceholderForeground { get; set; }
        PropertyValue<HorizontalAlignment>? HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? VerticalContentAlignment { get; set; }

    }

    public partial class RxComboBox<T> : RxSelectingItemsControl<T>, IRxComboBox where T : ComboBox, new()
    {
        public RxComboBox()
        {

        }

        public RxComboBox(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxComboBox.IsDropDownOpen { get; set; }
        PropertyValue<double>? IRxComboBox.MaxDropDownHeight { get; set; }
        PropertyValue<ItemVirtualizationMode>? IRxComboBox.VirtualizationMode { get; set; }
        PropertyValue<string>? IRxComboBox.PlaceholderText { get; set; }
        PropertyValue<IBrush>? IRxComboBox.PlaceholderForeground { get; set; }
        PropertyValue<HorizontalAlignment>? IRxComboBox.HorizontalContentAlignment { get; set; }
        PropertyValue<VerticalAlignment>? IRxComboBox.VerticalContentAlignment { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxComboBox = (IRxComboBox)this;
            NativeControl.Set(ComboBox.IsDropDownOpenProperty, thisAsIRxComboBox.IsDropDownOpen);
            NativeControl.Set(ComboBox.MaxDropDownHeightProperty, thisAsIRxComboBox.MaxDropDownHeight);
            NativeControl.Set(ComboBox.VirtualizationModeProperty, thisAsIRxComboBox.VirtualizationMode);
            NativeControl.Set(ComboBox.PlaceholderTextProperty, thisAsIRxComboBox.PlaceholderText);
            NativeControl.Set(ComboBox.PlaceholderForegroundProperty, thisAsIRxComboBox.PlaceholderForeground);
            NativeControl.Set(ComboBox.HorizontalContentAlignmentProperty, thisAsIRxComboBox.HorizontalContentAlignment);
            NativeControl.Set(ComboBox.VerticalContentAlignmentProperty, thisAsIRxComboBox.VerticalContentAlignment);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxComboBox : RxComboBox<ComboBox>
    {
        public RxComboBox()
        {

        }

        public RxComboBox(Action<ComboBox?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxComboBoxExtensions
    {
        public static T IsDropDownOpen<T>(this T combobox, bool isDropDownOpen) where T : IRxComboBox
        {
            combobox.IsDropDownOpen = new PropertyValue<bool>(isDropDownOpen);
            return combobox;
        }
        public static T MaxDropDownHeight<T>(this T combobox, double maxDropDownHeight) where T : IRxComboBox
        {
            combobox.MaxDropDownHeight = new PropertyValue<double>(maxDropDownHeight);
            return combobox;
        }
        public static T VirtualizationMode<T>(this T combobox, ItemVirtualizationMode virtualizationMode) where T : IRxComboBox
        {
            combobox.VirtualizationMode = new PropertyValue<ItemVirtualizationMode>(virtualizationMode);
            return combobox;
        }
        public static T PlaceholderText<T>(this T combobox, string placeholderText) where T : IRxComboBox
        {
            combobox.PlaceholderText = new PropertyValue<string>(placeholderText);
            return combobox;
        }
        public static T PlaceholderForeground<T>(this T combobox, IBrush placeholderForeground) where T : IRxComboBox
        {
            combobox.PlaceholderForeground = new PropertyValue<IBrush>(placeholderForeground);
            return combobox;
        }
        public static T HorizontalContentAlignment<T>(this T combobox, HorizontalAlignment horizontalContentAlignment) where T : IRxComboBox
        {
            combobox.HorizontalContentAlignment = new PropertyValue<HorizontalAlignment>(horizontalContentAlignment);
            return combobox;
        }
        public static T VerticalContentAlignment<T>(this T combobox, VerticalAlignment verticalContentAlignment) where T : IRxComboBox
        {
            combobox.VerticalContentAlignment = new PropertyValue<VerticalAlignment>(verticalContentAlignment);
            return combobox;
        }
    }
}
