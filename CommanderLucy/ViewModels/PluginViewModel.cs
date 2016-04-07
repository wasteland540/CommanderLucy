using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.ViewModels.Base;

namespace CommanderLucy.ViewModels
{
    public class PluginViewModel : ViewModelBase
    {
        private ICommand _deletePluginCommand;

        #region Properties

        public ICommand DeletePluginCommand
        {
            get
            {
                _deletePluginCommand = _deletePluginCommand ?? new DelegateCommand(DeletePlugin);
                return _deletePluginCommand;
            }
        }

        #endregion Properties

        #region Private Methods

        private void DeletePlugin(object obj)
        {
            
        }

        #endregion Private Methods
    }
}