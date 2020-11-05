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
    }

    public class MainComponentState : IState
    {
        public Page CurrentPage { get; set; }
    }

    public class MainComponent : RxComponent<MainComponentState>
    {
        private VisualNode Menu()=> 
            new RxListBox()
                .Items(Enum.GetValues(typeof(Page)))
                .OnRenderItem<RxListBox, Page>(
                    _ => new RxTextBlock()
                            .Text(_.ToString())
                            .Height(35)
                            .VCenter()
                            .HCenter())
                .FontSize(20)
                .SelectedItem(State.CurrentPage)
                .OnSelectionChanged(args =>
                {
                    if (args.AddedItems.Count > 0)
                    {
                        var selectedPage = (Page)args.AddedItems[0];
                        SetState(s => s.CurrentPage = selectedPage);
                    }
                });

        private VisualNode CurrentPage()
        {
            switch (State.CurrentPage)
            {
                case Page.Home:
                    return new RxTextBlock()
                        .Text("Avalonia + ReactorUI = Love!")
                        .FontSize(24)
                        .VCenter()
                        .HCenter();
                case Page.Counter:
                    return new CounterComponent();
                case Page.Items:
                    return new ItemsControlComponent();
                case Page.Timer:
                    return new TimerComponent();
                default:
                    throw new NotSupportedException();
            }
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
