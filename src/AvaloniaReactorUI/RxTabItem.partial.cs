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
    public partial class RxTabItem<T> : RxHeaderedContentControl<T>, IRxTabItem where T : TabItem, new()
    {
        public RxTabItem(object? header)
            :base(header)
        {
            
        }
    }
    public partial class RxTabItem : RxTabItem<TabItem>
    {
        public RxTabItem(object? header)
            :base(header)
        {
        }
    }
}
