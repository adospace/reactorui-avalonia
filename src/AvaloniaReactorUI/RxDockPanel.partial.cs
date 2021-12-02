using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI
{
    public static partial class RxDockPanelExtensions
    {
        public static T DockLeft<T>(this T element) where T : VisualNode
        {
            element.SetAttachedProperty(DockPanel.DockProperty, Dock.Left);
            return element;
        }

        public static T DockTop<T>(this T element) where T : VisualNode
        {
            element.SetAttachedProperty(DockPanel.DockProperty, Dock.Top);
            return element;
        }

        public static T DockRight<T>(this T element) where T : VisualNode
        {
            element.SetAttachedProperty(DockPanel.DockProperty, Dock.Right);
            return element;
        }

        public static T DockBottom<T>(this T element) where T : VisualNode
        {
            element.SetAttachedProperty(DockPanel.DockProperty, Dock.Bottom);
            return element;
        }
    }
}
