﻿using System.Windows;
using CommanderLucy.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CommanderLucy
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new UnityContainer();

            //service registrations
            //Container.RegisterType<IDatabaseService, DatabaseService>();

            //registraions utils
            //only one instance from messenger can exists! (recipient problems..)
            var messenger = new Messenger();
            Container.RegisterInstance(typeof (IMessenger), messenger);

            var mainView = Container.Resolve<MainWindow>();
            mainView.Show();
        }
    }
}