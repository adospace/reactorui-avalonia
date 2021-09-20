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
    public partial interface IRxPanel : IRxControl
    {
        PropertyValue<IBrush> Background { get; set; }

    }

    public partial class RxPanel<T> : RxControl<T>, IRxPanel where T : Panel, new()
    {
        public RxPanel()
        {

        }

        public RxPanel(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<IBrush> IRxPanel.Background { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxPanel = (IRxPanel)this;
            NativeControl.Set(Panel.BackgroundProperty, thisAsIRxPanel.Background);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxPanel = (IRxPanel)this;

            base.OnAttachNativeEvents();
        }


        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
            }

            base.OnDetachNativeEvents();
        }

    }
    public partial class RxPanel : RxPanel<Panel>
    {
        public RxPanel()
        {

        }

        public RxPanel(Action<Panel> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxPanelExtensions
    {
        public static T Background<T>(this T panel, IBrush background) where T : IRxPanel
        {
            panel.Background = new PropertyValue<IBrush>(background);
            return panel;
        }
    }
}
