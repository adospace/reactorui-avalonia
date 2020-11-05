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
            new RxListBox()
                .Items(new[] { new Item("Item1"), new Item("Item2"), new Item("Item3"), new Item("Item4") })
                .OnRenderItem<RxListBox, Item>(_ => new RxTextBlock().Text(_.Name).Foreground(Brushes.Green))
                .FontSize(36)
                .VCenter()
                .HCenter();
    }
}
