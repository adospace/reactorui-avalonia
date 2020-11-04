using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;

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

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public partial interface IRxItemsControl
    {
        PropertyValue<IDataTemplate> DataTemplate { get; set; }
    }

    public partial class RxItemsControl<T>
    {
        PropertyValue<IDataTemplate> IRxItemsControl.DataTemplate { get; set; }

        partial void OnBeginUpdate()
        {
            var thisAsIRxLayoutable = (IRxItemsControl)this;
            NativeControl.Set(ItemsControl.ItemTemplateProperty, thisAsIRxLayoutable.DataTemplate);
            //WARNING: changing ItemTemplate after the first time (i.e. when the listbox ItemContainerGenerator is already created)
            //doesn't re-create the item containers (see Avalonia source ItemsControl.cs:452)
            //Somenthing that will be fixed in coming releases
        }
    }

    public interface IItemTemplateFunction
    {
        VisualNode Render(object item);
    }

    internal class ItemTemplateNode : VisualNode
    {
        public ItemTemplateNode(VisualNode root)
        {
            Root = root;
        }

        private VisualNode _root;

        public VisualNode Root
        {
            get => _root;
            set
            {
                if (_root != value)
                {
                    _root = value;
                    Invalidate();
                }
            }
        }

        public IControl RootControl {get; private set;}

        protected sealed override void OnAddChild(VisualNode widget, AvaloniaObject nativeControl)
        {
            if (nativeControl is IControl view)
                RootControl = view;
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, AvaloniaObject nativeControl)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Root;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }

    public static partial class RxItemsControlExtensions
    {
        public static T OnRenderItem<T, I>(this T itemscontrol, Func<I, VisualNode> renderFunc) where T : IRxItemsControl
        {
            itemscontrol.DataTemplate = new PropertyValue<IDataTemplate>(
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