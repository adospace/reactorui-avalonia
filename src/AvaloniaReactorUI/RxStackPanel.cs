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
    public partial interface IRxStackPanel : IRxPanel
    {
        PropertyValue<double>? Spacing { get; set; }
        PropertyValue<Orientation>? Orientation { get; set; }

    }

    public partial class RxStackPanel<T> : RxPanel<T>, IRxStackPanel where T : StackPanel, new()
    {
        public RxStackPanel()
        {

        }

        public RxStackPanel(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double>? IRxStackPanel.Spacing { get; set; }
        PropertyValue<Orientation>? IRxStackPanel.Orientation { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxStackPanel = (IRxStackPanel)this;
            NativeControl.Set(StackPanel.SpacingProperty, thisAsIRxStackPanel.Spacing);
            NativeControl.Set(StackPanel.OrientationProperty, thisAsIRxStackPanel.Orientation);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxStackPanel : RxStackPanel<StackPanel>
    {
        public RxStackPanel()
        {

        }

        public RxStackPanel(Action<StackPanel?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxStackPanelExtensions
    {
        public static T Spacing<T>(this T stackpanel, double spacing) where T : IRxStackPanel
        {
            stackpanel.Spacing = new PropertyValue<double>(spacing);
            return stackpanel;
        }
        public static T Orientation<T>(this T stackpanel, Orientation orientation) where T : IRxStackPanel
        {
            stackpanel.Orientation = new PropertyValue<Orientation>(orientation);
            return stackpanel;
        }
    }
}
