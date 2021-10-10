using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using AvaloniaReactorUI.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AvaloniaReactorUI
{
    public interface IRxApplication
    {
        IRxHostElement Run();
        void Stop();
    }

    public static class RxApplicationBuilder<T> where T : RxComponent, new()
    {
        public static IRxApplication Create(Application application)
            => new RxApplication<T>(application);
    }

    internal abstract class RxApplication : VisualNode, IRxHostElement, IRxApplication
    {
        public static RxApplication? Instance { get; private set; }
        protected readonly Application _application;

        protected readonly IComponentLoader? _componentLoader;

        protected RxApplication(Application application)
        {
            var assemblyFileName = Environment.GetEnvironmentVariable("AvaloniaReactorUI.Host.HotReloadAssembly");

            if (!string.IsNullOrWhiteSpace(assemblyFileName))
            {
                _componentLoader = new WaitEventComponentLoader(assemblyFileName);//new AssemblyFileComponentLoader(assemblyFileName);
            }

            Instance = this;

            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public Action<UnhandledExceptionEventArgs>? UnhandledException { get; set; }

        internal void FireUnhandledExpectionEvent(Exception ex)
        {
            UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
            System.Diagnostics.Debug.WriteLine(ex);
        }

        public abstract IRxHostElement Run();

        public abstract void Stop();

        public RxApplication OnUnhandledException(Action<UnhandledExceptionEventArgs> action)
        {
            UnhandledException = action;
            return this;
        }

        public Window? ContainerWindow
        {
            get
            {
                if (_application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    return desktop.MainWindow;

                return null;
            }
        }
    }

    internal sealed class RxApplication<T> : RxApplication where T : RxComponent, new()
    {
        private RxComponent? _rootComponent;
        private bool _sleeping = true;


        internal RxApplication(Application application)
            : base(application)
        {
        }

        protected sealed override void OnAddChild(VisualNode widget, AvaloniaObject nativeControl)
        {
            if (nativeControl is Window window &&
                _application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = window;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a window (i.e. RxWindow)");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, AvaloniaObject nativeControl)
        {
            //MainPage can't be set to null!
            //_application.MainPage = null;
        }

        public override IRxHostElement Run()
        {
            if (_sleeping)
            {
                _rootComponent ??= new T();
                _sleeping = false;
                OnLayout();
            }

            if (_componentLoader != null)
            {
                _componentLoader.ComponentAssemblyChanged += OnComponentAssemblyChanged;
                _componentLoader.Run();
            }

            return this;
        }

        private void OnComponentAssemblyChanged(object? sender, EventArgs e)
        {
            try
            {
                Validate.EnsureNotNull(_componentLoader);

                var newComponent = _componentLoader.LoadComponent<T>();

                if (newComponent != null)
                {
                    _rootComponent = newComponent;
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }            
        }

        public override void Stop()
        {
            if (!_sleeping)
            {
                _sleeping = true;
            }

            if (_componentLoader != null)
            {
                _componentLoader.ComponentAssemblyChanged -= OnComponentAssemblyChanged;
                _componentLoader.Stop();
            }
        }

        protected internal override void OnLayoutCycleRequested()
        {
            if (!_sleeping)
            {
                //Device.BeginInvokeOnMainThread(OnLayout);
                Dispatcher.UIThread.Post(OnLayout);
                //OnLayout();
            }

            base.OnLayoutCycleRequested();
        }

        private void OnLayout()
        {
            try
            {
                Layout(this);
                SetupAnimationTimer();
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        protected override IEnumerable<VisualNode?> RenderChildren()
        {
            yield return _rootComponent;
        }

        private void SetupAnimationTimer()
        {
            if (IsAnimationFrameRequested)
            {
                DispatcherTimer.Run(() =>
                {
                    Animate();
                    return IsAnimationFrameRequested;
                }, TimeSpan.FromMilliseconds(16));
            }
        }
    }
}