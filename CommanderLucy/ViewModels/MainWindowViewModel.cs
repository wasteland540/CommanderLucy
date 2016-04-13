using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels.Base;
using CommanderLucy.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private ICommand _openPluginManagerCommand;
        private PluginView _pluginManagerView;

        public MainWindowViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Register<PluginViewClosedMsg>(this, OnPluginViewClosedMsg);
        }

        #region Properties

        public ICommand OpenPluginManagerCommand
        {
            get
            {
                _openPluginManagerCommand = _openPluginManagerCommand ?? new DelegateCommand(OpenPluginManagerView);
                return _openPluginManagerCommand;
            }
        }

        #endregion Properties

        #region Private Methods

        private void OpenPluginManagerView(object obj)
        {
            if (_pluginManagerView == null)
            {
                _pluginManagerView = Container.Resolve<PluginView>();
                _pluginManagerView.ShowDialog();
            }
        }

        private void OnPluginViewClosedMsg(PluginViewClosedMsg msg)
        {
            _pluginManagerView = null;
        }

        #endregion Private Methods
    }
}