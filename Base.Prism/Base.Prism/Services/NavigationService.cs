
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows.Controls;
using Base;
using Base.Prism.Interfaces;
using Prism.Logging;

namespace Base.Prism.Services
{
    public class NavigationService<T> : ObservableObject, INavigationService<T>
    {
        protected ExclusiveAction _DoOnce = new ExclusiveAction();

        public string RegionName { get; set; }

        #region Property: View

        protected T _View;
        public T View
        {
            get { return _View; }
            set
            {
                SetProperty(ref _View, value);

                _DoOnce.Do(Navigate);
            }
        }

        #endregion

        protected readonly IUnityContainer _Container;
        protected readonly IRegionManager _RegionManager;
        protected readonly ILoggerService _Logger;


        protected virtual void Navigate()
        {
            var viewName = _View.ToString();

            if (!_RegionManager.Regions.ContainsRegionWithName(RegionName)) return;


            var region = _RegionManager.Regions[RegionName];
            var view = region.Views.SingleOrDefault(o => o != null && o.GetType().Name == viewName);

            if (view == null)
            {
                var ctrl = _Container.Resolve<Control>(viewName);
                region.Add(ctrl);
            }

            region.NavigationService.RequestNavigate(new Uri(viewName, UriKind.Relative), (r) => NavigationCallback(r));
        }


        public virtual void NavigationCallback(NavigationResult navResult)
        {
            _Logger?.Log($"Navigation to {View}", Category.Info);
        }

        public NavigationService(
            IRegionManager regionManager,                        
            IUnityContainer container,
            ILoggerService logger = null)            
        {
            _Container = container;
            _RegionManager = regionManager;
            _Logger = logger;
        }
    }
}
