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
    public partial interface IRxGrid : IRxPanel
    {
        PropertyValue<bool> ShowGridLines { get; set; }

    }

    public partial class RxGrid<T> : RxPanel<T>, IRxGrid where T : Grid, new()
    {
        public RxGrid()
        {

        }

        public RxGrid(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool> IRxGrid.ShowGridLines { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            var thisAsIRxGrid = (IRxGrid)this;
            NativeControl.Set(Grid.ShowGridLinesProperty, thisAsIRxGrid.ShowGridLines);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            var thisAsIRxGrid = (IRxGrid)this;

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
    public partial class RxGrid : RxGrid<Grid>
    {
        public RxGrid()
        {

        }

        public RxGrid(Action<Grid> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxGridExtensions
    {
        public static T ShowGridLines<T>(this T grid, bool showGridLines) where T : IRxGrid
        {
            grid.ShowGridLines = new PropertyValue<bool>(showGridLines);
            return grid;
        }
    }
}
