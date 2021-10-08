using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.DemoApp
{
    public class CustomParameter
    {
        public int Numeric { get; set; }
    }

    public class ParameterParentComponent : RxComponent
    {
        private readonly IParameter<CustomParameter> _customParameter;

        public ParameterParentComponent()
        {
            _customParameter = CreateParameter<CustomParameter>();
        }

        public override VisualNode Render()
        {
            return new RxStackPanel
            {
                new RxButton()
                    .Content("Increment from parent")
                    .OnClick(()=> _customParameter.Set(_=>_.Numeric += 1   )),                
                new RxTextBlock()
                    .Text(_customParameter.Value.Numeric.ToString()!),

                new ParameterChildComponent()
            }
            .VCenter()
            .HCenter()
            .Spacing(10);
        }
    }

    partial class ParameterChildComponent : RxComponent
    {
        private IParameter<CustomParameter> _customParameter;

        public override VisualNode Render()
        {
            _customParameter = GetParameter<CustomParameter>();
            return new RxStackPanel
            {
                new RxButton()
                    .Content("Increment from child")
                    .OnClick(()=> _customParameter.Set(_=>_.Numeric++)),

                new RxTextBlock()
                    .Text(_customParameter.Value.Numeric.ToString()!),
            }
            .Spacing(10);
        }
    }
}
