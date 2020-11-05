using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI.DemoApp
{
    public class CounterState : IState
    {
        public int Count { get; set; } = 1;
    }

    public class CounterComponent : RxComponent<CounterState>
    {
        public override VisualNode Render()
            => new RxStackPanel()
                {
                    new RxTextBlock()
                        .Text(State.Count.ToString())
                        .FontSize(24)
                        .HCenter(),
                    new RxButton()
                        .Content("Click to increase counter")
                        .OnClick(() => SetState(_=> _.Count++))
                }
                .Spacing(10)
                .Orientation(Avalonia.Layout.Orientation.Vertical)
                .VCenter()
                .HCenter();
    }
}
