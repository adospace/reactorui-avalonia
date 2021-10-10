using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI
{
    public partial class RxTextBlock<T> : RxControl<T>, IRxTextBlock where T : TextBlock, new()
    {
        public RxTextBlock(string text)
        {
            ((IRxTextBlock)this).Text = new PropertyValue<string>(text);
        }
    }

    public partial class RxTextBlock : RxTextBlock<TextBlock>
    {
        public RxTextBlock(string text)
            :base(text)
        {

        }
    }

}
