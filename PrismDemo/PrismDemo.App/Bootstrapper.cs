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
using Prism.Regions;
using PrismDemo.App.ViewModels;
using PrismDemo.Common.Models;
using PrismDemo.App.Models;
using TabNavigationDemo.Views;

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


            Container.RegisterNavigationService<INavigationService<MainViews>, NavigationService<MainViews>>(
                "MainRegion");
            Container.RegisterNavigationControl<HomeView>();

            ViewModelLocatorSetup.Setup();


            //TODO: Tabs are not correct!
            Container.RegisterSingletonType<ITabNavigationService<TabViews>, TabNavigationService<TabViews>>();
            Container.RegisterTabNavigationService<TabViews>("TabRegion");

            var region = Container.Resolve<IRegionManager>();
            region.RegisterNavigationControlWithRegion<Tab1Control>("TabRegion");
            region.RegisterNavigationControlWithRegion<Tab2Control>("TabRegion");

        }

        protected override DependencyObject CreateShell() => Container.Resolve<MainWindow>();

        protected override void InitializeShell() => Application.Current.MainWindow.Show();

        //protected override void InitializeModules()
        //{
        //    base.InitializeModules();


        //    //var region = Container.Resolve<IRegionManager>();
        //    //region.RegisterNavigationControlWithRegion<Tab1Control>("TabRegion");
        //    //region.RegisterNavigationControlWithRegion<Tab2Control>("TabRegion");

        //    //Application.Current.MainWindow.Show();
        //}
    }
}
