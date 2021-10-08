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
using Avalonia.Controls.Selection;
using Avalonia.Input.TextInput;

using AvaloniaReactorUI.Internals;
using System.Linq;

namespace AvaloniaReactorUI
{
    public partial interface IRxHeaderedItemsControl : IRxItemsControl
    {
        object Header { get; set; }
    }

    public partial class RxHeaderedItemsControl<T> : IEnumerable<VisualNode>
    {
        private readonly List<VisualNode> _contents = new();
        public RxHeaderedItemsControl(VisualNode content)
        {
            _contents.Add(content);
        }

        public void Add(VisualNode child)
        {
            if (child is VisualNode && _contents.Any())
                throw new InvalidOperationException("Header already set");

            _contents.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        public object Header { get; set; } = (object)HeaderedItemsControl.HeaderProperty.GetDefaultValue<T>();

        protected override void OnAddChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Header = childControl;

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, AvaloniaObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Header = null;

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
            Validate.EnsureNotNull(NativeControl);

            if (Header != null)
            {
                NativeControl.Header = Header;
            }
        }
    }

    public static partial class RxHeaderedItemsControlExtensions
    {
        public static T Header<T>(this T contentcontrol, object header) where T : IRxHeaderedItemsControl
        {
            contentcontrol.Header = header;
            return contentcontrol;
        }

    }

}
