using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaReactorUI.ScaffoldApp
{
    public class MainWindow : Window
    {
        public MainWindow()
        {

            this.DataContext = new MainWindowViewModel(this);

            InitializeComponent();
//#if DEBUG
//            this.AttachDevTools();
//#endif
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
