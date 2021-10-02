using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.DemoApp
{
    public class TextBoxComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxTextBox()
                .MinWidth(200)
                .HCenter()
                .VCenter();
        }
    }
}
