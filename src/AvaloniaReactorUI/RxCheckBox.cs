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
    public partial interface IRxCheckBox : IRxToggleButton
    {

    }

    public partial class RxCheckBox<T> : RxToggleButton<T>, IRxCheckBox where T : CheckBox, new()
    {
        public RxCheckBox()
        {

        }

        public RxCheckBox(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }



        protected override void OnUpdate()
        {
            OnBeginUpdate();

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxCheckBox : RxCheckBox<CheckBox>
    {
        public RxCheckBox()
        {

        }

        public RxCheckBox(Action<CheckBox?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxCheckBoxExtensions
    {
    }
}
