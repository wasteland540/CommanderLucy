using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.Model;
using CommanderLucy.ViewModels.Base;
using CommanderLucy.ViewModels.Config;
using CommanderLucy.Views.Config;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        public const string ConfigFilename = "CommandConfig.xml";
        private readonly IMessenger _messenger;
        private ObservableCollection<Command> _commands;
        private ICommand _deleteCommandCommand;
        private ICommand _editCommandCommand;
        private ICommand _newCommandCommand;
        private Command _selectedCommand;
        private AddEditCommandView _addEditCommandView;

        public ConfigViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Register<DeleteCommandResponseMsg>(this, OnDeleteCommandResponseMsg);
            _messenger.Register<AddEditCommandViewClosedMsg>(this, OnAddEditCommandViewClosedMsg);
        }
        
        #region Properties

        public ICommand DeleteCommandCommand
        {
            get
            {
                _deleteCommandCommand = _deleteCommandCommand ?? new DelegateCommand(DeleteCommand);
                return _deleteCommandCommand;
            }
        }

        public ICommand NewCommandCommand
        {
            get
            {
                _newCommandCommand = _newCommandCommand ?? new DelegateCommand(NewCommand);
                return _newCommandCommand;
            }
        }

        public ICommand EditCommandCommand
        {
            get
            {
                _editCommandCommand = _editCommandCommand ?? new DelegateCommand(EditCommand);
                return _editCommandCommand;
            }
        }

        public ObservableCollection<Command> Commands
        {
            get
            {
                if (_commands == null)
                {
                    if (File.Exists(ConfigFilename))
                    {
                        _commands = new ObservableCollection<Command>(Command.Deserialize(ConfigFilename).ToList());
                    }
                    else
                    {
                        _commands = new ObservableCollection<Command>
                        {
                            new Command
                            {
                                Name = "Hello Master",
                                CommandText = "Hello",
                                Type = CommandType.Basic,
                                Action = "https://github.com/wasteland540/CommanderLucy"
                            }
                        };
                    }
                }

                return _commands;
            }

            set
            {
                _commands = value;
                RaisePropertyChanged(() => Commands);
            }
        }

        public Command SelectedCommand
        {
            get { return _selectedCommand; }
            set
            {
                _selectedCommand = value;
                RaisePropertyChanged(() => SelectedCommand);
            }
        }

        #endregion Properties

        #region Private Methods

        private void NewCommand(object obj)
        {
            //TODO: implement NewCommand
            if (_addEditCommandView == null)
            {
                _addEditCommandView = Container.Resolve<AddEditCommandView>();
                
                var viewmodel = (AddEditCommandViewModel)_addEditCommandView.DataContext;
                viewmodel.Title = "Add New Command";

                _addEditCommandView.ShowDialog();
            }
        }

        private void EditCommand(object obj)
        {
            //TODO: implement EditCommand
            if (_selectedCommand != null)
            {
                if (_addEditCommandView == null)
                {
                    _addEditCommandView = Container.Resolve<AddEditCommandView>();

                    var viewmodel = (AddEditCommandViewModel) _addEditCommandView.DataContext;
                    viewmodel.Title = "Edit Command";
                    viewmodel.CurrentCommand = _selectedCommand;

                    _addEditCommandView.ShowDialog();
                }
            }
        }

        private void DeleteCommand(object obj)
        {
            if (_selectedCommand != null)
            {
                _messenger.Send(new DeleteCommandRequestMsg());
            }
        }

        private void OnDeleteCommandResponseMsg(DeleteCommandResponseMsg msg)
        {
            if (msg.IsSure)
            {
                _commands.Remove(_selectedCommand);
                Commands = _commands;
            }
        }
        
        private void OnAddEditCommandViewClosedMsg(AddEditCommandViewClosedMsg msg)
        {
            _addEditCommandView = null;
        }

        #endregion Private Methods
    }
}