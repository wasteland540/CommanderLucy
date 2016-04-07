using System.Windows.Input;
using CommanderLucy.Commands;
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

        public MainWindowViewModel(IMessenger messenger)
        {
            _messenger = messenger;
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
            var pluginManagerView = Container.Resolve<PluginView>();
            pluginManagerView.ShowDialog();
        }

        #endregion Private Methods
    }
}