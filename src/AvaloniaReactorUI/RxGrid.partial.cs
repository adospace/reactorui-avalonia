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
        ColumnDefinitions Columns { get; set; }

        RowDefinitions Rows { get; set; }
    }

    public partial class RxGrid<T> : RxPanel<T>, IRxGrid where T : Grid, new()
    {
        public RxGrid(string rows, string columns)
        {
            var thisAsIRxGrid = (IRxGrid)this;
            thisAsIRxGrid.Rows = new RowDefinitions(rows);
            thisAsIRxGrid.Columns = new ColumnDefinitions(columns);
        }

        public RxGrid(RowDefinitions rows, ColumnDefinitions columns)
        {
            var thisAsIRxGrid = (IRxGrid)this;
            thisAsIRxGrid.Rows = rows;
            thisAsIRxGrid.Columns = columns;
        }

        public RxGrid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
        {
            var thisAsIRxGrid = (IRxGrid)this;
            thisAsIRxGrid.Rows = new RowDefinitions();
            thisAsIRxGrid.Columns = new ColumnDefinitions();
            foreach (var row in rows)
                thisAsIRxGrid.Rows.Add(row);
            foreach (var column in columns)
                thisAsIRxGrid.Columns.Add(column);
        }

        ColumnDefinitions IRxGrid.Columns { get; set; } = new ColumnDefinitions();
        RowDefinitions IRxGrid.Rows { get; set; } = new RowDefinitions();

        partial void OnBeginUpdate()
        {
            var thisAsIRxGrid = (IRxGrid)this;
            NativeControl.RowDefinitions = thisAsIRxGrid.Rows;
            NativeControl.ColumnDefinitions = thisAsIRxGrid.Columns;            
        }
    }

    public partial class RxGrid : RxGrid<Grid>
    {

    }
    public static partial class RxGridExtensions
    {
        public static T Rows<T>(this T grid, string rows) where T : IRxGrid
        {
            grid.Rows = new RowDefinitions(rows);
            return grid;
        }

        public static T Columns<T>(this T grid, string columns) where T : IRxGrid
        {
            grid.Columns = new ColumnDefinitions(columns);
            return grid;
        }
    }
}
