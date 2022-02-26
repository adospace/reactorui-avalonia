using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;

namespace AvaloniaReactorUI.HotReloader
{
    class Program
    {

        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine($"MauiReactor Hot-Reload CLI");
            Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}");

            var optionsParsed = Parser.Default.ParseArguments<Options>(args);

            await optionsParsed.WithParsedAsync(RunMonitorAndConnectionClient);

            return 0;
        }

        private static async Task RunMonitorAndConnectionClient(Options options)
        {
            var hotReloadClient = new HotReloadClient(options);

            Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");

            var tsc = new CancellationTokenSource();
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => tsc.Cancel();
            }

            await hotReloadClient.Run(tsc.Token);            
        }

    }
}
