using System;
using System.Globalization;
using Microsoft.Practices.Unity;
using Prism.Unity;
using PrismDemo.App.Views;
using System.Windows;
using Base;
using Base.Prism;
using Base.Prism.Extensions;
using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using PrismDemo.Module1;
using Prism.Mvvm;
using Base.Prism.Interfaces;
using Base.Prism.Services;
using Prism.Logging;
using PrismDemo.App.ViewModels;
using PrismDemo.Common.Models;

namespace PrismDemo.App
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override ILoggerFacade CreateLogger() => new LoggerService();

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var catalog = (ModuleCatalog)this.ModuleCatalog;

            catalog.AddModule(typeof(Module1Module));

            WinConsole.Initialize();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance(Logger as ILoggerService);

            ((ILoggerService) Logger).Config(Container);


            Container.RegisterNavigationService<INavigationService<MainViews>, NavigationService<MainViews>>("MainRegion");
            Container.RegisterNavigationControl<HomeView>();

            //Container.RegisterSingletonType<IConfig, Config>();


            ViewModelLocatorSetup.Setup();
        }

        protected override DependencyObject CreateShell() => Container.Resolve<MainWindow>();

        protected override void InitializeShell() => Application.Current.MainWindow.Show();
    }
}
