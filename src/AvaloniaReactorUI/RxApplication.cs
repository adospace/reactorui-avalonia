using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using AvaloniaReactorUI.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AvaloniaReactorUI
{
    public abstract class RxApplication : VisualNode, IRxHostElement
    {
        public static RxApplication Instance { get; private set; }
        protected readonly Application _application;

        //internal IComponentLoader ComponentLoader { get; set; } = new LocalComponentLoader();

        protected RxApplication(Application application)
        {
            //if (Instance != null)
            //{
            //    throw new InvalidOperationException("Only one instance of RxApplication is permitted");
            //}

            Instance = this;

            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public Action<UnhandledExceptionEventArgs> UnhandledException { get; set; }

        internal void FireUnhandledExpectionEvent(Exception ex)
        {
            UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
            System.Diagnostics.Debug.WriteLine(ex);
        }

        public abstract IRxHostElement Run();

        public abstract void Stop();

        public static RxApplication Create<T>(Application application) where T : RxComponent, new()
            => new RxApplication<T>(application);

        public static RxApplication CreateWithHotReload<T>(Application application) where T : RxComponent, new()
            => new RxHotReloadApplication<T>(application);

        public RxApplication WithContext(string key, object value)
        {
            Context[key] = value;
            return this;
        }

        public RxApplication OnUnhandledException(Action<UnhandledExceptionEventArgs> action)
        {
            UnhandledException = action;
            return this;
        }

        //public INavigation Navigation => _application.MainWindow?.Navigation;

        public RxContext Context { get; } = new RxContext();

        public Window ContainerWindow
        {
            get
            {
                if (_application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    return desktop.MainWindow;

                return null;
            }
        }

    }

    public class RxApplication<T> : RxApplication where T : RxComponent, new()
    {
        protected RxComponent _rootComponent;
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

            return this;
        }

        public override void Stop()
        {
            if (!_sleeping)
            {
                _sleeping = true;
            }
        }

        protected internal override void OnLayoutCycleRequested()
        {
            if (!_sleeping)
            {
                //Device.BeginInvokeOnMainThread(OnLayout);
                //Dispatcher.UIThread.Post(OnLayout);
                OnLayout();
            }

            base.OnLayoutCycleRequested();
        }

        private void OnLayout()
        {
            try
            {
                Layout();
                SetupAnimationTimer();
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
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

    public class RxHotReloadApplication<T> : RxApplication<T> where T : RxComponent, new()
    {
        private string _assemblyPath;
        private readonly HotReloadServer _hotReloadServer;


        internal RxHotReloadApplication(Application application, int serverPort = 45821) : base(application)
        {
            _assemblyPath = application.GetType().Assembly.Location;
            _hotReloadServer = new HotReloadServer(serverPort);
        }

        public override IRxHostElement Run()
        {
            _hotReloadServer.HotReloadCommandIssued += OnHotReloadServer_HotReloadCommandIssued;
            _hotReloadServer.Start();

            return base.Run();
        }

        private void OnHotReloadServer_HotReloadCommandIssued(object sender, EventArgs e)
        {
            try
            {
                var type = Assembly.LoadFrom(_assemblyPath).GetType(typeof(T).FullName);
                var newComponent = (RxComponent)Activator.CreateInstance(type);

                if (newComponent != null)
                {
                    _rootComponent = newComponent;
                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        public override void Stop()
        {
            _hotReloadServer.Stop();

            base.Stop();
        }
    }
}