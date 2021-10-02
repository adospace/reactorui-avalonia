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
    public partial interface IRxStyledElement : IRxAnimatable
    {
        PropertyValue<object?>? DataContext { get; set; }

    }

    public partial class RxStyledElement<T> : RxAnimatable<T>, IRxStyledElement where T : StyledElement, new()
    {
        public RxStyledElement()
        {

        }

        public RxStyledElement(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<object?>? IRxStyledElement.DataContext { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxStyledElement = (IRxStyledElement)this;
            NativeControl.SetNullable(StyledElement.DataContextProperty, thisAsIRxStyledElement.DataContext);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxStyledElement : RxStyledElement<StyledElement>
    {
        public RxStyledElement()
        {

        }

        public RxStyledElement(Action<StyledElement?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxStyledElementExtensions
    {
        public static T DataContext<T>(this T styledelement, object? dataContext) where T : IRxStyledElement
        {
            styledelement.DataContext = new PropertyValue<object?>(dataContext);
            return styledelement;
        }
    }
}
