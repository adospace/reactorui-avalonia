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
    public partial interface IRxControl : IRxInputElement
    {


    }

    public partial class RxControl<T> : RxInputElement<T>, IRxControl where T : Control, new()
    {



    }
    public partial class RxControl : RxControl<Control>
    {

    }
    public static partial class RxControlExtensions
    {

    }
}