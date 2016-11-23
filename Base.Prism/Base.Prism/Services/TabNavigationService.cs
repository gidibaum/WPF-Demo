
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows.Controls;
using Base.Prism.Interfaces;


namespace Base.Prism.Services
{
    public class TabNavigationService<T> : NavigationService<T>, ITabNavigationService<T>
    {
        #region Property: SelectedTab

        Control _SelectedTab;
        public Control SelectedTab
        {
            get { return _SelectedTab; }
            set
            {
                if (SetProperty(ref _SelectedTab, value))
                {
                    if (SelectedTab?.Tag != null)
                        View = (T)Enum.Parse(typeof(T), SelectedTab.Tag.ToString());
                }
            }
        }

        #endregion

        public TabNavigationService(
          IRegionManager regionManager,
          IUnityContainer container,
          ILoggerService logger) 
            : base(regionManager, container, logger)
        {
        }

        public void InitTabItems()
        {
            var region = _RegionManager.Regions[RegionName];
            
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var viewName = item.ToString();

                //var view = region.Views.OfType<Control>().SingleOrDefault(o => o?.Tag?.ToString() == viewName);
                var view = region.Views.OfType<Control>().SingleOrDefault(o => o?.GetType().Name == viewName);

                if (view == null)
                {
                    var ctrl = _Container.Resolve<Control>(viewName);
                    ctrl.Tag = viewName;
                    region.Add(ctrl);
                }
            }

        }

        protected override void Navigate()
        {
            var viewName = _View.ToString();

            var region = _RegionManager.Regions[RegionName];

            region.NavigationService.RequestNavigate(new Uri(viewName, UriKind.Relative), NavigationCallback);
        }

    }
}
