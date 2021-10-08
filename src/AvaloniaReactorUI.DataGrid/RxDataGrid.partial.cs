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
        DataGridColumn[] Columns { get; set; }
    }

    public partial class RxDataGrid<T> : RxTemplatedControl<T>, IRxDataGrid where T : DataGrid, new()
    {
        DataGridColumn[] IRxDataGrid.Columns { get; set; }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIRxDataGrid = (IRxDataGrid)this;
            int iColumn = 0;
            while (true)
            {
                if (iColumn < thisAsIRxDataGrid.Columns.Length && iColumn < NativeControl.Columns.Count)
                {
                    if (thisAsIRxDataGrid.Columns[iColumn] != NativeControl.Columns[iColumn])
                    {
                        NativeControl.Columns.RemoveAt(iColumn);
                        NativeControl.Columns.Insert(iColumn, thisAsIRxDataGrid.Columns[iColumn]);
                    }

                    iColumn++;
                }
                else if (iColumn < thisAsIRxDataGrid.Columns.Length && iColumn >= NativeControl.Columns.Count)
                {
                    NativeControl.Columns.Add(thisAsIRxDataGrid.Columns[iColumn]);
                    iColumn++;
                }
                else if (iColumn >= thisAsIRxDataGrid.Columns.Length && iColumn < NativeControl.Columns.Count)
                {
                    NativeControl.Columns.RemoveAt(iColumn);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static partial class RxDataGridExtensions
    {
        public static T Columns<T>(this T datagrid, params DataGridColumn[] columns) where T : IRxDataGrid
        {
            datagrid.Columns = columns;
            return datagrid;
        }
    }
}
