using CommanderLucy.ViewModels;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView
    {
        public ConfigView()
        {
            InitializeComponent();
        }

        [Dependency]
        public ConfigViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}