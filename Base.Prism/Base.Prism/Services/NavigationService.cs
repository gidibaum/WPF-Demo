
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows.Controls;
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

        protected readonly IRegionManager _RegionManager;
        protected readonly ILoggerService _Logger;


        protected virtual void Navigate()
        {
            _RegionManager.RequestNavigate(RegionName, _View.ToString(), NavigationCallback);
        }


        public virtual void NavigationCallback(NavigationResult navResult)
        {
            _Logger?.Log($"Navigation to {View}", Category.Info);
        }

        public NavigationService(
            IRegionManager regionManager,                        
            ILoggerService logger = null)            
        {
            _RegionManager = regionManager;
            _Logger = logger;
        }
    }
}
