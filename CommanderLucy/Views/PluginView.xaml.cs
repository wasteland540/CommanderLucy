using System;
using System.Windows;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using Microsoft.Win32;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for PluginView.xaml
    /// </summary>
    public partial class PluginView
    {
        private readonly IMessenger _messenger;

        public PluginView(IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;
            _messenger.Register<ChoosePluginFileRequestMsg>(this, OnChoosePluginFileRequestMsg);
            _messenger.Register<DeletePluginSecurityRequestMsg>(this, OnDeletePluginSecurityRequestMsg);
            _messenger.Register<CannotAddPluginMsg>(this, OnCannotAddPluginMsg);
        }
        
        [Dependency]
        public PluginViewModel ViewModel
        {
            set { DataContext = value; }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //unregister messages
            _messenger.Unregister<ChoosePluginFileRequestMsg>(this, OnChoosePluginFileRequestMsg);
            _messenger.Unregister<DeletePluginSecurityRequestMsg>(this, OnDeletePluginSecurityRequestMsg);
            _messenger.Unregister<CannotAddPluginMsg>(this, OnCannotAddPluginMsg);

            //send close msg
            _messenger.Send(new PluginViewClosedMsg());
        }

        #region Private Methods

        private void OnChoosePluginFileRequestMsg(ChoosePluginFileRequestMsg msg)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Commander Plugins (*.dll)|*.dll",
                Multiselect = false
            };

            bool? result = openFileDialog.ShowDialog();

            if (result != null && result.Value)
            {
                _messenger.Send(new ChoosePluginFileResponseMsg(openFileDialog.FileName));
            }
        }

        private void OnDeletePluginSecurityRequestMsg(DeletePluginSecurityRequestMsg msg)
        {
            MessageBoxResult confirmDialog =
                MessageBox.Show("Are you sure, that you want to delete the selected plugin?",
                    "Security Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            _messenger.Send(confirmDialog == MessageBoxResult.Yes
                ? new DeletePluginSecurityResponseMsg(true)
                : new DeletePluginSecurityResponseMsg(false));
        }

        private void OnCannotAddPluginMsg(CannotAddPluginMsg msg)
        {
            MessageBox.Show("Can not add plugin, because: " + msg.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion Private Methods
    }
}