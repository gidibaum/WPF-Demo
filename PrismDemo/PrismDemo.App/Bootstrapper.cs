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

using Base.Prism.Interfaces;
using Base.Prism.Services;
using Prism.Logging;
using Prism.Regions;
using PrismDemo.Common.Models;
using PrismDemo.App.Models;

namespace PrismDemo.App
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override ILoggerFacade CreateLogger() => new LoggerService();

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var catalog = (ModuleCatalog) this.ModuleCatalog;

            catalog.AddModule(typeof(Module1Module));

            WinConsole.Initialize();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));

            Container.RegisterInstance(Logger as ILoggerService);

            ((ILoggerService) Logger).Config(Container);


            Container.RegisterNavigationService<MainViews>("MainRegion");
            Container.RegisterNavigationControl<HomeView>();


            // Tab setup
            Container
                .RegisterNavigationService<TabViews>("TabRegion")
                .RegisterNavigationControlWithRegion<Tab1Control>("TabRegion")
                .RegisterNavigationControlWithRegion<Tab2Control>("TabRegion");


            ViewModelLocatorSetup.Setup();
        }

        protected override DependencyObject CreateShell() => Container.Resolve<MainWindow>();

        protected override void InitializeShell() => Application.Current.MainWindow.Show();
    }
}
