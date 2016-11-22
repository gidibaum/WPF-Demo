using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace Base.Prism.Views
{

    public partial class RegionControl
    {
        public object TargetView { get; set; }
        readonly string _RegionName;

        public RegionControl()
        {
            InitializeComponent();
           
            _RegionName = Guid.NewGuid().ToString();
            RegionManager.SetRegionName(contentControl, _RegionName);
        }

        void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TargetView == null)
                return;

            var regionMng = ServiceLocator.Current.GetInstance<IRegionManager>();

            var viewName = TargetView.ToString();
            var region = regionMng.Regions[_RegionName];
            var control = ServiceLocator.Current.GetInstance<Control>(viewName);

            if (!region.Views.Contains(control))
                region.Add(control);

            region.NavigationService.RequestNavigate(new Uri(viewName, UriKind.Relative));
        }


        void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var regionMng = ServiceLocator.Current.GetInstance<IRegionManager>();

            var region = regionMng.Regions[_RegionName];

            region.NavigationService.RequestNavigate(new Uri("None", UriKind.Relative));
        }
    }
}
