using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.DemoApp
{
    public class TreeViewTestItem
    {
        public TreeViewTestItem(string name, params TreeViewTestItem[] children)
        {
            Name = name;
            Children = children;
        }
        public string Name { get; }
        public TreeViewTestItem[] Children { get; }
    }

    public class TreeViewComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxTreeView()
                .Items(new[]
                {
                    new TreeViewTestItem("Item1", new[]
                    {
                        new TreeViewTestItem("Item1 - Child1"),
                        new TreeViewTestItem("Item1 - Child2"),
                    }),
                    new TreeViewTestItem("Item2", new[]
                    {
                        new TreeViewTestItem("Item2 - Child1"),
                        new TreeViewTestItem("Item2 - Child2"),
                        new TreeViewTestItem("Item2 - Child3", new []
                        {
                            new TreeViewTestItem("Item2 - Child3 - SubChild1"),
                            new TreeViewTestItem("Item2 - Child3 - SubChild2"),
                        }),
                    })
                })
                .OnRenderTreeItem<RxTreeView, TreeViewTestItem>(RenderItem, _ => _.Children);
        }

        private VisualNode RenderItem(TreeViewTestItem item)
        {
            return new RxTextBlock().Text(item.Name);
        }
    }
}
