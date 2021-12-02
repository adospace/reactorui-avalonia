using Avalonia.Controls;
using Avalonia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.DemoApp
{
    public class TestRow
    {
        private readonly int _i;

        public TestRow(int i)
        {
            _i = i;
        }

        public string Column1 => $"Column1_Row{_i}";
        public string Column2 => $"Column2_Row{_i}";
        public string Column3 => $"Column3_Row{_i}";
        public string Column4 => $"Column4_Row{_i}";
        public string Column5 => $"Column5_Row{_i}";
    }

    public class DataGridComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxDataGrid()
                .Columns(new[] 
                {
                    new DataGridTextColumn()
                    {
                        Binding = new Binding("Column1"),
                    },
                    new DataGridTextColumn()
                    {
                        Binding = new Binding("Column2"),
                    },
                    new DataGridTextColumn()
                    {
                        Binding = new Binding("Column3"),
                    },
                })
                .Items(new[] { new TestRow(1), new TestRow(2) });
        }
    }
}
