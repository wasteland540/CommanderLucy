using CommanderLucy.ViewModels;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Dependency]
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}