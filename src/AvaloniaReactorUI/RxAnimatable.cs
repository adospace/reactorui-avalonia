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
    public partial interface IRxAnimatable : IVisualNode
    {
        PropertyValue<IClock>? Clock { get; set; }
        PropertyValue<Transitions?>? Transitions { get; set; }

    }

    public partial class RxAnimatable<T> : VisualNode<T>, IRxAnimatable where T : Animatable, new()
    {
        public RxAnimatable()
        {

        }

        public RxAnimatable(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IClock>? IRxAnimatable.Clock { get; set; }
        PropertyValue<Transitions?>? IRxAnimatable.Transitions { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            OnBeginUpdate();

            var thisAsIRxAnimatable = (IRxAnimatable)this;
            NativeControl.Set(Animatable.ClockProperty, thisAsIRxAnimatable.Clock);
            NativeControl.SetNullable(Animatable.TransitionsProperty, thisAsIRxAnimatable.Transitions);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxAnimatable : RxAnimatable<Animatable>
    {
        public RxAnimatable()
        {

        }

        public RxAnimatable(Action<Animatable?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxAnimatableExtensions
    {
        public static T Clock<T>(this T animatable, IClock clock) where T : IRxAnimatable
        {
            animatable.Clock = new PropertyValue<IClock>(clock);
            return animatable;
        }
        public static T Transitions<T>(this T animatable, Transitions? transitions) where T : IRxAnimatable
        {
            animatable.Transitions = new PropertyValue<Transitions?>(transitions);
            return animatable;
        }
    }
}
