using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AvaloniaReactorUI.Host
{
    class Program
    {
        public static int Main(string[] args)
        {
#if DEBUG
            if (args == null ||
                args.Length == 0)
            {
                args = new[] { @"AvaloniaReactorUI.DemoApp.dll" };
            }
#endif
            if (args == null ||
                args.Length == 0)
            {
                Console.WriteLine("Specify the assembly path to load");
                return -1;
            }

            var assemblyPath = args[0];

            if (!Path.IsPathRooted(assemblyPath))
            {
                assemblyPath = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyPath);
            }

            if (!File.Exists(assemblyPath))
            {
                Console.WriteLine($"The assembly '{assemblyPath}' doesn't exist");
                return -1;
            }

            var assemblyPdbPath = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb");

            var assembly = File.Exists(assemblyPdbPath) ?
                Assembly.Load(File.ReadAllBytes(assemblyPath)) 
                :
                Assembly.Load(File.ReadAllBytes(assemblyPath), File.ReadAllBytes(assemblyPdbPath));

            assembly
                .GetTypes()
                .First(_ => _.Name == "Program")
                .GetMethod("Main")
                .Invoke(null, new object[] { args.Skip(1).ToArray() });

            return 0;
        }

    }
}
