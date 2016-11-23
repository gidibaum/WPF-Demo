using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace PrismDemo.Common
{
    public class CommonModule : IModule
    {
        readonly IRegionManager _RegionManager;
        readonly IUnityContainer _Container;

        public CommonModule(IUnityContainer container, RegionManager regionManager)
        {
            _RegionManager = regionManager;
            _Container = container;
        }

        public void Initialize()
        {
            
        }
    }
}