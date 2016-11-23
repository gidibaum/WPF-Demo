using Prism.Modularity;
using Prism.Unity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using PrismDemo.Module1.Views;
using Base.Prism.Extensions;
using PrismDemo.Common.Services;
using PrismDemo.Module1.Services;

namespace PrismDemo.Module1
{
    public class Module1Module : IModule
    {
        readonly IUnityContainer _Container;

        public Module1Module(IUnityContainer container)
        {
            _Container = container;
        }

        public void Initialize()
        {
            _Container.RegisterNavigationControl<Experience1View>();
            _Container.RegisterSingletonType<ICounterService, CounterService>();
        }
    }
}