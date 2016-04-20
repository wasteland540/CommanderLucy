using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.Services;
using CommanderLucy.ViewModels.Base;
using CommanderLucy.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly ISpeechService _speechService;
        private ConfigView _configManagerView;
        private ICommand _openConfigManagerCommand;
        private ICommand _openPluginManagerCommand;
        private PluginView _pluginManagerView;

        public MainWindowViewModel(IMessenger messenger, ISpeechService speechService)
        {
            _messenger = messenger;
            _speechService = speechService;
            _messenger.Register<PluginViewClosedMsg>(this, OnPluginViewClosedMsg);
            _messenger.Register<ConfigViewClosedMsg>(this, OnConfigViewClosedMsg);
            _messenger.Register<MainWindowInitializedMsg>(this, OnMainWindowInitializedMsg);
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

        public ICommand OpenConfigManagerCommand
        {
            get
            {
                _openConfigManagerCommand = _openConfigManagerCommand ?? new DelegateCommand(OpenConfigManagerView);
                return _openConfigManagerCommand;
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

        private void OpenConfigManagerView(object obj)
        {
            if (_configManagerView == null)
            {
                _configManagerView = Container.Resolve<ConfigView>();
                _configManagerView.ShowDialog();
            }
        }

        private void OnConfigViewClosedMsg(ConfigViewClosedMsg msg)
        {
            _configManagerView = null;
        }

        private void OnMainWindowInitializedMsg(MainWindowInitializedMsg msg)
        {
            _speechService.StartRecognizing();
        }

        #endregion Private Methods
    }
}