using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AvaloniaReactorUI.DemoApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public override void OnFrameworkInitializationCompleted()
        {
            RxApplication.Create<TimerComponent>(this)
                .Run();

            base.OnFrameworkInitializationCompleted();
        }
   }
}