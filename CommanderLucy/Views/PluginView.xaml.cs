using CommanderLucy.ViewModels;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for PluginView.xaml
    /// </summary>
    public partial class PluginView
    {
        public PluginView()
        {
            InitializeComponent();
        }

        [Dependency]
        public PluginViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}