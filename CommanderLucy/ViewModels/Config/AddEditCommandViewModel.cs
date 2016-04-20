using System.Collections.Generic;
using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.Model;
using CommanderLucy.Services;
using CommanderLucy.ViewModels.Base;
using GalaSoft.MvvmLight.Messaging;

namespace CommanderLucy.ViewModels.Config
{
    public class AddEditCommandViewModel : ViewModelBase
    {
        private readonly IConfigService _configService;
        private readonly IMessenger _messenger;
        private readonly IPluginService _pluginService;
        private Command _currentCommand;
        private List<string> _plugins;
        private ICommand _saveCommand;
        private string _title;

        public AddEditCommandViewModel(IMessenger messenger, IConfigService configService, IPluginService pluginService)
        {
            _messenger = messenger;
            _configService = configService;
            _pluginService = pluginService;

            //TODO: register plugin msg?
        }

        #region Properties

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public Command CurrentCommand
        {
            get
            {
                _currentCommand = _currentCommand ?? new Command {Type = CommandType.Basic};
                return _currentCommand;
            }
            set
            {
                _currentCommand = value;
                RaisePropertyChanged(() => CurrentCommand);
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new DelegateCommand(Save);
                return _saveCommand;
            }
        }

        public List<string> Plugins
        {
            get
            {
                return _plugins ??
                       (_plugins = _pluginService.GetPluginList());
            }

            set
            {
                _plugins = value;
                RaisePropertyChanged(() => Plugins);
            }
        }

        #endregion Properties

        #region Private Methods

        private void Save(object obj)
        {
            if (!IsEdit)
            {
                _configService.AddCommand(_currentCommand);
            }
            else
            {
                _configService.UpdateCommand(_currentCommand);
            }

            _messenger.Send(new ConfigUpdatedMsg());
            _messenger.Send(new CloseAddEditCommandViewMsg());
        }

        #endregion Private Methods

        public bool IsEdit { get; set; }
    }
}