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
    public partial interface IRxMenu : IRxMenuBase
    {

    }

    public partial class RxMenu<T> : RxMenuBase<T>, IRxMenu where T : Menu, new()
    {
        public RxMenu()
        {

        }

        public RxMenu(Action<T?> componentRefAction)
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
    public partial class RxMenu : RxMenu<Menu>
    {
        public RxMenu()
        {

        }

        public RxMenu(Action<Menu?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxMenuExtensions
    {
    }
}
