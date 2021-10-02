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

namespace AvaloniaReactorUI
{
    public partial interface IRxWindow : IRxWindowBase
    {
        PropertyValue<SizeToContent>? SizeToContent { get; set; }
        PropertyValue<bool>? ExtendClientAreaToDecorationsHint { get; set; }
        PropertyValue<ExtendClientAreaChromeHints>? ExtendClientAreaChromeHints { get; set; }
        PropertyValue<double>? ExtendClientAreaTitleBarHeightHint { get; set; }
        PropertyValue<SystemDecorations>? SystemDecorations { get; set; }
        PropertyValue<bool>? ShowActivated { get; set; }
        PropertyValue<bool>? ShowInTaskbar { get; set; }
        PropertyValue<WindowState>? WindowState { get; set; }
        PropertyValue<string>? Title { get; set; }
        PropertyValue<WindowIcon>? Icon { get; set; }
        PropertyValue<WindowStartupLocation>? WindowStartupLocation { get; set; }
        PropertyValue<bool>? CanResize { get; set; }

    }

    public partial class RxWindow<T> : RxWindowBase<T>, IRxWindow where T : Window, new()
    {
        public RxWindow()
        {

        }

        public RxWindow(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<SizeToContent>? IRxWindow.SizeToContent { get; set; }
        PropertyValue<bool>? IRxWindow.ExtendClientAreaToDecorationsHint { get; set; }
        PropertyValue<ExtendClientAreaChromeHints>? IRxWindow.ExtendClientAreaChromeHints { get; set; }
        PropertyValue<double>? IRxWindow.ExtendClientAreaTitleBarHeightHint { get; set; }
        PropertyValue<SystemDecorations>? IRxWindow.SystemDecorations { get; set; }
        PropertyValue<bool>? IRxWindow.ShowActivated { get; set; }
        PropertyValue<bool>? IRxWindow.ShowInTaskbar { get; set; }
        PropertyValue<WindowState>? IRxWindow.WindowState { get; set; }
        PropertyValue<string>? IRxWindow.Title { get; set; }
        PropertyValue<WindowIcon>? IRxWindow.Icon { get; set; }
        PropertyValue<WindowStartupLocation>? IRxWindow.WindowStartupLocation { get; set; }
        PropertyValue<bool>? IRxWindow.CanResize { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            OnBeginUpdate();

            var thisAsIRxWindow = (IRxWindow)this;
            NativeControl.Set(Window.SizeToContentProperty, thisAsIRxWindow.SizeToContent);
            NativeControl.Set(Window.ExtendClientAreaToDecorationsHintProperty, thisAsIRxWindow.ExtendClientAreaToDecorationsHint);
            NativeControl.Set(Window.ExtendClientAreaChromeHintsProperty, thisAsIRxWindow.ExtendClientAreaChromeHints);
            NativeControl.Set(Window.ExtendClientAreaTitleBarHeightHintProperty, thisAsIRxWindow.ExtendClientAreaTitleBarHeightHint);
            NativeControl.Set(Window.SystemDecorationsProperty, thisAsIRxWindow.SystemDecorations);
            NativeControl.Set(Window.ShowActivatedProperty, thisAsIRxWindow.ShowActivated);
            NativeControl.Set(Window.ShowInTaskbarProperty, thisAsIRxWindow.ShowInTaskbar);
            NativeControl.Set(Window.WindowStateProperty, thisAsIRxWindow.WindowState);
            NativeControl.Set(Window.TitleProperty, thisAsIRxWindow.Title);
            NativeControl.Set(Window.IconProperty, thisAsIRxWindow.Icon);
            NativeControl.Set(Window.WindowStartupLocationProperty, thisAsIRxWindow.WindowStartupLocation);
            NativeControl.Set(Window.CanResizeProperty, thisAsIRxWindow.CanResize);

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

    }
    public partial class RxWindow : RxWindow<Window>
    {
        public RxWindow()
        {

        }

        public RxWindow(Action<Window?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }
    public static partial class RxWindowExtensions
    {
        public static T SizeToContent<T>(this T window, SizeToContent sizeToContent) where T : IRxWindow
        {
            window.SizeToContent = new PropertyValue<SizeToContent>(sizeToContent);
            return window;
        }
        public static T ExtendClientAreaToDecorationsHint<T>(this T window, bool extendClientAreaToDecorationsHint) where T : IRxWindow
        {
            window.ExtendClientAreaToDecorationsHint = new PropertyValue<bool>(extendClientAreaToDecorationsHint);
            return window;
        }
        public static T ExtendClientAreaChromeHints<T>(this T window, ExtendClientAreaChromeHints extendClientAreaChromeHints) where T : IRxWindow
        {
            window.ExtendClientAreaChromeHints = new PropertyValue<ExtendClientAreaChromeHints>(extendClientAreaChromeHints);
            return window;
        }
        public static T ExtendClientAreaTitleBarHeightHint<T>(this T window, double extendClientAreaTitleBarHeightHint) where T : IRxWindow
        {
            window.ExtendClientAreaTitleBarHeightHint = new PropertyValue<double>(extendClientAreaTitleBarHeightHint);
            return window;
        }
        public static T SystemDecorations<T>(this T window, SystemDecorations systemDecorations) where T : IRxWindow
        {
            window.SystemDecorations = new PropertyValue<SystemDecorations>(systemDecorations);
            return window;
        }
        public static T ShowActivated<T>(this T window, bool showActivated) where T : IRxWindow
        {
            window.ShowActivated = new PropertyValue<bool>(showActivated);
            return window;
        }
        public static T ShowInTaskbar<T>(this T window, bool showInTaskbar) where T : IRxWindow
        {
            window.ShowInTaskbar = new PropertyValue<bool>(showInTaskbar);
            return window;
        }
        public static T WindowState<T>(this T window, WindowState windowState) where T : IRxWindow
        {
            window.WindowState = new PropertyValue<WindowState>(windowState);
            return window;
        }
        public static T Title<T>(this T window, string title) where T : IRxWindow
        {
            window.Title = new PropertyValue<string>(title);
            return window;
        }
        public static T Icon<T>(this T window, WindowIcon icon) where T : IRxWindow
        {
            window.Icon = new PropertyValue<WindowIcon>(icon);
            return window;
        }
        public static T WindowStartupLocation<T>(this T window, WindowStartupLocation windowStartupLocation) where T : IRxWindow
        {
            window.WindowStartupLocation = new PropertyValue<WindowStartupLocation>(windowStartupLocation);
            return window;
        }
        public static T CanResize<T>(this T window, bool canResize) where T : IRxWindow
        {
            window.CanResize = new PropertyValue<bool>(canResize);
            return window;
        }
    }
}
