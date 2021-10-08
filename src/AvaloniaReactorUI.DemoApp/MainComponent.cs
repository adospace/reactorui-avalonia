using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI.DemoApp
{
    public enum Page
    {
        Home,
        Counter,
        Timer,
        Items,
        TextBox,
        Parameters,
        TreeView
    }

    public class MainComponentState : IState
    {
        public Page CurrentPage { get; set; }
    }

    public class MainComponent : RxComponent<MainComponentState>
    {
        private static readonly Page[] _pages = new[] { Page.Home, Page.Counter, Page.Timer, Page.Items, Page.TextBox, Page.Parameters, Page.TreeView };

        private VisualNode Menu()=> 
            new RxListBox()
                .Items(_pages)
                .OnRenderItem<RxListBox, Page>(
                    _ => new RxTextBlock()
                            .Text(_.ToString()))
                .FontSize(20)
                .SelectedItem(State.CurrentPage)
                .OnSelectionChanged(args =>
                {
                    if (args.AddedItems.Count > 0)
                    {
                        var selectedPage = (Page)args.AddedItems[0]!;
                        SetState(s => s.CurrentPage = selectedPage);
                    }
                });

        private VisualNode CurrentPage()
        {
            return State.CurrentPage switch
            {
                Page.Home => new RxTextBlock()
                    .Text("Avalonia + ReactorUI = Love!")
                    .FontSize(24)
                    .VCenter()
                    .HCenter(),
                Page.Counter => new CounterComponent(),
                Page.Items => new ItemsControlComponent(),
                Page.Timer => new TimerComponent(),
                Page.TextBox => new TextBoxComponent(),
                Page.Parameters => new ParameterParentComponent(),
                Page.TreeView => new TreeViewComponent(),
                _ => throw new NotSupportedException(),
            };
        }

        public override VisualNode Render() =>
            new RxWindow()
            {
                new RxGrid()
                {
                    Menu(),
                    CurrentPage()
                        .GridColumn(1)
                }
                .Columns("200 *")
            }
            .Title("AvaloniaReactorUI Demo App");
    }


}
