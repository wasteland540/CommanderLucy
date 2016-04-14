using System;
using System.Linq;
using System.Windows;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views
{
    /// <summary>
    ///     Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView
    {
        private readonly IMessenger _messenger;

        public ConfigView(IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;
            _messenger.Register<DeleteCommandRequestMsg>(this, OnDeleteCommandRequestMsg);
        }

        [Dependency]
        public ConfigViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }

            private get { return (ConfigViewModel) DataContext; }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //unregister messages
            _messenger.Unregister<DeleteCommandRequestMsg>(this, OnDeleteCommandRequestMsg);
            
            //Serialize Config
            Model.Command.Serialize(ConfigViewModel.ConfigFilename, ViewModel.Commands.ToArray());

            //send close msg
            _messenger.Send(new ConfigViewClosedMsg());
        }

        #region Private Methods

        private void OnDeleteCommandRequestMsg(DeleteCommandRequestMsg msg)
        {
            MessageBoxResult confirmDialog =
                MessageBox.Show("Are you sure, that you want to delete the selected command?",
                    "Security Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            _messenger.Send(confirmDialog == MessageBoxResult.Yes
                ? new DeleteCommandResponseMsg(true)
                : new DeleteCommandResponseMsg(false));
        }

        #endregion Private Methods
    }
}