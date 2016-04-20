using System;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IMessenger _messenger;

        public MainWindow(IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;

            ContentRendered += MainWindow_ContentRendered;
        }

        [Dependency]
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }

        #region Private Methods

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            ContentRendered -= MainWindow_ContentRendered;

            _messenger.Send(new MainWindowInitializedMsg());
        }

        #endregion Private Methods
    }
}