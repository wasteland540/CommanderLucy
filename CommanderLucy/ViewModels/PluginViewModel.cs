using System.Collections.Generic;
using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.Services;
using CommanderLucy.ViewModels.Base;
using GalaSoft.MvvmLight.Messaging;

namespace CommanderLucy.ViewModels
{
    public class PluginViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IPluginService _pluginService;
        private ICommand _addPluginCommand;
        private ICommand _chooseFileCommand;
        private ICommand _deletePluginCommand;
        private List<string> _pluginList;
        private string _pluginPath;
        private string _selectedPlugin;

        public PluginViewModel(IMessenger messenger, IPluginService pluginService)
        {
            _messenger = messenger;
            _pluginService = pluginService;
            _messenger.Register<ChoosePluginFileResponseMsg>(this, OnChoosePluginFileResponseMsg);
            _messenger.Register<DeletePluginSecurityResponseMsg>(this, OnDeletePluginSecurityResponseMsg);
            _messenger.Register<PluginAddedMsg>(this, OnPluginAddedMsg);
            _messenger.Register<PluginDeletedMsg>(this, OnPluginDeletedMsg);
        }

        #region Properties

        public ICommand DeletePluginCommand
        {
            get
            {
                _deletePluginCommand = _deletePluginCommand ?? new DelegateCommand(DeletePlugin);
                return _deletePluginCommand;
            }
        }

        public ICommand ChooseFileCommand
        {
            get
            {
                _chooseFileCommand = _chooseFileCommand ?? new DelegateCommand(ChooseFile);
                return _chooseFileCommand;
            }
        }

        public ICommand AddPluginCommand
        {
            get
            {
                _addPluginCommand = _addPluginCommand ?? new DelegateCommand(AddPlugin);
                return _addPluginCommand;
            }
        }

        public string PluginPath
        {
            get { return _pluginPath; }
            set
            {
                _pluginPath = value;
                RaisePropertyChanged(() => PluginPath);
            }
        }

        public List<string> PluginList
        {
            get { return _pluginList ?? (_pluginList = _pluginService.GetPluginList()); }
            set
            {
                _pluginList = value;
                RaisePropertyChanged(() => PluginList);
            }
        }

        public string SelectedPlugin
        {
            get { return _selectedPlugin; }
            set
            {
                _selectedPlugin = value;
                RaisePropertyChanged(() => SelectedPlugin);
            }
        }

        #endregion Properties

        #region Private Methods

        private void DeletePlugin(object obj)
        {
            _messenger.Send(new DeletePluginSecurityRequestMsg());
        }

        private void ChooseFile(object obj)
        {
            _messenger.Send(new ChoosePluginFileRequestMsg());
        }

        private void AddPlugin(object obj)
        {
            if (!string.IsNullOrEmpty(_pluginPath))
            {
                _pluginService.AddPlugin(_pluginPath);
            }
        }

        private void OnPluginAddedMsg(PluginAddedMsg msg)
        {
            ReloadPluginList();
            PluginPath = string.Empty;
        }

        private void ReloadPluginList()
        {
            PluginList = _pluginService.GetPluginList();
        }

        private void OnChoosePluginFileResponseMsg(ChoosePluginFileResponseMsg msg)
        {
            if (!string.IsNullOrEmpty(msg.PluginFilename))
            {
                PluginPath = msg.PluginFilename;
            }
        }

        private void OnDeletePluginSecurityResponseMsg(DeletePluginSecurityResponseMsg msg)
        {
            if (msg.IsSure)
            {
                _pluginService.DeletePlugin(_selectedPlugin);
            }
        }

        private void OnPluginDeletedMsg(PluginDeletedMsg msg)
        {
            ReloadPluginList();
        }

        #endregion Private Methods
    }
}