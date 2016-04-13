using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CommanderLucy.Commands;
using CommanderLucy.Messages;
using CommanderLucy.ViewModels.Base;
using GalaSoft.MvvmLight.Messaging;

namespace CommanderLucy.ViewModels
{
    public class PluginViewModel : ViewModelBase
    {
        private const string PluginFolderName = "Plugins";
        private const string PluginExtension = ".dll";
        private readonly IMessenger _messenger;
        private ICommand _addPluginCommand;
        private ICommand _chooseFileCommand;
        private ICommand _deletePluginCommand;
        private List<string> _pluginList;
        private string _pluginPath;
        private string _selectedPlugin;

        public PluginViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Register<ChoosePluginFileResponseMsg>(this, OnChoosePluginFileResponseMsg);
            _messenger.Register<DeletePluginSecurityResponseMsg>(this, OnDeletePluginSecurityResponseMsg);
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
            get
            {
                return _pluginList ??
                       (_pluginList =
                           Directory.GetFiles(PluginFolderName)
                               // ReSharper disable once PossibleNullReferenceException
                               // ReSharper disable once AssignNullToNotNullAttribute
                               .Select(n => Path.GetFileName(n).Replace(Path.GetExtension(n), ""))
                               .ToList());
            }
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
                if (!Directory.Exists(PluginFolderName))
                {
                    Directory.CreateDirectory(PluginFolderName);
                }

                try
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    File.Copy(_pluginPath, Path.Combine(PluginFolderName, Path.GetFileName(_pluginPath)));

                    ReloadPluginList();
                    PluginPath = string.Empty;
                }
                catch (Exception e)
                {
                    _messenger.Send(new CannotAddPluginMsg(e.Message));
                }
            }
        }

        private void ReloadPluginList()
        {
            PluginList =
                Directory.GetFiles(PluginFolderName)
                    // ReSharper disable once PossibleNullReferenceException
                    // ReSharper disable once AssignNullToNotNullAttribute
                    .Select(n => Path.GetFileName(n).Replace(Path.GetExtension(n), "")).ToList();
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
                if (Directory.Exists(PluginFolderName) && !string.IsNullOrEmpty(_selectedPlugin))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    string filePath = Path.Combine(PluginFolderName, Path.GetFileName(_selectedPlugin)) +
                                      PluginExtension;
                    File.Delete(filePath);

                    ReloadPluginList();
                }
            }
        }

        #endregion Private Methods
    }
}