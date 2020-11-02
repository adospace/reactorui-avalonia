using Avalonia.Animation;
using Avalonia.Controls;
using AvaloniaReactorUI.ScaffoldApp;
using System;
using System.IO;
using System.Linq;

namespace AvaloniaReactorUI.ScaffoldConsole
{
    class Program
    {
        static void Main()
        {
            var animatable = new Animatable();
            var button = new Button();
            var types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                             // alternative: from domainAssembly in domainAssembly.GetExportedTypes()
                         from assemblyType in domainAssembly.GetTypes()
                         where typeof(Animatable).IsAssignableFrom(assemblyType)
                         // alternative: where assemblyType.IsSubclassOf(typeof(B))
                         // alternative: && ! assemblyType.IsAbstract
                         select assemblyType)
                .ToDictionary(_ => _.FullName, _ => _);

            foreach (var classNameToGenerate in File.ReadAllLines("WidgetList.txt"))
            {
                var typeToGenerate = types[classNameToGenerate];
                var targetPath = $@"..\..\..\..\AvaloniaReactorUI\Rx{typeToGenerate.Name}.cs";
                Console.WriteLine($"Generating {typeToGenerate.FullName} to {targetPath}...");

                var generator = new TypeSourceGenerator(typeToGenerate);
                File.WriteAllText(targetPath, generator.TransformAndPrettify());
            }
            
        }
    }
}
