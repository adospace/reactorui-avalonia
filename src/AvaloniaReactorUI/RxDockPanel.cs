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
    public partial interface IRxDockPanel : IRxPanel
    {
        PropertyValue<bool>? LastChildFill { get; set; }

    }

    public partial class RxDockPanel<T> : RxPanel<T>, IRxDockPanel where T : DockPanel, new()
    {
        public RxDockPanel()
        {

        }

        public RxDockPanel(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxDockPanel.LastChildFill { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxDockPanel = (IRxDockPanel)this;
            NativeControl.Set(DockPanel.LastChildFillProperty, thisAsIRxDockPanel.LastChildFill);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxDockPanel : RxDockPanel<DockPanel>
    {
        public RxDockPanel()
        {

        }

        public RxDockPanel(Action<DockPanel?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxDockPanelExtensions
    {
        public static T LastChildFill<T>(this T dockpanel, bool lastChildFill) where T : IRxDockPanel
        {
            dockpanel.LastChildFill = new PropertyValue<bool>(lastChildFill);
            return dockpanel;
        }
    }
}
