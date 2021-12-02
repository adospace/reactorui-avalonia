using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

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
            RxApplicationBuilder<MainComponent>
                .Create(this)
                .Run();

            base.OnFrameworkInitializationCompleted();
        }
   }
}