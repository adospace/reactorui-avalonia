using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI.DemoApp
{
    public class MainComponent : RxComponent
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
