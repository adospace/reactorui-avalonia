using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.DemoApp
{
    public class MenuComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxMenu
            {
                new RxMenuItem("Menu1")
                {
                    new RxCheckBox(),
                    new RxMenuItem("Menu1-Child1")
                    {
                        new RxMenuItem("Menu1-Child1-Child1")
                    }
                },
                new RxMenuItem("Menu2")
                {
                    new RxMenuItem("Menu2-Child1")
                    {
                        new RxMenuItem("Menu2-Child2-Child1"),
                        new RxMenuItem("Menu2-Child2-Child2"),
                        new RxMenuItem("Menu2-Child2-Child3"),
                    },
                    new RxMenuItem("Menu2-Child2"),
                    new RxMenuItem("Menu2-Child3"),
                }
            };
        }
    }
}
