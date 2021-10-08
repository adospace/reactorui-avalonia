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
    public partial interface IRxDataGrid : IRxTemplatedControl
    {
        PropertyValue<bool>? CanUserReorderColumns { get; set; }
        PropertyValue<bool>? CanUserResizeColumns { get; set; }
        PropertyValue<bool>? CanUserSortColumns { get; set; }
        PropertyValue<double>? ColumnHeaderHeight { get; set; }
        PropertyValue<DataGridLength>? ColumnWidth { get; set; }
        PropertyValue<IBrush>? AlternatingRowBackground { get; set; }
        PropertyValue<int>? FrozenColumnCount { get; set; }
        PropertyValue<DataGridGridLinesVisibility>? GridLinesVisibility { get; set; }
        PropertyValue<DataGridHeadersVisibility>? HeadersVisibility { get; set; }
        PropertyValue<IBrush>? HorizontalGridLinesBrush { get; set; }
        PropertyValue<ScrollBarVisibility>? HorizontalScrollBarVisibility { get; set; }
        PropertyValue<bool>? IsReadOnly { get; set; }
        PropertyValue<bool>? AreRowGroupHeadersFrozen { get; set; }
        PropertyValue<double>? MaxColumnWidth { get; set; }
        PropertyValue<double>? MinColumnWidth { get; set; }
        PropertyValue<IBrush>? RowBackground { get; set; }
        PropertyValue<double>? RowHeight { get; set; }
        PropertyValue<double>? RowHeaderWidth { get; set; }
        PropertyValue<DataGridSelectionMode>? SelectionMode { get; set; }
        PropertyValue<IBrush>? VerticalGridLinesBrush { get; set; }
        PropertyValue<ScrollBarVisibility>? VerticalScrollBarVisibility { get; set; }
        PropertyValue<int>? SelectedIndex { get; set; }
        PropertyValue<object>? SelectedItem { get; set; }
        PropertyValue<DataGridClipboardCopyMode>? ClipboardCopyMode { get; set; }
        PropertyValue<bool>? AutoGenerateColumns { get; set; }
        PropertyValue<IEnumerable>? Items { get; set; }
        PropertyValue<bool>? AreRowDetailsFrozen { get; set; }
        PropertyValue<DataGridRowDetailsVisibilityMode>? RowDetailsVisibilityMode { get; set; }

        Action? SelectionChangedAction { get; set; }
        Action<SelectionChangedEventArgs>? SelectionChangedActionWithArgs { get; set; }
    }

    public partial class RxDataGrid<T> : RxTemplatedControl<T>, IRxDataGrid where T : DataGrid, new()
    {
        public RxDataGrid()
        {

        }

        public RxDataGrid(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IRxDataGrid.CanUserReorderColumns { get; set; }
        PropertyValue<bool>? IRxDataGrid.CanUserResizeColumns { get; set; }
        PropertyValue<bool>? IRxDataGrid.CanUserSortColumns { get; set; }
        PropertyValue<double>? IRxDataGrid.ColumnHeaderHeight { get; set; }
        PropertyValue<DataGridLength>? IRxDataGrid.ColumnWidth { get; set; }
        PropertyValue<IBrush>? IRxDataGrid.AlternatingRowBackground { get; set; }
        PropertyValue<int>? IRxDataGrid.FrozenColumnCount { get; set; }
        PropertyValue<DataGridGridLinesVisibility>? IRxDataGrid.GridLinesVisibility { get; set; }
        PropertyValue<DataGridHeadersVisibility>? IRxDataGrid.HeadersVisibility { get; set; }
        PropertyValue<IBrush>? IRxDataGrid.HorizontalGridLinesBrush { get; set; }
        PropertyValue<ScrollBarVisibility>? IRxDataGrid.HorizontalScrollBarVisibility { get; set; }
        PropertyValue<bool>? IRxDataGrid.IsReadOnly { get; set; }
        PropertyValue<bool>? IRxDataGrid.AreRowGroupHeadersFrozen { get; set; }
        PropertyValue<double>? IRxDataGrid.MaxColumnWidth { get; set; }
        PropertyValue<double>? IRxDataGrid.MinColumnWidth { get; set; }
        PropertyValue<IBrush>? IRxDataGrid.RowBackground { get; set; }
        PropertyValue<double>? IRxDataGrid.RowHeight { get; set; }
        PropertyValue<double>? IRxDataGrid.RowHeaderWidth { get; set; }
        PropertyValue<DataGridSelectionMode>? IRxDataGrid.SelectionMode { get; set; }
        PropertyValue<IBrush>? IRxDataGrid.VerticalGridLinesBrush { get; set; }
        PropertyValue<ScrollBarVisibility>? IRxDataGrid.VerticalScrollBarVisibility { get; set; }
        PropertyValue<int>? IRxDataGrid.SelectedIndex { get; set; }
        PropertyValue<object>? IRxDataGrid.SelectedItem { get; set; }
        PropertyValue<DataGridClipboardCopyMode>? IRxDataGrid.ClipboardCopyMode { get; set; }
        PropertyValue<bool>? IRxDataGrid.AutoGenerateColumns { get; set; }
        PropertyValue<IEnumerable>? IRxDataGrid.Items { get; set; }
        PropertyValue<bool>? IRxDataGrid.AreRowDetailsFrozen { get; set; }
        PropertyValue<DataGridRowDetailsVisibilityMode>? IRxDataGrid.RowDetailsVisibilityMode { get; set; }

        Action? IRxDataGrid.SelectionChangedAction { get; set; }
        Action<SelectionChangedEventArgs>? IRxDataGrid.SelectionChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIRxDataGrid = (IRxDataGrid)this;
            NativeControl.Set(DataGrid.CanUserReorderColumnsProperty, thisAsIRxDataGrid.CanUserReorderColumns);
            NativeControl.Set(DataGrid.CanUserResizeColumnsProperty, thisAsIRxDataGrid.CanUserResizeColumns);
            NativeControl.Set(DataGrid.CanUserSortColumnsProperty, thisAsIRxDataGrid.CanUserSortColumns);
            NativeControl.Set(DataGrid.ColumnHeaderHeightProperty, thisAsIRxDataGrid.ColumnHeaderHeight);
            NativeControl.Set(DataGrid.ColumnWidthProperty, thisAsIRxDataGrid.ColumnWidth);
            NativeControl.Set(DataGrid.AlternatingRowBackgroundProperty, thisAsIRxDataGrid.AlternatingRowBackground);
            NativeControl.Set(DataGrid.FrozenColumnCountProperty, thisAsIRxDataGrid.FrozenColumnCount);
            NativeControl.Set(DataGrid.GridLinesVisibilityProperty, thisAsIRxDataGrid.GridLinesVisibility);
            NativeControl.Set(DataGrid.HeadersVisibilityProperty, thisAsIRxDataGrid.HeadersVisibility);
            NativeControl.Set(DataGrid.HorizontalGridLinesBrushProperty, thisAsIRxDataGrid.HorizontalGridLinesBrush);
            NativeControl.Set(DataGrid.HorizontalScrollBarVisibilityProperty, thisAsIRxDataGrid.HorizontalScrollBarVisibility);
            NativeControl.Set(DataGrid.IsReadOnlyProperty, thisAsIRxDataGrid.IsReadOnly);
            NativeControl.Set(DataGrid.AreRowGroupHeadersFrozenProperty, thisAsIRxDataGrid.AreRowGroupHeadersFrozen);
            NativeControl.Set(DataGrid.MaxColumnWidthProperty, thisAsIRxDataGrid.MaxColumnWidth);
            NativeControl.Set(DataGrid.MinColumnWidthProperty, thisAsIRxDataGrid.MinColumnWidth);
            NativeControl.Set(DataGrid.RowBackgroundProperty, thisAsIRxDataGrid.RowBackground);
            NativeControl.Set(DataGrid.RowHeightProperty, thisAsIRxDataGrid.RowHeight);
            NativeControl.Set(DataGrid.RowHeaderWidthProperty, thisAsIRxDataGrid.RowHeaderWidth);
            NativeControl.Set(DataGrid.SelectionModeProperty, thisAsIRxDataGrid.SelectionMode);
            NativeControl.Set(DataGrid.VerticalGridLinesBrushProperty, thisAsIRxDataGrid.VerticalGridLinesBrush);
            NativeControl.Set(DataGrid.VerticalScrollBarVisibilityProperty, thisAsIRxDataGrid.VerticalScrollBarVisibility);
            NativeControl.Set(DataGrid.SelectedIndexProperty, thisAsIRxDataGrid.SelectedIndex);
            NativeControl.Set(DataGrid.SelectedItemProperty, thisAsIRxDataGrid.SelectedItem);
            NativeControl.Set(DataGrid.ClipboardCopyModeProperty, thisAsIRxDataGrid.ClipboardCopyMode);
            NativeControl.Set(DataGrid.AutoGenerateColumnsProperty, thisAsIRxDataGrid.AutoGenerateColumns);
            NativeControl.Set(DataGrid.ItemsProperty, thisAsIRxDataGrid.Items);
            NativeControl.Set(DataGrid.AreRowDetailsFrozenProperty, thisAsIRxDataGrid.AreRowDetailsFrozen);
            NativeControl.Set(DataGrid.RowDetailsVisibilityModeProperty, thisAsIRxDataGrid.RowDetailsVisibilityMode);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxDataGrid = (IRxDataGrid)this;
            if (thisAsIRxDataGrid.SelectionChangedAction != null || thisAsIRxDataGrid.SelectionChangedActionWithArgs != null)
            {
                NativeControl.SelectionChanged += NativeControl_SelectionChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var thisAsIRxDataGrid = (IRxDataGrid)this;
            thisAsIRxDataGrid.SelectionChangedAction?.Invoke();
            thisAsIRxDataGrid.SelectionChangedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
            }

            base.OnDetachNativeEvents();
        }
    }
    public partial class RxDataGrid : RxDataGrid<DataGrid>
    {
        public RxDataGrid()
        {

        }

        public RxDataGrid(Action<DataGrid?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxDataGridExtensions
    {
        public static T CanUserReorderColumns<T>(this T datagrid, bool canUserReorderColumns) where T : IRxDataGrid
        {
            datagrid.CanUserReorderColumns = new PropertyValue<bool>(canUserReorderColumns);
            return datagrid;
        }
        public static T CanUserResizeColumns<T>(this T datagrid, bool canUserResizeColumns) where T : IRxDataGrid
        {
            datagrid.CanUserResizeColumns = new PropertyValue<bool>(canUserResizeColumns);
            return datagrid;
        }
        public static T CanUserSortColumns<T>(this T datagrid, bool canUserSortColumns) where T : IRxDataGrid
        {
            datagrid.CanUserSortColumns = new PropertyValue<bool>(canUserSortColumns);
            return datagrid;
        }
        public static T ColumnHeaderHeight<T>(this T datagrid, double columnHeaderHeight) where T : IRxDataGrid
        {
            datagrid.ColumnHeaderHeight = new PropertyValue<double>(columnHeaderHeight);
            return datagrid;
        }
        public static T ColumnWidth<T>(this T datagrid, DataGridLength columnWidth) where T : IRxDataGrid
        {
            datagrid.ColumnWidth = new PropertyValue<DataGridLength>(columnWidth);
            return datagrid;
        }
        public static T AlternatingRowBackground<T>(this T datagrid, IBrush alternatingRowBackground) where T : IRxDataGrid
        {
            datagrid.AlternatingRowBackground = new PropertyValue<IBrush>(alternatingRowBackground);
            return datagrid;
        }
        public static T FrozenColumnCount<T>(this T datagrid, int frozenColumnCount) where T : IRxDataGrid
        {
            datagrid.FrozenColumnCount = new PropertyValue<int>(frozenColumnCount);
            return datagrid;
        }
        public static T GridLinesVisibility<T>(this T datagrid, DataGridGridLinesVisibility gridLinesVisibility) where T : IRxDataGrid
        {
            datagrid.GridLinesVisibility = new PropertyValue<DataGridGridLinesVisibility>(gridLinesVisibility);
            return datagrid;
        }
        public static T HeadersVisibility<T>(this T datagrid, DataGridHeadersVisibility headersVisibility) where T : IRxDataGrid
        {
            datagrid.HeadersVisibility = new PropertyValue<DataGridHeadersVisibility>(headersVisibility);
            return datagrid;
        }
        public static T HorizontalGridLinesBrush<T>(this T datagrid, IBrush horizontalGridLinesBrush) where T : IRxDataGrid
        {
            datagrid.HorizontalGridLinesBrush = new PropertyValue<IBrush>(horizontalGridLinesBrush);
            return datagrid;
        }
        public static T HorizontalScrollBarVisibility<T>(this T datagrid, ScrollBarVisibility horizontalScrollBarVisibility) where T : IRxDataGrid
        {
            datagrid.HorizontalScrollBarVisibility = new PropertyValue<ScrollBarVisibility>(horizontalScrollBarVisibility);
            return datagrid;
        }
        public static T IsReadOnly<T>(this T datagrid, bool isReadOnly) where T : IRxDataGrid
        {
            datagrid.IsReadOnly = new PropertyValue<bool>(isReadOnly);
            return datagrid;
        }
        public static T AreRowGroupHeadersFrozen<T>(this T datagrid, bool areRowGroupHeadersFrozen) where T : IRxDataGrid
        {
            datagrid.AreRowGroupHeadersFrozen = new PropertyValue<bool>(areRowGroupHeadersFrozen);
            return datagrid;
        }
        public static T MaxColumnWidth<T>(this T datagrid, double maxColumnWidth) where T : IRxDataGrid
        {
            datagrid.MaxColumnWidth = new PropertyValue<double>(maxColumnWidth);
            return datagrid;
        }
        public static T MinColumnWidth<T>(this T datagrid, double minColumnWidth) where T : IRxDataGrid
        {
            datagrid.MinColumnWidth = new PropertyValue<double>(minColumnWidth);
            return datagrid;
        }
        public static T RowBackground<T>(this T datagrid, IBrush rowBackground) where T : IRxDataGrid
        {
            datagrid.RowBackground = new PropertyValue<IBrush>(rowBackground);
            return datagrid;
        }
        public static T RowHeight<T>(this T datagrid, double rowHeight) where T : IRxDataGrid
        {
            datagrid.RowHeight = new PropertyValue<double>(rowHeight);
            return datagrid;
        }
        public static T RowHeaderWidth<T>(this T datagrid, double rowHeaderWidth) where T : IRxDataGrid
        {
            datagrid.RowHeaderWidth = new PropertyValue<double>(rowHeaderWidth);
            return datagrid;
        }
        public static T SelectionMode<T>(this T datagrid, DataGridSelectionMode selectionMode) where T : IRxDataGrid
        {
            datagrid.SelectionMode = new PropertyValue<DataGridSelectionMode>(selectionMode);
            return datagrid;
        }
        public static T VerticalGridLinesBrush<T>(this T datagrid, IBrush verticalGridLinesBrush) where T : IRxDataGrid
        {
            datagrid.VerticalGridLinesBrush = new PropertyValue<IBrush>(verticalGridLinesBrush);
            return datagrid;
        }
        public static T VerticalScrollBarVisibility<T>(this T datagrid, ScrollBarVisibility verticalScrollBarVisibility) where T : IRxDataGrid
        {
            datagrid.VerticalScrollBarVisibility = new PropertyValue<ScrollBarVisibility>(verticalScrollBarVisibility);
            return datagrid;
        }
        public static T SelectedIndex<T>(this T datagrid, int selectedIndex) where T : IRxDataGrid
        {
            datagrid.SelectedIndex = new PropertyValue<int>(selectedIndex);
            return datagrid;
        }
        public static T SelectedItem<T>(this T datagrid, object selectedItem) where T : IRxDataGrid
        {
            datagrid.SelectedItem = new PropertyValue<object>(selectedItem);
            return datagrid;
        }
        public static T ClipboardCopyMode<T>(this T datagrid, DataGridClipboardCopyMode clipboardCopyMode) where T : IRxDataGrid
        {
            datagrid.ClipboardCopyMode = new PropertyValue<DataGridClipboardCopyMode>(clipboardCopyMode);
            return datagrid;
        }
        public static T AutoGenerateColumns<T>(this T datagrid, bool autoGenerateColumns) where T : IRxDataGrid
        {
            datagrid.AutoGenerateColumns = new PropertyValue<bool>(autoGenerateColumns);
            return datagrid;
        }
        public static T Items<T>(this T datagrid, IEnumerable items) where T : IRxDataGrid
        {
            datagrid.Items = new PropertyValue<IEnumerable>(items);
            return datagrid;
        }
        public static T AreRowDetailsFrozen<T>(this T datagrid, bool areRowDetailsFrozen) where T : IRxDataGrid
        {
            datagrid.AreRowDetailsFrozen = new PropertyValue<bool>(areRowDetailsFrozen);
            return datagrid;
        }
        public static T RowDetailsVisibilityMode<T>(this T datagrid, DataGridRowDetailsVisibilityMode rowDetailsVisibilityMode) where T : IRxDataGrid
        {
            datagrid.RowDetailsVisibilityMode = new PropertyValue<DataGridRowDetailsVisibilityMode>(rowDetailsVisibilityMode);
            return datagrid;
        }
        public static T OnSelectionChanged<T>(this T datagrid, Action selectionchangedAction) where T : IRxDataGrid
        {
            datagrid.SelectionChangedAction = selectionchangedAction;
            return datagrid;
        }

        public static T OnSelectionChanged<T>(this T datagrid, Action<SelectionChangedEventArgs> selectionchangedActionWithArgs) where T : IRxDataGrid
        {
            datagrid.SelectionChangedActionWithArgs = selectionchangedActionWithArgs;
            return datagrid;
        }
    }
}
