using Avalonia.Animation;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AvaloniaReactorUI.ScaffoldApp
{
    class MainWindowViewModel : ViewModelBase
    {
        private MainWindow _view;

        public MainWindowViewModel(MainWindow view)
        {
            _view = view;
            _types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    // alternative: from domainAssembly in domainAssembly.GetExportedTypes()
                from assemblyType in domainAssembly.GetTypes()
                where typeof(Animatable).IsAssignableFrom(assemblyType)
                // alternative: where assemblyType.IsSubclassOf(typeof(B))
                // alternative: && ! assemblyType.IsAbstract
                select assemblyType)
                    .OrderBy(_ => _.FullName)
                    .ToList();
        }

        //public string AssemblyPath { get; set; }

        private string _assemblyPath;
        public string AssemblyPath
        {
            get => _assemblyPath;
            set
            {
                if (_assemblyPath != value)
                {
                    _assemblyPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _sourceCode;
        public string SourceCode
        {
            get => _sourceCode;
            set
            {
                if (_sourceCode != value)
                {
                    _sourceCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private Type _selectedType;
        public Type SelectedType
        {
            get => _selectedType;
            set
            {
                if (_selectedType != value)
                {
                    _selectedType = value;
                    OnPropertyChanged();
                    GenerateSourceForType();
                }
            }
        }

        private void GenerateSourceForType()
        {
            if (SelectedType == null)
            {
                SourceCode = null;
                return;
            }

            var generator = new TypeSourceGenerator(SelectedType);
            SourceCode = generator.TransformAndPrettify();
        }

        public async void BrowseAssemblyPath()
        {
            var dlg = new OpenFileDialog
            {
                Filters = new[] { new FileDialogFilter() { Name = "Assembly files (*.dll,*.exe)", Extensions = new[] { "*.dll", "*.exe" }.ToList() } }.ToList()
            };

            var fileNames = await dlg.ShowAsync(_view);
            if (fileNames.Length == 1)
            {
                AssemblyPath = fileNames[0];

                Types = (
                    // alternative: from domainAssembly in domainAssembly.GetExportedTypes()
                    from assemblyType in Assembly.LoadFrom(AssemblyPath).GetTypes()
                    where typeof(Animatable).IsAssignableFrom(assemblyType)
                    // alternative: where assemblyType.IsSubclassOf(typeof(B))
                    // alternative: && ! assemblyType.IsAbstract
                    select assemblyType)
                .OrderBy(_ => _.Name)
                .ToList();
            }
        }

        private IEnumerable<Type> _types;

        public IEnumerable<Type> Types
        {
            get => _types;
            set
            {
                if (_types != value)
                {
                    _types = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
