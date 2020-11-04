using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using System.Linq;

using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Platform;
using Avalonia.Controls.Selection;

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public partial interface IRxControl : IRxInputElement
    {
        SortedList<Type, IDataTemplate> DataTemplates {get; set;}
    }

    public partial class RxControl<T> : RxInputElement<T>, IRxControl where T : Control, new()
    {
        SortedList<Type, IDataTemplate> IRxControl.DataTemplates { get; set; } = new SortedList<Type, IDataTemplate>();

        partial void OnBeginUpdate()
        {
            var thisAsIRxContentControl = (IRxControl)this;
            //TODO: somenthing smarter than replace all
            NativeControl.DataTemplates.Clear();
            foreach(var dt in thisAsIRxContentControl.DataTemplates)
                NativeControl.DataTemplates.Add(dt.Value);
        }

    }
    public partial class RxControl : RxControl<Control>
    {

    }
    public static partial class RxControlExtensions
    {
        public static T OnRenderType<T, I>(this T itemscontrol, Func<I, VisualNode> renderFunc) where T : IRxControl
        {
            itemscontrol.DataTemplates.Add(typeof(T), 
                new FuncDataTemplate<I>((item, ns) =>
                {
                    VisualNode newRoot = renderFunc(item);
                    var itemTemplateNode = new ItemTemplateNode(newRoot);
                    itemTemplateNode.Layout();
                    return itemTemplateNode.RootControl;
                }));

            return itemscontrol;
        }
    }
}