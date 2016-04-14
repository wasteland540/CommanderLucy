using System;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels.Config;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.Views.Config
{
    /// <summary>
    ///     Interaction logic for AddEditCommandView.xaml
    /// </summary>
    public partial class AddEditCommandView
    {
        private readonly IMessenger _messenger;

        public AddEditCommandView(IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;
        }

        [Dependency]
        public AddEditCommandViewModel ViewModel
        {
            set { DataContext = value; }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //unregister messages
            //TODO:_messenger.Unregister<DeleteCommandRequestMsg>(this, OnDeleteCommandRequestMsg);

            //send close msg
            _messenger.Send(new AddEditCommandViewClosedMsg());
        }
    }
}