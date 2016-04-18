using System;
using System.Linq;
using System.Windows;
using CommanderLucy.Messages;
using CommanderLucy.Services;
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
        private readonly IConfigService _configService;

        public ConfigView(IMessenger messenger, IConfigService configService)
        {
            InitializeComponent();

            _messenger = messenger;
            _configService = configService;
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
            _configService.SaveConfig(ViewModel.Commands.ToList());

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