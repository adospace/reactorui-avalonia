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
    public partial interface IRxToggleButton : IRxButton
    {
        PropertyValue<bool>? IsChecked { get; set; }
    }

    public partial class RxToggleButton<T> : RxButton<T>, IRxToggleButton where T : ToggleButton, new()
    {
        PropertyValue<bool>? IRxToggleButton.IsChecked { get; set; }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxToggleButton = (IRxToggleButton)this;
            //NativeControl.Set(ToggleButton.IsCheckedProperty, thisAsIRxToggleButton.IsChecked);
            NativeControl.IsChecked = thisAsIRxToggleButton.IsChecked?.Value;
        }
    }

    public static partial class RxToggleButtonExtensions
    {
        public static T IsChecked<T>(this T togglebutton, bool isChecked) where T : IRxToggleButton
        {
            togglebutton.IsChecked = new PropertyValue<bool>(isChecked);
            return togglebutton;
        }
    }
}
