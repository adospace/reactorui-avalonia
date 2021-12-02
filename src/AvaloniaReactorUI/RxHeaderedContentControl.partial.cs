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
    public partial interface IRxHeaderedContentControl : IRxContentControl
    {
    }

    public partial class RxHeaderedContentControl<T> : RxContentControl<T>, IRxHeaderedContentControl where T : HeaderedContentControl, new()
    {
        public RxHeaderedContentControl(object? header)
        {
            ((IRxHeaderedContentControl)this).Header = new PropertyValue<object?>(header);
        }
    }
    public partial class RxHeaderedContentControl : RxHeaderedContentControl<HeaderedContentControl>
    {
        public RxHeaderedContentControl(object? header)
            : base(header)
        {

        }
    }
}
