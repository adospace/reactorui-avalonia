using System;

namespace AvaloniaReactorUI.Internals
{
    public class AssemblyToReloadEventArgs : EventArgs
    {
        public AssemblyToReloadEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}