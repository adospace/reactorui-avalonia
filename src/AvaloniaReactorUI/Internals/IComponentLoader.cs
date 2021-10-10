using System;

namespace AvaloniaReactorUI.Internals
{
    internal interface IComponentLoader
    {
       RxComponent LoadComponent<T>() where T : RxComponent, new();

       event EventHandler ComponentAssemblyChanged;

       void Run();

       void Stop();
    }
}