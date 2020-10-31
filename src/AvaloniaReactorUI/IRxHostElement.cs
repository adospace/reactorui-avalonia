using Avalonia.Controls;

namespace AvaloniaReactorUI
{
    public interface IRxHostElement
    {
        IRxHostElement Run();

        void Stop();

        Window ContainerWindow { get; }
    }
}