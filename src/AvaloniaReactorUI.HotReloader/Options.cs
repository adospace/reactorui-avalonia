using CommandLine;

namespace AvaloniaReactorUI.HotReloader;

public class Options
{
    [Option('p', "proj", Required = false)]
    public string? ProjectFileName { get; set; }

    [Option('d', "dir", Required = false)]
    public string? WorkingDirectory { get; set; }
}