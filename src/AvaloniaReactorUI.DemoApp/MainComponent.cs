﻿using Avalonia.Media;
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
        public override VisualNode Render() =>
            new RxWindow()
            {
                new RxTextBlock()
                    .Text("Avalonia + ReactorUI = Love!")
                    .FontSize(24)
                    .VCenter()
                    .HCenter()
            }
            .Title("AvaloniaReactorUI Demo App");
    }


}
