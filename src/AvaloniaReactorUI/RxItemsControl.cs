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
    public partial interface IRxItemsControl : IRxTemplatedControl
    {

    }

    public partial class RxItemsControl<T> : RxTemplatedControl<T>, IRxItemsControl where T : ItemsControl, new()
    {
        public RxItemsControl()
        {

        }

        public RxItemsControl(Action<T?> componentRefAction)
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
    public partial class RxItemsControl : RxItemsControl<ItemsControl>
    {
        public RxItemsControl()
        {

        }

        public RxItemsControl(Action<ItemsControl?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxItemsControlExtensions
    {
    }
}
