using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Model;
using CommanderLucy.ViewModels.Base;

namespace CommanderLucy.ViewModels.Config
{
    public class AddEditCommandViewModel : ViewModelBase
    {
        private Command _currentCommand;
        private string _title;
        private ICommand _saveCommand;

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

        #endregion Properties

        #region Private Methods

        private void Save(object obj)
        {
            //TODO: implement Save
        }

        #endregion Private Methods
    }
}