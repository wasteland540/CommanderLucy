using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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

        private void btnTopMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideTopMenu1", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
            btnTopMenuShow2.Visibility = Visibility.Visible;
        }

        private void btnTopMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowTopMenu1", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
            btnTopMenuShow2.Visibility = Visibility.Hidden;
        }

        private void btnTopMenu2Hide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideTopMenu2", btnTopMenuHide2, btnTopMenuShow2, pnlTopMenu2);
            btnTopMenuShow.Visibility = Visibility.Visible;
        }

        private void btnTopMenu2Show_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowTopMenu2", btnTopMenuHide2, btnTopMenuShow2, pnlTopMenu2);
            btnTopMenuShow.Visibility = Visibility.Hidden;
        }

        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            var sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = Visibility.Visible;
                btnShow.Visibility = Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = Visibility.Hidden;
                btnShow.Visibility = Visibility.Visible;
            }
        }
    }
}