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
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxGrid = (IRxGrid)this;
            if (NativeControl.RowDefinitions.ToString() != thisAsIRxGrid.Rows.ToString())
            {
                NativeControl.RowDefinitions = thisAsIRxGrid.Rows;
            }

            if (NativeControl.ColumnDefinitions.ToString() != thisAsIRxGrid.Columns.ToString())
            {
                NativeControl.ColumnDefinitions = thisAsIRxGrid.Columns;
            }
        }
    }

    public partial class RxGrid : RxGrid<Grid>
    {
        public RxGrid(string rows, string columns)
            :base(rows, columns)
        {
        }

        public RxGrid(RowDefinitions rows, ColumnDefinitions columns)
            :base(rows, columns)
        {
        }

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

        public static T ColumnDefinition<T>(this T grid) where T : IRxGrid
        {
            grid.Columns.Add(new ColumnDefinition());
            return grid;
        }

        public static T ColumnDefinition<T>(this T grid, double width) where T : IRxGrid
        {
            grid.Columns.Add(new ColumnDefinition() { Width = new GridLength(width) });
            return grid;
        }

        public static T ColumnDefinitionAuto<T>(this T grid) where T : IRxGrid
        {
            grid.Columns.Add(new ColumnDefinition() { Width = GridLength.Auto });
            return grid;
        }

        public static T ColumnDefinitionStar<T>(this T grid, double starValue) where T : IRxGrid
        {
            grid.Columns.Add(new ColumnDefinition() { Width = new GridLength(starValue, GridUnitType.Star) });
            return grid;
        }

        public static T ColumnDefinition<T>(this T grid, GridLength width) where T : IRxGrid
        {
            grid.Columns.Add(new ColumnDefinition() { Width = width });
            return grid;
        }

        public static T RowDefinition<T>(this T grid) where T : IRxGrid
        {
            grid.Rows.Add(new RowDefinition());
            return grid;
        }

        public static T RowDefinition<T>(this T grid, double height) where T : IRxGrid
        {
            grid.Rows.Add(new RowDefinition() { Height = new GridLength(height) });
            return grid;
        }

        public static T RowDefinitionAuto<T>(this T grid) where T : IRxGrid
        {
            grid.Rows.Add(new RowDefinition() { Height = GridLength.Auto });
            return grid;
        }

        public static T RowDefinitionStar<T>(this T grid, double starValue) where T : IRxGrid
        {
            grid.Rows.Add(new RowDefinition() { Height = new GridLength(starValue, GridUnitType.Star) });
            return grid;
        }

        public static T RowDefinition<T>(this T grid, GridLength width) where T : IRxGrid
        {
            grid.Rows.Add(new RowDefinition() { Height = width });
            return grid;
        }

        public static T GridRow<T>(this T element, int rowIndex) where T : VisualNode
        {
            element.SetAttachedProperty(Grid.RowProperty, rowIndex);
            return element;            
        }

        public static T GridRowSpan<T>(this T element, int rowSpan) where T : VisualNode
        {
            element.SetAttachedProperty(Grid.RowSpanProperty, rowSpan);
            return element;
        }

        public static T GridColumn<T>(this T element, int columnIndex) where T : VisualNode
        {
            element.SetAttachedProperty(Grid.ColumnProperty, columnIndex);
            return element;
        }

        public static T GridColumnSpan<T>(this T element, int columnSpan) where T : VisualNode
        {
            element.SetAttachedProperty(Grid.ColumnSpanProperty, columnSpan);
            return element;
        }
    }
}
