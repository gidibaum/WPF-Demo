using Prism.Modularity;
using Prism.Unity;
using Prism.Regions;
using System;
using System.Linq;
using Microsoft.Practices.Unity;
using PrismDemo.Module1.Views;
using Base.Prism.Extensions;
using PrismDemo.Common.Services;
using PrismDemo.Module1.Services;

namespace PrismDemo.Module1
{
    public class Module1Module : IModule
    {
        readonly IRegionManager _Manager;
        readonly IUnityContainer _Container;

        public Module1Module(IUnityContainer container, IRegionManager manager)
        {
            _Manager = manager;
            _Container = container;
        }

        public void Initialize()
        {
            _Container.RegisterNavigationControl<Experience1View>();
            _Container.RegisterSingletonType<ICounterService, CounterService>();

            _Container.RegisterNavigationControl<ContentView>();

            //var t1 = _Container.Resolve<object>("ContentView");

            //var t =_Container.Registrations
            //    .FirstOrDefault(x => x.Name == "ContentView")?.MappedToType;

            _Manager.RegisterViewWithRegion("ContentRegion", typeof(ContentView));
        }
    }
}