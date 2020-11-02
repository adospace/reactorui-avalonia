using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public partial interface IRxContentControl : IRxTemplatedControl
    {
        object Content { get; set; }
    }

    public partial class RxContentControl<T> : IEnumerable<VisualNode>
    {
        private readonly List<VisualNode> _contents = new List<VisualNode>();
        public RxContentControl(VisualNode content)
        {
            _contents.Add(content);
        }

        public void Add(VisualNode child)
        {
            if (child is VisualNode && _contents.Any())
                throw new InvalidOperationException("Content already set");

            _contents.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }
        
        public object Content { get; set; } = (object)ContentControl.ContentProperty.GetDefaultValue<T>();

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            NativeControl.Content = childControl;

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, AvaloniaObject childControl)
        {
            NativeControl.Content = null;

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        partial void OnBeginUpdate()
        {
            if (Content != null)
            {
                NativeControl.Content = Content;
            }
            //else if ()
            //{
            //    NativeControl.SetValue(ContentControl.ContentProperty, AvaloniaProperty.UnsetValue);
            //}
        }
    }

    public static partial class RxContentControlExtensions
    {
        public static T Content<T>(this T contentcontrol, object content) where T : IRxContentControl
        {
            contentcontrol.Content = content;
            return contentcontrol;
        }

    }
}
