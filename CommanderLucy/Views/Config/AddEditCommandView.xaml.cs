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
            _messenger.Register<CloseAddEditCommandViewMsg>(this, OnCloseAddEditCommandViewMsg);
        }

        [Dependency]
        public AddEditCommandViewModel ViewModel
        {
            set { DataContext = value; }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            //send close msg
            _messenger.Send(new AddEditCommandViewClosedMsg());
        }

        #region Private Methods

        private void OnCloseAddEditCommandViewMsg(CloseAddEditCommandViewMsg msg)
        {
            Close();
        }

        #endregion Private Methods
    }
}