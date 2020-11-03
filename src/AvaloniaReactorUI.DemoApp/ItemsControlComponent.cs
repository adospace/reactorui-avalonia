using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI.DemoApp
{
    public class Item
    {
        public Item(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
    public class ItemsControlComponent : RxComponent
    {
        public override VisualNode Render() =>
            new RxWindow()
            {
                new RxListBox()
                    .Items(new [] { new Item("Item1"), new Item("Item2"), new Item("Item3")})
                    .OnRenderItem<RxListBox, Item>(_ => new RxTextBlock().Text(_.Name))
                    .FontSize(24)
                    .VCenter()
                    .HCenter()
            }
            .Title("AvaloniaReactorUI Demo App");
    }
}
